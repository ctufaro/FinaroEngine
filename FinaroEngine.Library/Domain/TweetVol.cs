using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class TweetVol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int TweetVolume { get; set; }
        public DateTime LoadDate { get; set; }
        public bool UserEntry { get; set; }
        public double? AvgSentiment { get; set; }
    }
}
