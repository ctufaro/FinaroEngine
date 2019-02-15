using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface IOrder
    {
        MarketOrders AddNewOrder(Order newOrder);

        IEnumerable<Order> GetOrders(int userId, int entityId);

        IEnumerable<Order> MatchOrders(int entityId, IEnumerable<Order> orderBook, Order newOrder);
    }
}
