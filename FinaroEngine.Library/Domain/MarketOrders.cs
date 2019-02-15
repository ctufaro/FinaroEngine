using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class MarketOrders
    {
        public MarketData MarketData { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
