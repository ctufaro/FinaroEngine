using FinaroEngine.Library;
using System;
using System.Data;
using Xunit;

namespace FinaroEngine.xUnit
{

    public class OrderBookTests
    {
        [Fact]
        public void Test_Orders_On_Fill_And_Partial()
        {
            bool update;
            DataTable dt = GetDT(new Tuple<int, int>[] {Tuple.Create(100, 1)});
            DataRow dr = GetDR(dt, 10, 1);

            //OrderProcess.MatchOrders(dr, dt, out update);

            Assert.True(Convert.ToInt32(dt.Rows[0]["Quantity"]) == 90);
            Assert.True(Convert.ToInt32(dt.Rows[0]["Status"]) == 2);

            Assert.True(Convert.ToInt32(dr["Quantity"]) == 0);
            Assert.True(Convert.ToInt32(dr["Status"]) == 3);
        }

        [Fact]
        public void Test_Orders_On_Two_Fills()
        {
            bool update;
            DataTable dt = GetDT(new Tuple<int, int>[] { Tuple.Create(100, 1) });
            DataRow dr = GetDR(dt, 100, 1);
            //OrderProcess.MatchOrders(dr, dt, out update);

            Assert.True(Convert.ToInt32(dt.Rows[0]["Quantity"]) == 0);
            Assert.True(Convert.ToInt32(dt.Rows[0]["Status"]) == 3);

            Assert.True(Convert.ToInt32(dr["Quantity"]) == 0);
            Assert.True(Convert.ToInt32(dr["Status"]) == 3);
        }

        public DataTable GetDT(Tuple<int, int>[] values)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Quantity", typeof(Int32));
            dt.Columns.Add("Status", typeof(Int32));
            foreach(Tuple<int,int> v in values)
            {
                dt.Rows.Add(v.Item1, v.Item2);
            }
            return dt;
        }
        public DataRow GetDR(DataTable dt, int quantity, int status)
        {
            DataRow dr = dt.NewRow();
            dr["Quantity"] = quantity;
            dr["Status"] = status;
            return dr;
        }
    }

}
