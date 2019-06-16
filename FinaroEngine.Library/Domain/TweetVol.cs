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
        public List<int> TweetVolume { get; set; }
        public List<string> LoadDate { get; set; }
        public bool UserEntry { get; set; }
        public List<double?> AvgSentiment { get; set; }
    }
}
