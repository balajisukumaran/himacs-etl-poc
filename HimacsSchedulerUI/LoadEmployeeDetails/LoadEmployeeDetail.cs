using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LoadEmployeeDetails.CSVProfiler;
using LoadEmployeeDetails.Blob;
using System.Data;
using LoadEmployeeDetails.DataTier;


namespace LoadEmployeeDetails
{
    public static class LoadEmployeeDetail
    {
        [FunctionName("LoadEmployeeDetail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                //getting the http request details
                string container = req.Query["container"];
                //string directory = req.Query["directory"];
                string filename = req.Query["filename"];
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                //directory = directory ?? data?.directory;
                filename = filename ?? data?.filename;
                container = container ?? data?.container;
                //download and parse the input file
                StreamReader sr = await BlobContainer.download_FromBlobAsync(filename, container);
                DataTable dt = CsvProperties.ConvertCSVtoDataTable(sr);
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                //load the file to persistance 
                LoadEmployeeDO.LoadEmployeeData(ds.GetXml());
            }
            catch (Exception e)
            {
                return (ActionResult)new OkObjectResult("{ \"status\":\"failed\" }");
            }
            return (ActionResult)new OkObjectResult("{ \"status\":\"passed\" }");
        }
    }
}
