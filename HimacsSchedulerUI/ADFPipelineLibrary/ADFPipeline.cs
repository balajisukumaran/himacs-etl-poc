using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.Services.AppAuthentication;

namespace ADFPipelineLibrary
{
    public class ADFPipeline
    {
        #region :: PIPELINE VARIABLES ::
        string tenantID ;
        string applicationId ;
        string authenticationKey ;
        string subscriptionId ;
        string resourceGroup ;
        string dataFactoryName ;
        string pipelineName ;
        DataFactoryManagementClient client;
        CreateRunResponse runResponse;
        #endregion

        public ADFPipeline(string tenantID,string applicationId, string authenticationKey, string subscriptionId, string resourceGroup, string dataFactoryName, string pipelineName)
        {
            this.tenantID = tenantID;
            this.applicationId = applicationId;
            this.authenticationKey = authenticationKey;
            this.subscriptionId = subscriptionId;
            this.resourceGroup = resourceGroup;
            this.dataFactoryName = dataFactoryName;
            this.pipelineName = pipelineName;
            this.client = ADFClient();
        }

        public DataFactoryManagementClient ADFClient()
        {
            var context = new AuthenticationContext("https://login.windows.net/" + tenantID);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync(
                "https://management.azure.com/", cc).Result;
            ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
            var client = new DataFactoryManagementClient(cred)
            {
                SubscriptionId = subscriptionId
            };
            return client;
        }
        public void PipelineRun()
        {
            Console.WriteLine("Creating pipeline run...");
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(
                resourceGroup, dataFactoryName, pipelineName, parameters: parameters
            ).Result.Body;
            Console.WriteLine("Pipeline run ID: " + runResponse.RunId);
        }
        public void PipelineMonitor()
        {
            Console.WriteLine("Checking pipeline run status...");
            PipelineRun pipelineRun;
            while (true)
            {
                pipelineRun = client.PipelineRuns.Get(
                    resourceGroup, dataFactoryName, runResponse.RunId);
                Console.WriteLine("Status: " + pipelineRun.Status);
                if (pipelineRun.Status == "InProgress" || pipelineRun.Status == "Queued")
                    System.Threading.Thread.Sleep(2000);
                else
                    break;
            }
        }
        public string GetPipelineRunStatus()
        {
            Console.WriteLine("Checking pipeline run status...");
            PipelineRun pipelineRun;
            pipelineRun = client.PipelineRuns.Get(
                    resourceGroup, dataFactoryName, runResponse.RunId);
                Console.WriteLine("Status: " + pipelineRun.Status);
            return pipelineRun.Status;
        }
    }
}
