using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EmployeeDetailsExtract.BusinessTier;

namespace EmployeeDetailsExtract
{
    public static class HttpTrigger
    {
        [FunctionName("EmployeeDetailExtract")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            await EmployeeBO.CreateCSVAsync();
            log.LogInformation("C# HTTP trigger function processed a request.");
            return (ActionResult)new OkObjectResult("{}");
        }
    }
}
