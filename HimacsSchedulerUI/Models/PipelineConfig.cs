using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PipelineConfig
    {
        public int JobId { get; set; }
        public string TenantID { get; set; }
        public string ApplicationId { get; set; }
        public string AuthenticationKey { get; set; }
        public string SubscriptionId{ get; set; }
        public string ResourceGroup { get; set; }
        public string DataFactoryName { get; set; }
        public string PipelineName { get; set; }
    }
}
