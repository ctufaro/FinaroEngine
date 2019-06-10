using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace FinaroEngine.Loader
{
    class Program
    {
        //static string sqlConnectionString = "";
        //static string twitterConsumerKey = "";
        //static string twitterConsumerSecret = "";
        //static string twitterAccessToken = "";
        //static string twitterAccessTokenSecret = "";
        //static IConfiguration config;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            string sqlConnectionString = config["SQLConnectionString"];
            string twitterConsumerKey = config["TwitterConsumerKey"];
            string twitterConsumerSecret = config["TwitterConsumerSecret"];
            string twitterAccessToken = config["TwitterAccessToken"];
            string twitterAccessTokenSecret = config["TwitterAccessTokenSecret"];

            //DoWorkPollingTask();
            //SearchTweets("litecoin");

            await Task.Run(() =>
            {
                LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
            }).ContinueWith((t) =>
            {
                Console.WriteLine("Completed");
            });

        }

        private static void LoadTrends(string sqlConnectionString, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
        {
            List<SqlParameter> dummy = new List<SqlParameter>();
            dummy.Add(new SqlParameter("@DUMMY", -1));
            FinaroEngine.Library.DBUtility.ExecuteQuery(sqlConnectionString, "spClearTrends", dummy);

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

        private static void SearchTweets(string tweet, string twitterConsumerKey, string twitterConsumerSecret, string twitterAccessToken, string twitterAccessTokenSecret)
        {
            var request = new RestRequest("1.1/search/tweets.json", Method.GET);
            request.AddQueryParameter("q", tweet);
            //request.AddQueryParameter("result_type", "popular");
            request.AddQueryParameter("count", "100");

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
            Console.WriteLine("Hello World!");
            dynamic stuff = JsonConvert.DeserializeObject(content);

            foreach (JObject item in stuff["statuses"])
            {
                Console.WriteLine(item["text"]);
            }


        }
        
        static void DoWorkPollingTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    // update the UI on the UI thread
                    //Trending();

                    // don't run again for at least 200 milliseconds
                    await Task.Delay(200);
                }
            });
        }
    }
}
