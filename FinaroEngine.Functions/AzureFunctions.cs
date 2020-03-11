using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using System.Net;

namespace FinaroEngine.Functions
{
    public static class AzureFunctions
    {
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
            var result = await client.DownloadStringTaskAsync(URL.ToString());
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
            var result = await client.DownloadStringTaskAsync(URL.ToString());
            return new OkObjectResult(result);
        }

        public static (string url, string key) GetCreds(bool useProduction)
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
