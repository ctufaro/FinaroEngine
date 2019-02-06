using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace FinaroEngine.Library
{
    public class OrderProcess
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
                            row["PriceSort"] = tradeType == TradeType.Buy? price*-1: price;

                            bool updated;
                            DataTable updatedOrders = MatchOrders(row, dt, out updated);                         
                            
                            sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();

                            if (updated)
                                sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();

                            sda.Update(dt);

                            return JsonConvert.SerializeObject(new { data = updatedOrders });
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

        public static DataTable MatchOrders(DataRow newOrder, DataTable orderBook, out bool updated)
        {
            bool isUpdated = false;
            DataTable updatedOrders = orderBook.Clone();

            foreach (DataRow dr in orderBook.Rows)
            {

                int quant = Convert.ToInt32(dr["Quantity"]);
                int newQuant = Convert.ToInt32(newOrder["Quantity"]);
                isUpdated = true;

                //after each pass, lets check if quantity == 0, then it's filled, break
                if (newQuant == 0)
                {
                    newOrder["Status"] = (int)Status.Filled;
                    break;
                }

                //new order has same amount as the first row
                else if (newQuant == quant)
                {
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
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = newQuant - quant;
                    newOrder["Status"] = (int)Status.Partial;
                }

                //new order has less shares than the first row
                else if (newQuant < quant)
                {
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
            return updatedOrders;
        }
    }
}
