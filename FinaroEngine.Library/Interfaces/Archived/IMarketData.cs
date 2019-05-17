using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    interface IMarketData
    {
        MarketData GetMarketData(int userId, int entityId);
        MarketData UpdateMarketData(MarketData marketData);
    }

}
