﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace FinaroEngine.Library
{
    public class OrderProc
    {
        public static string AddNewOrder(string constring, int userId, int entityId, TradeType tradeType, decimal price, int quantity)
        {
            Guid orderId = Guid.NewGuid();
            
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectOrdersForMatch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", entityId));
                    cmd.Parameters.Add(new SqlParameter("@TRADETYPEID", (int)tradeType));
                    cmd.Parameters.Add(new SqlParameter("@PRICE", price));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            DataRow row = dt.NewRow();
                            row["OrderId"] = orderId;
                            row["UserId"] = userId;
                            row["EntityId"] = entityId;
                            row["TradeTypeId"] = (int)tradeType;
                            row["Price"] = price;
                            row["Date"] = DateTime.Now;
                            row["Quantity"] = quantity;
                            row["Status"] = (int)Status.Open;

                            bool updated;
                            MarketData marketData;
                            DataTable updatedOrders = MatchOrders(entityId, row, dt, out updated, out marketData);                         
                            
                            sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();             

                            if (updated)
                            {
                                UpdateMarketData(constring, marketData);
                                sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();
                                marketData = marketData == null ? marketData : GetMarketDataStrong(constring, userId, entityId);
                            }

                            sda.Update(dt);
                            

                            return JsonConvert.SerializeObject(new { market = marketData, orderbook = updatedOrders });
                        }
                    }
                }
            }
        }

        public static string GetNewOrders(string constring, int userId, int entityId)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectOrders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", entityId));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return JsonConvert.SerializeObject(new { data = dt });
                        }
                    }
                }
            }
        }

        public static string GetMarketData(string constring, int userId, int entityId)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectMarketData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", entityId));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return JsonConvert.SerializeObject(new { data = dt });
                        }
                    }
                }
            }
        }

        public static MarketData GetMarketDataStrong(string constring, int userId, int entityId)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectMarketData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", entityId));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            MarketData marketData = new MarketData();
                            DataRow dr = dt.Rows[0];
                            marketData.EntityId = entityId;
                            marketData.LastTradePrice = Convert.ToDecimal(dr["LastTradePrice"]);
                            marketData.LastTradeTime = Convert.ToDateTime(dr["LastTradeTime"]);
                            marketData.MarketPrice = Convert.ToDecimal(dr["MarketPrice"]);
                            marketData.Volume = Convert.ToInt32(dr["Volume"]);
                            marketData.ChangeInPrice = Convert.ToDecimal(dr["ChangeInPrice"]);
                            return marketData;
                        }
                    }
                }
            }
        }

        public static DataTable MatchOrders(int entityId, DataRow newOrder, DataTable orderBook, out bool updated, out MarketData marketData)
        {
            bool isUpdated = false;            
            
            DataTable updatedOrders = orderBook.Clone();
            marketData = new MarketData();
            marketData.EntityId = entityId;
            marketData.Volume = 0;
            marketData.LastTradeTime = null;
            marketData.LastTradePrice = null;
            marketData.MarketPrice = null;

            foreach (DataRow dr in orderBook.Rows)
            {

                int quant = Convert.ToInt32(dr["Quantity"]);
                int newQuant = Convert.ToInt32(newOrder["Quantity"]);
                marketData.LastTradeTime = Convert.ToDateTime(newOrder["Date"]);
                marketData.LastTradePrice = Convert.ToDecimal(newOrder["Price"]); 
                isUpdated = true;

                //check if prices are equal, if so, mark this as market price
                if(dr["Price"] == newOrder["Price"])
                {
                    marketData.MarketPrice = Convert.ToDecimal(dr["Price"]);
                }

                //after each pass, lets check if quantity == 0, then it's filled, break
                if (newQuant == 0)
                {
                    newOrder["Status"] = (int)Status.Filled;
                    break;
                }

                //new order has same amount as the first row
                else if (newQuant == quant)
                {
                    marketData.Volume = marketData.Volume + quant;
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;                    
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    break;
                }

                //new order has more shares than the first row
                else if (newQuant > quant)
                {
                    marketData.Volume = marketData.Volume + quant;
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = newQuant - quant;
                    newOrder["Status"] = (int)Status.Partial;
                }

                //new order has less shares than the first row
                else if (newQuant < quant)
                {
                    marketData.Volume = marketData.Volume + newQuant;
                    dr["Quantity"] = quant - newQuant;
                    dr["Status"] = (int)Status.Partial;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    break;
                }
                
            }
                        
            orderBook.Rows.Add(newOrder);
            updated = isUpdated;
            updatedOrders.ImportRow(newOrder);
            marketData = !isUpdated ? null : marketData;
            return updatedOrders;
        }

        public static void UpdateMarketData(string connectionString, MarketData marketData)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("spInsertMarketData", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", marketData.EntityId));
                    cmd.Parameters.Add(new SqlParameter("@VOLUME", marketData.Volume));
                    cmd.Parameters.Add(new SqlParameter("@LASTTRADETIME", marketData.LastTradeTime));
                    cmd.Parameters.Add(new SqlParameter("@LASTTRADEPRICE", marketData.LastTradePrice));
                    cmd.Parameters.Add(new SqlParameter("@MARKETPRICE", marketData.MarketPrice));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
