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
using System.Net;

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

        [FunctionName("getUserTrendShares")]
        public static async Task<IActionResult> GetUserTrendShares([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "trends/user/count/{userId}/{trendName}")]HttpRequest req, ILogger log, int userId, string trendName)
        {
            log.LogInformation("Getting User Trend Shares");
            Trends trends = new Trends(GetOptions());
            return new OkObjectResult(await trends.GetUserTrendShares(userId, trendName));
        }

        [FunctionName("userSignup")]
        public static async Task<IActionResult> SignUpUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "signup/user")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Signing Up User");
            string response = new StreamReader(req.Body).ReadToEnd();//int userId, string trendName
            var resp = JsonConvert.DeserializeAnonymousType(response, new { email = "", username = "", password = "", mobile = "", publicKey = "", privateKey = "" });            
            Options opts = GetOptions();
            Users users = new Users(opts);
            try
            {
                bool exists = await users.EmailExists(resp.email);
                // EMAIL DOESNT EXIST, SIGN THEM UP
                if (!exists)
                {
                    var user = await users.SignUpUser(resp.email, resp.username, resp.password, resp.mobile, resp.publicKey, resp.privateKey);
                    return new OkObjectResult(user);
                }
                // EMAIL EXISTS
                else
                {                    
                    return new ObjectResult(new { title = "Signup Error", message = "This email address has already been registered." })
                    {
                        StatusCode = 500
                    };
                }                
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("userLogin")]
        public static async Task<IActionResult> UserLogin([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login/user")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Logging In User");
            string response = new StreamReader(req.Body).ReadToEnd();//int userId, string trendName
            var resp = JsonConvert.DeserializeAnonymousType(response, new { username = "", password = ""});
            Options opts = GetOptions();
            Users users = new Users(opts);
            try
            {
                var user = await users.LoginUser(resp.username, resp.password);
                if (user!=null)
                { 
                    return new OkObjectResult(user);
                }
                else
                {
                    // BAD CREDENTIALS
                    return new ObjectResult(new { title = "Login Error", message = "Incorrect Username/Password" }) {
                        StatusCode = 500
                    };
                }
            }
            catch
            {
                return new BadRequestResult();
            }
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

        [FunctionName("insertOrder")]
        public static async Task<IActionResult> InsertOrder([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders/new")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating Order");
            var neworder = new JsonSerializer().Deserialize<Order>(new JsonTextReader(new StreamReader(req.Body)));
            Options opts = GetOptions();
            Orders orders = new Orders(opts, neworder.UserId);
            try
            {
                var retval = await orders.InsertOrder(neworder, Guid.NewGuid(), (int)Status.Filled);
                if (retval.ReturnCode == 0)
                {
                    return new OkObjectResult(retval.Balance);
                }
                else if (retval.ReturnCode == 1)
                {
                    return Utility.APIError("Uh-oh!", "Invalid number of shares specified");
                }
                else if (retval.ReturnCode == 2)
                {
                    return Utility.APIError("Uh-oh!", "Not enough funds to place this order");
                }
                else if (retval.ReturnCode == 3)
                {
                    return Utility.APIError("Uh-oh!", "You do no own enough shares of the this trend");
                }
                else if (retval.ReturnCode == 4)
                {
                    return Utility.APIError("Uh-oh!", "You cannot buy/sell trends with a price of zero. Trend may no longer be trending.");
                }
                else
                {
                    return Utility.APIError("Uh-oh!", "Something Bad Happened! We're on it..");
                }
            }
            catch(Exception e)
            {
                return Utility.APIError("Uh-oh!", "Something Bad Happened! We're on it..");
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
        
        [FunctionName("getPriceVol")]
        public static async Task<IActionResult> GetPriceVol([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pricevol/{name}")]HttpRequest req, ILogger log, string name)
        {
            log.LogInformation("Getting Price & Volume");
            PriceVols priceVols= new PriceVols(GetOptions());
            name = HttpUtility.HtmlDecode(name);
            return new OkObjectResult(priceVols.GetPriceVolJSON(name));
        }

        [FunctionName("getCoinListData")]
        public static async Task<IActionResult> GetCoinListData([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "coinlistdata")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting Coin Listing Data");
            bool useProduction = bool.Parse(Environment.GetEnvironmentVariable("CoinProduction"));
            var creds = GetCreds(useProduction);
            var URL = new UriBuilder($"https://{creds.url}/v1/cryptocurrency/quotes/latest");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["slug"] = "bitcoin,ethereum,electroneum,tomochain";
            URL.Query = queryString.ToString();
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", creds.key);
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            return new OkObjectResult(result);
        }

        [FunctionName("getCoinMarketData")]
        public static async Task<IActionResult> GetCoinMarketData([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "coinmarketdata")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting Coin Market Data");
            bool useProduction = bool.Parse(Environment.GetEnvironmentVariable("CoinProduction"));
            var creds = GetCreds(useProduction);
            var URL = new UriBuilder($"https://{creds.url}/v1/global-metrics/quotes/latest");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["convert"] = "USD";
            URL.Query = queryString.ToString();
            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", creds.key);
            client.Headers.Add("Accepts", "application/json");
            var result = client.DownloadString(URL.ToString());
            return new OkObjectResult(result);
        }

        [FunctionName("sendEmail")]
        public static OkObjectResult SendEmail([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "email")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Sending Email");
            string res = new StreamReader(req.Body).ReadToEnd();
            var response = JsonConvert.DeserializeAnonymousType(res, new { Email = "", Subject = "", Message = "" });

            if (string.IsNullOrEmpty(response.Email))
                return new OkObjectResult("Empty Email Address");

            Task<string> result = Email.SendAsync(Environment.GetEnvironmentVariable("EmailAPIKey"),
                                                Environment.GetEnvironmentVariable("EmailRecipients"), response.Email, response.Subject, response.Message);
            result.Wait();
            return new OkObjectResult(result.Result);
        }

        //[FunctionName("loadTrends")]
        //public static void LoadTrends([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        //{
        //    string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
        //    string twitterConsumerKey = Environment.GetEnvironmentVariable("TwitterConsumerKey");
        //    string twitterConsumerSecret = Environment.GetEnvironmentVariable("TwitterConsumerSecret");
        //    string twitterAccessToken = Environment.GetEnvironmentVariable("TwitterAccessToken");
        //    string twitterAccessTokenSecret = Environment.GetEnvironmentVariable("TwitterAccessTokenSecret");

        //    TrendLibrary.LoadTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
        //    //TrendLibrary.LoadUserTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
        //    log.LogInformation($"C# LoadTrends Timer trigger function executed at: {DateTime.Now}");
        //}

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
            //TrendLibrary.LoadUserTrends(sqlConnectionString, twitterConsumerKey, twitterConsumerSecret, twitterAccessToken, twitterAccessTokenSecret, err => log.LogInformation(err));
            log.LogInformation($"C# LoadTrends HTTP trigger function executed at: {DateTime.Now}");
        }

        [FunctionName("clearTrendsDemand")]
        public static void ClearTrendsManually([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "utils/trends/clear")]HttpRequest req, ILogger log)
        {
            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            TrendLibrary.ClearTrends(sqlConnectionString);
            log.LogInformation($"C# ClearTrends HTTP trigger function executed at: {DateTime.Now}");
        }

        [FunctionName("clearOrdersDemand")]
        public static void ClearOrdersManually([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "utils/orders/clear")]HttpRequest req, ILogger log)
        {
            string sqlConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
            TrendLibrary.ClearOrders(sqlConnectionString);
            log.LogInformation($"C# ClearOrders HTTP trigger function executed at: {DateTime.Now}");
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

        public static(string url, string key) GetCreds(bool useProduction)
        {
            if (useProduction)
            {
                return (Environment.GetEnvironmentVariable("ProductionAPI"), Environment.GetEnvironmentVariable("ProductionKey"));
            }
            else
            {
                return (Environment.GetEnvironmentVariable("SandboxAPI"), Environment.GetEnvironmentVariable("SandboxKey"));
            }
        }
    }
}
