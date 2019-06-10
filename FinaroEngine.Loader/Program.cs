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
using FinaroEngine.Library;

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
                TrendLoader.LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
            }).ContinueWith((t) =>
            {
                Console.WriteLine("Completed");
            });

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
