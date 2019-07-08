using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class PriceVol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public decimal[] Prices { get; set; }
        public string[] Times { get; set; }
    }
}
