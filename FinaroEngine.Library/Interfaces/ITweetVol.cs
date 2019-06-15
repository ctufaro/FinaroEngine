using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public interface ITweetVol
    {
        List<TweetVol> GetTweetVol(string name);
        string GetTweetVolJSON(string name);
    }
}
