using FinaroEngine.Library;
using System;
using System.Data;
using Xunit;

namespace FinaroEngine.xUnit
{

    public class OrderProcessTests
    {
        private Options opts = new Options {
            ConnectionString = @"Data Source=CHRIS\SQLEXPRESS;Initial Catalog=FinaroDB;persist security info=True; Integrated Security=SSPI;"
        };

        [Fact]
        public void Test()
        {
            OrderProcess orderProcess = new OrderProcess(opts, 1, 1);
            //var ret = orderProcess.AddNewOrder(TradeType.Sell, 123, 5);
            var market = orderProcess.GetMarketData();
            var orders = orderProcess.GetNewOrders();
            Assert.True(true);
        }

    }

}
