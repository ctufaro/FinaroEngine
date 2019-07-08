using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FinaroEngine.Library;
using System.Web;

namespace FinaroEngine.Functions
{
    public static class TrndxFunc
    {
        /// <summary>
        /// /api/trends
        /// </summary>

        [FunctionName("getTrends")]
        public static async Task<IActionResult> GetTrends([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "trends/filter/{filterId}")]HttpRequest req, ILogger log, int filterId)
        {
            log.LogInformation("Getting all Trends");            
            Trends trends = new Trends(GetOptions());
            return new OkObjectResult(trends.GetTrendsJSON(filterId));
        }

        [FunctionName("insertUserTrend")]
        public static async Task<IActionResult> InsertUserTrend([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "trends/user")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Inserting User Trend");
            string response = new StreamReader(req.Body).ReadToEnd();//int userId, string trendName
            var resp = JsonConvert.DeserializeAnonymousType(response, new { UserId = 0, TrendName = "" });
            Options opts = GetOptions();
            Trends trends = new Trends(GetOptions());
            try
            {
                await trends.InsertUserTrend(resp.UserId, resp.TrendName);
                return new OkResult();
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("getTweetVol")]
        public static async Task<IActionResult> GetTweetVol([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tweets/{name}")]HttpRequest req, ILogger log, string name)
        {
            log.LogInformation("Getting Tweet Volume");
            TweetVols tweetVols = new TweetVols(GetOptions());
            name = HttpUtility.HtmlDecode(name);
            return new OkObjectResult(tweetVols.GetTweetVolJSON(name));
        }


        [FunctionName("loadTrends")]
        public static void LoadTrends([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            string twitterConsumerKey = Environment.GetEnvironmentVariable("TwitterConsumerKey");
            string twitterConsumerSecret = Environment.GetEnvironmentVariable("TwitterConsumerSecret");
            string twitterAccessToken = Environment.GetEnvironmentVariable("TwitterAccessToken");
            string twitterAccessTokenSecret = Environment.GetEnvironmentVariable("TwitterAccessTokenSecret");

            TrendLibrary.LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
            TrendLibrary.LoadUserTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
            log.LogInformation($"C# LoadTrends Timer trigger function executed at: {DateTime.Now}");
        }

        //[FunctionName("clearTrends")]
        //public static void ClearTrends([TimerTrigger("0 0 */24 * * *")]TimerInfo myTimer, ILogger log)
        //{
        //    string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
        //    TrendLibrary.ClearTrends(sqlConnectionString);
        //    log.LogInformation($"C# ClearTrends Timer trigger function executed at: {DateTime.Now}");
        //}

        [FunctionName("loadTrendsDemand")]
        public static void LoadTrendsManually([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "utils/trends/load")]HttpRequest req, ILogger log)
        {
            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            string twitterConsumerKey = Environment.GetEnvironmentVariable("TwitterConsumerKey");
            string twitterConsumerSecret = Environment.GetEnvironmentVariable("TwitterConsumerSecret");
            string twitterAccessToken = Environment.GetEnvironmentVariable("TwitterAccessToken");
            string twitterAccessTokenSecret = Environment.GetEnvironmentVariable("TwitterAccessTokenSecret");

            TrendLibrary.LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
            TrendLibrary.LoadUserTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
            log.LogInformation($"C# LoadTrends HTTP trigger function executed at: {DateTime.Now}");
        }

        [FunctionName("clearTrendsDemand")]
        public static void ClearTrendsManually([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "utils/trends/clear")]HttpRequest req, ILogger log)
        {
            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            TrendLibrary.ClearTrends(sqlConnectionString);
            log.LogInformation($"C# ClearTrends HTTP trigger function executed at: {DateTime.Now}");
        }

        public static Options GetOptions()
        {
            Options opts = new Options();
            opts.ConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            opts.ABI = Environment.GetEnvironmentVariable("ContractABI");
            opts.URL = Environment.GetEnvironmentVariable("ContractURL");
            opts.ContractAddress = Environment.GetEnvironmentVariable("ContractAddress");
            opts.SigningKey = Environment.GetEnvironmentVariable("ContractSigningKey");
            opts.GasAmount = Convert.ToInt32(Environment.GetEnvironmentVariable("GasAmount"));
            return opts;
        }
    }
}
