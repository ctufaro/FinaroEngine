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

namespace FinaroEngine.Functions
{
    public static class TrndxFunc
    {
        /// <summary>
        /// /api/trends
        /// </summary>

        [FunctionName("getTrends")]
        public static async Task<IActionResult> GetTrends([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "trends")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting all Trends");            
            Trends trends = new Trends(GetOptions());
            return new OkObjectResult(trends.GetTrendsJSON());
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
