using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class DBOrder : OptInit<Options>, IOrder
    {
        private int userId;
        private int entityId;
        private Options opts;

        public DBOrder(Options opts, int userId, int entityId)
                    : base(opts)
        {
            this.opts = opts;
            this.userId = userId;
            this.entityId = entityId;
        }

        public MarketOrders AddNewOrder(Order newOrder)
        {
            Guid orderId = Guid.NewGuid();
            DBMarket dBMarket = new DBMarket(opts, this.userId, this.entityId);

            using (SqlConnection con = new SqlConnection(opts.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectOrdersForMatch", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ENTITYID", entityId));
                    cmd.Parameters.Add(new SqlParameter("@TRADETYPEID", newOrder.TradeTypeId));
                    cmd.Parameters.Add(new SqlParameter("@PRICE", newOrder.Price));
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            DataRow row = dt.NewRow();
                            row["OrderId"] = orderId;
                            row["UserId"] = userId;
                            row["EntityId"] = entityId;
                            row["TradeTypeId"] = newOrder.TradeTypeId;
                            row["Price"] = newOrder.Price;
                            row["Date"] = DateTime.Now;
                            row["Quantity"] = newOrder.Quantity;
                            row["UnsetQuantity"] = newOrder.UnsetQuantity;
                            row["Status"] = (int)Status.Open;

                            bool updated;
                            MarketData marketData;
                            DataTable updatedOrders = MatchOrders(entityId, row, dt, out updated, out marketData);

                            //NEW ORDERS (SOME MAY ALREADY HAVE MATCHES)
                            sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();

                            //ORDERS NEED TO BE UPDATED
                            if (updated)
                            {
                                sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();
                            }                            

                            //RUNNING NEW ORDERS AND UPDATES AGAINST DB
                            sda.Update(dt);

                            //UPDATE THE MARKET DATA DATABASE TABLE, RETURNING CHANGE IN PRICE + VOLUME
                            marketData = dBMarket.UpdateMarketData(marketData);

                            //MARKET DATA AND FINAL ORDERBOOK RETURN
                            return new MarketOrders { MarketData = marketData, Orders = updatedOrders };
                        }
                    }
                }
            }

        }

        public IEnumerable<Order> GetOrders(int userId, int entityId)
        {
            List<Order> orders = new List<Order>();
            List<SqlParameter> prms = new List<SqlParameter> { new SqlParameter("@ENTITYID", entityId) };
            DataTable marketTable = DBUtility.GetDataTable(opts.ConnectionString, "spSelectOrders", prms);
            foreach(DataRow dr in marketTable.Rows)
            {
                Order order = new Order();
                order.Id = Convert.ToInt32(dr["Id"]);                
                order.OrderId = Guid.Parse(dr["OrderId"].ToString());
                order.UserId = Convert.ToInt32(dr["UserId"]);
                order.EntityId = Convert.ToInt32(dr["EntityId"]);
                order.TradeTypeId = Convert.ToInt32(dr["TradeTypeId"]);
                order.Price = Convert.ToDecimal(dr["Price"]);
                order.Date = Convert.ToDateTime(dr["Date"]);
                order.Quantity = Convert.ToInt32(dr["Quantity"]);
                order.Status = Convert.ToInt32(dr["Status"]);
                orders.Add(order);
            }
            return orders;            
        }

        public static DataTable MatchOrders(int entityId, DataRow newOrder, DataTable orderBook, out bool updated, out MarketData marketData)
        {
            updated = false;
            DataTable updatedOrders = orderBook.Clone();
            marketData = new MarketData();
            marketData.EntityId = entityId;
            marketData.Volume = 0;
            marketData.MarketPrice = null;         

            foreach (DataRow dr in orderBook.Rows)
            {

                int quant = Convert.ToInt32(dr["Quantity"]);
                int newQuant = Convert.ToInt32(newOrder["Quantity"]);
                decimal price = Convert.ToDecimal(dr["Price"]);
                decimal newPrice = Convert.ToDecimal(newOrder["Price"]);
                marketData.LastTradeTime = Convert.ToDateTime(newOrder["Date"]);
                marketData.LastTradePrice = Convert.ToDecimal(newOrder["Price"]);
                updated = true;

                //AFTER EACH PASS, LETS CHECK IF QUANTITY == 0, THEN IT'S FILLED, BREAK
                if (newQuant == 0)
                {
                    newOrder["Status"] = (int)Status.Filled;
                    break;
                }

                //NEW ORDER HAS SAME AMOUNT AS THE FIRST ROW IN THE BOOK
                else if (newQuant == quant)
                {                    
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    marketData.Volume = marketData.Volume + quant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                    break;
                }

                //NEW ORDER HAS MORE SHARES THAN THE FIRST ROW IN THE BOOK
                else if (newQuant > quant)
                {                    
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = newQuant - quant;
                    newOrder["Status"] = (int)Status.Partial;
                    marketData.Volume = marketData.Volume + quant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                }

                //NEW ORDER HAS LESS SHARES THAN THE FIRST ROW IN THE BOOK
                else if (newQuant < quant)
                {                    
                    dr["Quantity"] = quant - newQuant;
                    dr["Status"] = (int)Status.Partial;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    marketData.Volume = marketData.Volume + newQuant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                    break;
                }

            }

            orderBook.Rows.Add(newOrder);
            updatedOrders.ImportRow(newOrder);
            return updatedOrders;
        }

    }
}
