using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FinaroEngine.Library.Domain;

namespace FinaroEngine.Functions
{
    public static class VueFx
    {
        [FunctionName("insertregister")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Register data = JsonConvert.DeserializeObject<Register>(requestBody);
            Register register = new Register(Environment.GetEnvironmentVariable("SQLConnectionString"));
            await register.InsertRegisterAsync(data);
            return new OkObjectResult(data);
        }
    }
}
