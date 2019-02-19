using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    interface IMarketData
    {
        MarketData GetMarketData(int userId, int entityId);
        void UpdateMarketData(MarketData marketData);
        void UpdateBidsAsks(int entityId);
    }

}
