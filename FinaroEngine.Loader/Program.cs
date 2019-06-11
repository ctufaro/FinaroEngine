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
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            string sqlConnectionString = config["SQLConnectionString"];
            string twitterConsumerKey = config["TwitterConsumerKey"];
            string twitterConsumerSecret = config["TwitterConsumerSecret"];
            string twitterAccessToken = config["TwitterAccessToken"];
            string twitterAccessTokenSecret = config["TwitterAccessTokenSecret"];
            string endPoint = config["TxtAnalyEndPoint"];
            string key1 = config["TxtAnalyKey"];

            //var tweets = await TrendLibrary.GetTweetsAsync("#TrumpsWorseThanNixon", 3, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
            //var retval = await TrendLibrary.GetSentimentAvgAsync(endPoint, key1, tweets);
            //Console.WriteLine($"Average: {retval.Value}");
            Console.ReadLine();
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
