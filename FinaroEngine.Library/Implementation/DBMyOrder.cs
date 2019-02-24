using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class DBMyOrder : OptInit<Options>, IMyOrder
    {
        private int userId;
        private Options opts;

        public DBMyOrder(Options opts, int userId)
            : base(opts)
        {
            this.opts = opts;
            this.userId = userId;
        }

        public IEnumerable<MyOrder> GetMyOrder(int userId)
        {
            List<MyOrder> myOrders = new List<MyOrder>();
            List<SqlParameter> prms = new List<SqlParameter> {
                new SqlParameter("@USERID", userId)
            };
            DataTable marketTable = DBUtility.GetDataTable(opts.ConnectionString, "spSelectMyOrders", prms);
            foreach (DataRow dr in marketTable.Rows)
            {
                MyOrder myOrder = new MyOrder();
                myOrder.Id = Convert.ToInt32(dr["Id"]);
                myOrder.OrderId = Guid.Parse(dr["OrderId"].ToString());
                myOrder.UserId = Convert.ToInt32(dr["UserId"]);
                myOrder.Name = Convert.ToString(dr["Name"]);
                myOrder.EntityId = Convert.ToInt32(dr["EntityId"]);
                myOrder.TradeTypeId = Convert.ToInt32(dr["TradeTypeId"]);
                myOrder.Price = Convert.ToDecimal(dr["Price"]);
                myOrder.Date = Convert.ToDateTime(dr["Date"]);
                myOrder.Quantity = Convert.ToInt32(dr["Quantity"]);
                myOrder.UnsetQuantity = Convert.ToInt32(dr["UnsetQuantity"]);
                myOrder.Status = Convert.ToInt32(dr["Status"]);
                myOrders.Add(myOrder);
            }
            return myOrders;
        }

    }
}
