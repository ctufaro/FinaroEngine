using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface IOrder
    {
        MarketOrders AddNewOrder(Order newOrder, IContractCall contract);

        IEnumerable<Order> GetOrders(int userId, int entityId);

        void SetUnits(int userId, int entityId, int units);

        //IEnumerable<Order> MatchOrders(int entityId, IEnumerable<Order> orderBook, Order newOrder);
    }
}
