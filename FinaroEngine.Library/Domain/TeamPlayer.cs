using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? CurrentBid { get; set; }
        public decimal? CurrentAsk { get; set; }
        public decimal? LastPrice { get; set; }
    }
}
