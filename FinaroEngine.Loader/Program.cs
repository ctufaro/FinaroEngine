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

            //while (true)
            //{
            //    Console.Write("Enter Text: ");
            //    string q = Console.ReadLine();
            //    var tweets = await TrendLibrary.GetTweetsAsync(q, 10, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret);
            //    var retval = await TrendLibrary.GetVaderSentAvgAsync(tweets);
            //    Console.WriteLine($"({q}) Score: {retval}");                
            //}

            TrendLibrary.LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, (err) => Console.WriteLine(err));
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
