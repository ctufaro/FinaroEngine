using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace FinaroEngine.Library
{
    public class OrderProcess
    {
        public static void ProcessNewOrder(int userId, int entityId, TradeType tradeType, decimal price, int quantity)
        {
            Guid orderId = Guid.NewGuid();
            string constring = @"Data Source=CHRIS\SQLEXPRESS;Initial Catalog=FinaroDB;persist security info=True; Integrated Security=SSPI;";
            using (SqlConnection con = new SqlConnection(constring))
            {
                TradeType searchType = TradeType.Sell;
                string priceSort = "";
                if (tradeType == TradeType.Buy)
                { 
                    searchType = TradeType.Sell;
                    priceSort = "<= " + price;
                }
                else if (tradeType == TradeType.Sell)
                { 
                    searchType = TradeType.Buy;
                    priceSort = ">= " + price;
                }

                string stringSelect = "SELECT * FROM ORDERS WHERE EntityId = {0} AND TradeTypeId = {1} AND Status < 3 AND PRICE {2} AND OrderId <> '{3}' ORDER BY [Date]";
                string selectCmd = string.Format(stringSelect, entityId, (int)searchType, priceSort, orderId);
                using (SqlCommand cmd = new SqlCommand(selectCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
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
                            dt.Rows.Add(row);

                            //Try to fill or partial

                            if(dt.Rows.Count > 0)
                            {

                            }


                            //dt.Rows[0]["Quantity"] = 5466;
                            //dt.Rows[0]["Status"] = 2;
                            sda.InsertCommand = new SqlCommandBuilder(sda).GetInsertCommand();
                            //sda.UpdateCommand = new SqlCommandBuilder(sda).GetUpdateCommand();
                            sda.Update(dt);

                            //dt.AcceptChanges();
                        }
                    }
                }
            }
        }

        
    }
}
