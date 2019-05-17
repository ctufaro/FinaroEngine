using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class MarketData
    {
        public int EntityId { get; set; }
        public int? Volume { get; set; }
        public DateTime? LastTradeTime { get; set; }
        public decimal? LastTradePrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? ChangeInPrice { get; set; }
        public decimal? CurrentBid { get; set; }
        public decimal? CurrentAsk { get; set; }
    }
}
