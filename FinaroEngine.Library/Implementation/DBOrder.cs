using System;
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
            //not easy
            throw new NotImplementedException();
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
                order.Price = Convert.ToInt32(dr["Price"]);
                order.Date = Convert.ToDateTime(dr["Date"]);
                order.Quantity = Convert.ToInt32(dr["Quantity"]);
                order.Status = Convert.ToInt32(dr["Status"]);
                orders.Add(order);
            }
            return orders;            
        }

        public IEnumerable<Order> MatchOrders(int entityId, IEnumerable<Order> orderBook, Order newOrder)
        {
            //not easy
            throw new NotImplementedException();
        }
    }
}
