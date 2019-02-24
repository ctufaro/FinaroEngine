using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class DBTradeHistory : OptInit<Options>, ITradeHistory
    {
        private int userId;
        private int entityId;
        private Options opts;

        public DBTradeHistory(Options opts, int userId, int entityId)
            : base(opts)
        {
            this.opts = opts;
            this.userId = userId;
            this.entityId = entityId;
        }

        public IEnumerable<Order> GetTradeHistory(int userId, int entityId)
        {
            List<Order> orders = new List<Order>();
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@ENTITYID", entityId),
                new SqlParameter("@USERID", userId)
            };
            DataTable marketTable = DBUtility.GetDataTable(opts.ConnectionString, "spSelectTradeHistory", prms);
            foreach (DataRow dr in marketTable.Rows)
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
                order.UnsetQuantity = Convert.ToInt32(dr["UnsetQuantity"]);
                order.Status = Convert.ToInt32(dr["Status"]);
                orders.Add(order);
            }
            return orders;
        }
    }
}
