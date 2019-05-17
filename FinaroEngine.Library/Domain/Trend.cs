using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class Trend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int TweetVolume { get; set; }
        public DateTime LoadDate { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public decimal[] Prices { get; set; }
        public string Color { get; set; }
        public string CSS { get; set; }
        public string[] Gradient { get; set; }
        public bool Notify { get; set; }
        public bool Faved { get; set; }


    }
}
