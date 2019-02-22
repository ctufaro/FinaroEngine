using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface ITradeHistory
    {
        IEnumerable<Order> GetTradeHistory(int userId, int entityId);
    }
}
