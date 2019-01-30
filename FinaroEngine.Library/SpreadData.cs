using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class SpreadData
    {
        public string Date { get; set; }
        public string TeamA { get; set; }
        public string SpreadA { get; set; }
        public string TeamB { get; set; }
        public string SpreadB { get; set; }
    }

    public class SpreadDataAll
    {
        public List<SpreadData> Data { get; set; }

        public SpreadDataAll()
        {
            Data = new List<SpreadData>();
        }
    }
}
