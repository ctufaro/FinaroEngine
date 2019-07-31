using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FinaroEngine.Library
{
    public class Orders : OptInit<Options>, IOrder
    {
        private int userId;
        private int entityId;
        private Options opts;

        public Orders(Options opts, int userId)
                    : base(opts)
        {
            this.opts = opts;
            this.userId = userId;
        }

        public Orders(Options opts, int userId, int entityId)
            : base(opts)
        {

        }

        public async Task<(int ReturnCode, decimal? Balance)> InsertOrder(Order newOrder, Guid orderId, int status)
        {
            var dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spSelectUserBalance", new List<SqlParameter> { new SqlParameter("@USERID", this.userId) });
            var balance = Convert.ToDecimal(dt.Rows[0][0]);
            dt = await DBUtility.GetDataTableAsync(opts.ConnectionString, "spSelectUserShares", new List<SqlParameter> { new SqlParameter("@USERID", this.userId), new SqlParameter("@TRENDNAME", newOrder.TrendName) });
            var totalTrendShares = Convert.ToDecimal(dt.Rows[0][0]);

            decimal amount = newOrder.Price * newOrder.Quantity;
            decimal newBalance = (newOrder.TradeTypeId == 1) ? balance - amount : balance + amount;
            decimal newShareQuantity = 0;

            //// IM TRYING TO BUY/SELL ZERO SHARES
            if (newOrder.Quantity <= 0) return (1,null);

            //// IM TRYING TO BUY/SELL TRENDS WITH A PRICE OF ZERO
            if (newOrder.Price == 0) return (4, null);

            //// IM TRYING TO BUY STUFF
            if (newOrder.TradeTypeId == 1)
            {
                // I DONT HAVE ENOUGH FUNDS TO BUY SHARES
                if (newBalance < 0)
                    return (2,null);
                // I DO HAVE ENOUGH FUNDS TO BUY SHARES
                else
                {
                    newShareQuantity = totalTrendShares + newOrder.Quantity;
                }
            }

            //// IM TRYING TO SELL STUFF
            if (newOrder.TradeTypeId == 2)
            {
                // I DONT HAVE ANY OR ENOUGH SHARES THAT IM TRYING TO SELL
                if (totalTrendShares < newOrder.Quantity)
                    return (3,null);
                // I DO HAVE ENOUGH SHARES TO SELL
                else
                    newShareQuantity = totalTrendShares - newOrder.Quantity;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ORDERID", orderId));
            parameters.Add(new SqlParameter("@USERID", newOrder.UserId));
            parameters.Add(new SqlParameter("@TRENDID", newOrder.TrendId));
            parameters.Add(new SqlParameter("@TRENDNAME", newOrder.TrendName));
            parameters.Add(new SqlParameter("@TRADETYPEID", newOrder.TradeTypeId));
            parameters.Add(new SqlParameter("@ACTIVITYID", newOrder.TradeTypeId));
            parameters.Add(new SqlParameter("@PRICE", newOrder.Price));
            parameters.Add(new SqlParameter("@QUANTITY", newOrder.Quantity));
            parameters.Add(new SqlParameter("@TOTALAMOUNT", amount));
            parameters.Add(new SqlParameter("@NEWBALANCE", newBalance));
            parameters.Add(new SqlParameter("@STATUS", status));
            parameters.Add(new SqlParameter("@NEWQUANTITY", newShareQuantity));
            await DBUtility.ExecuteQueryAsync(opts.ConnectionString, "spInsertOrder", parameters);
            return (0,newBalance);
        }

        public MarketOrders AddNewOrder(Order newOrder, IContractCall contract)
        {
            Guid orderId = Guid.NewGuid();
            DBMarket dBMarket = new DBMarket(opts, this.userId, this.entityId);

            //BEFORE WE MATCH, LETS CHECK IF WE NEED TO MOVE MONEY TO MARGIN (SHORT SELLS)
            MarginTransfer(newOrder, this.userId, this.entityId, contract, orderId);

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
                            row["PublicKey"] = newOrder.PublicKey;
                            row["Status"] = (int)Status.Open;

                            bool updated;
                            MarketData marketData;
                            DataTable updatedOrders = MatchOrders(contract, entityId, row, dt, out updated, out marketData);

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
                order.TrendId = Convert.ToInt32(dr["TrendId"]);
                order.TradeTypeId = Convert.ToInt32(dr["TradeTypeId"]);
                order.Price = Convert.ToDecimal(dr["Price"]);
                order.Date = Convert.ToDateTime(dr["Date"]);
                order.Quantity = Convert.ToDecimal(dr["Quantity"]);
                order.Status = Convert.ToInt32(dr["Status"]);
                orders.Add(order);
            }
            return orders;            
        }

        public DataTable MatchOrders(IContractCall contract, int entityId, DataRow newOrder, DataTable orderBook, out bool updated, out MarketData marketData)
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

                //SAFETY CHECK, LETS CHECK IF QUANTITY == 0, THEN IT'S FILLED, BREAK
                if (newQuant == 0) { break; }

                //1. NEW ORDER HAS SAME AMOUNT AS THE FIRST ROW IN THE BOOK
                else if (newQuant == quant)
                {
                    var txResult = Transfer(newOrder, dr, contract, false);
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    dr["TxHash"] = txResult;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    newOrder["TxHash"] = txResult;
                    marketData.Volume = marketData.Volume + quant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                    break;
                }

                //2. NEW ORDER HAS MORE SHARES THAN THE FIRST ROW IN THE BOOK
                else if (newQuant > quant)
                {
                    var txResult = Transfer(newOrder, dr, contract, true);
                    dr["Quantity"] = 0;
                    dr["Status"] = (int)Status.Filled;
                    dr["TxHash"] = txResult;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = newQuant - quant;
                    newOrder["Status"] = (int)Status.Partial;
                    newOrder["TxHash"] = txResult;
                    marketData.Volume = marketData.Volume + quant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                }

                //3. NEW ORDER HAS LESS SHARES THAN THE FIRST ROW IN THE BOOK
                else if (newQuant < quant)
                {
                    var txResult = Transfer(newOrder, dr, contract, true);
                    dr["Quantity"] = quant - newQuant;
                    dr["Status"] = (int)Status.Partial;
                    dr["TxHash"] = txResult;
                    updatedOrders.ImportRow(dr);
                    newOrder["Quantity"] = 0;
                    newOrder["Status"] = (int)Status.Filled;
                    newOrder["TxHash"] = txResult;
                    marketData.Volume = marketData.Volume + newQuant;
                    marketData.MarketPrice = (price < newPrice) ? price : newPrice;
                    break;
                }
                
            }

            orderBook.Rows.Add(newOrder);
            updatedOrders.ImportRow(newOrder);
            return updatedOrders;
        }

        public void SetUnits(int userId, int entityId, int units)
        {
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@USERID", userId),
                new SqlParameter("@ENTITYID", entityId),
                new SqlParameter("@UNITS", units)
            };
            DBUtility.ExecuteQuery(opts.ConnectionString, "spUpdateUserUnits", prms);
        }

        public string Transfer(DataRow sideA, DataRow sideB, IContractCall contract, bool isPartial)
        {
            //SELLER SHOWS DECREASE IN UNITS AND INCREASE IN TOKEN BALANCE
            //BUYER SHOWS INCREASE IN UNITS AND DECREASE IN TOKEN BALANCE
            //TOKENS MOVED FROM BUYER ACCOUNT TO SELLER ACCOUNT

            //WHO IS THE BUYER/SELLER
            DataRow buyer = Convert.ToInt32(sideA["TradeTypeId"]) == (int)TradeType.Buy ? sideA : sideB;
            DataRow seller = Convert.ToInt32(sideA["TradeTypeId"]) == (int)TradeType.Sell ? sideA : sideB;

            int buyerQuantity = Convert.ToInt32(buyer["UnsetQuantity"]);
            int sellerQuantity = Convert.ToInt32(seller["UnsetQuantity"]);

            if (isPartial)
            {
                //IF SELLER IS SELLING MORE THAN ASKING, SELLER QUANTITY IS THE BUYERS QUANTITY
                if (buyerQuantity < sellerQuantity)
                {
                    sellerQuantity = buyerQuantity;
                }
                //IF BUYER IS BUYING MORE THAN ASKING, BUYER QUANTITY IS THE SELLERS QUANTITY
                else if (buyerQuantity > sellerQuantity)
                {
                    buyerQuantity = sellerQuantity;
                }
            }

            string buyerPK = Convert.ToString(buyer["PublicKey"]);
            int buyerUserId = Convert.ToInt32(buyer["UserId"]);
            int buyerEntityId = Convert.ToInt32(buyer["EntityId"]);
            double buyerAmount = Convert.ToDouble(buyer["Price"]) * buyerQuantity;

            string sellerPK = Convert.ToString(seller["PublicKey"]);
            int sellerUserId = Convert.ToInt32(seller["UserId"]);
            int sellerEntityId = Convert.ToInt32(seller["EntityId"]);

            Task<string> txResult = contract.SendTokensFromAsync(buyerPK, sellerPK, buyerAmount, opts.GasAmount);
            txResult.Wait();
            var result = txResult.Result;

            SetUnits(buyerUserId, buyerEntityId, buyerQuantity);
            SetUnits(sellerUserId, sellerEntityId, sellerQuantity * -1);            

            return result;
        }

        public void MarginTransfer(Order newOrder, int userId, int entityId, IContractCall contract, Guid orderId)
        {
            //SHORT SELL: FOR EVERY 1 UNIT LETS TRANSFER $100 INTO THEIR MARGIN ACCOUNT
            if(newOrder.TradeTypeId == (int)TradeType.ShortSell)
            {
                double transferAmount = Convert.ToDouble(newOrder.Quantity) * 100D;
                Task<string> txResult = contract.TransferMargin(newOrder.PublicKey, newOrder.PublicKey, transferAmount, (int)MarginMove.BalanceToMargin, opts.GasAmount);
                txResult.Wait();
                var result = txResult.Result;
                //INSERT INTO DATABASE
                List<SqlParameter> prms = new List<SqlParameter>
                {
                    new SqlParameter("@ORDERID", orderId),
                    new SqlParameter("@USERID", userId),
                    new SqlParameter("@PUBLICKEY", newOrder.PublicKey),
                    new SqlParameter("@ENTITYID", entityId),
                    new SqlParameter("@TRADETYPEID", newOrder.TradeTypeId),
                    new SqlParameter("@PRICE", newOrder.Price),
                    new SqlParameter("@QUANTITY", newOrder.Quantity),
                    new SqlParameter("@TXHASH", result),
                };
                DBUtility.ExecuteQuery(opts.ConnectionString, "spInsertMargin", prms);
            }
        }
    }
}
