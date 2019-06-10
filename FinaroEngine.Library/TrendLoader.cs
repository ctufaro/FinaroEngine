using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FinaroEngine.Library
{
    public class TrendLoader
    {
        public static void LoadTrends(string sqlConnectionString, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
        {
            var request = new RestRequest("1.1/trends/place.json", Method.GET);
            request.AddQueryParameter("id", "23424977");
            request.AddQueryParameter("result_type", "popular");

            var client = new RestClient("https://api.twitter.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    twitterConsumerKey,
                    twitterConsumerSecret,
                    twitterAccessToken,
                    twitterAccessTokenSecret)
            };

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            dynamic stuff = JsonConvert.DeserializeObject(content);

            foreach (JObject item in stuff)
            {
                foreach (var trend in item["trends"])
                {
                    //Console.WriteLine($"Hello, Name:{trend["name"]} Url:{trend["url"]} Volume:{trend["tweet_volume"]}");
                    //"name": "#ChainedToTheRhythm", "url": "http://twitter.com/search?q=%23ChainedToTheRhythm", "promoted_content": null, "query": "%23ChainedToTheRhythm", "tweet_volume": 48857
                    //Console.WriteLine($"Name:{trend["name"]} Volume:{trend["tweet_volume"]}");

                    int volume = 0;
                    Int32.TryParse(trend["tweet_volume"].ToString(), out volume);
                    List<SqlParameter> sparams = new List<SqlParameter>();
                    sparams.Add(new SqlParameter("@NAME", trend["name"].ToString()));
                    sparams.Add(new SqlParameter("@URL", trend["url"].ToString()));
                    sparams.Add(new SqlParameter("@TWEETVOLUME", volume));
                    FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spInsertTrend", sparams);
                }
            }
        }

        public static void ClearTrends(string sqlConnectionString)
        {
            List<SqlParameter> dummy = new List<SqlParameter> { new SqlParameter("@DUMMY", -1) };
            FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spClearTrends", dummy);
        }
    }
}
