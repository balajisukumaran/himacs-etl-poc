using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using HIMACSSchedulerBusinessTier;
using ADFPipelineLibrary;
using System.Threading;

namespace HIMACSScheduleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Start();
                //Update();
            }
        }

        #region :: Run Not started Schedules ::
        static void Start()
        {
            DataTable schedules = HIMACSScheduleEngineBO.GetActiveJobSchedule();
            foreach (DataRow schedule in schedules.Rows)
            {
                int jobid = Convert.ToInt32(schedule["jobid"]);
                DataTable plConfig = HIMACSScheduleEngineBO.GetPipelineConfig(jobid);
                ADFPipeline pipeline = new ADFPipeline(plConfig.Rows[0]["TenantID"].ToString(), plConfig.Rows[0]["ApplicationId"].ToString(), plConfig.Rows[0]["AuthenticationKey"].ToString(), plConfig.Rows[0]["SubscriptionId"].ToString(), plConfig.Rows[0]["ResourceGroup"].ToString(), plConfig.Rows[0]["dataFactoryName"].ToString(), plConfig.Rows[0]["PipelineName"].ToString());
                pipeline.PipelineRun();
                HIMACSScheduleEngineBO.UpdateRunStatus(jobid, "Running");
            }
        }
        #endregion

        //#region :: Update Status ::
        //static void Update()
        //{
        //    DataTable schedules = HIMACSScheduleEngineBO.GetRunningJobSchedule();
        //    foreach (DataRow schedule in schedules.Rows)
        //    {
        //        int jobid = Convert.ToInt32(schedule["jobid"]);
        //        DataTable plConfig = HIMACSScheduleEngineBO.GetPipelineConfig(jobid);
        //        ADFPipeline pipeline = new ADFPipeline(plConfig.Rows[0]["TenantID"].ToString(), plConfig.Rows[0]["ApplicationId"].ToString(), plConfig.Rows[0]["AuthenticationKey"].ToString(), plConfig.Rows[0]["SubscriptionId"].ToString(), plConfig.Rows[0]["ResourceGroup"].ToString(), plConfig.Rows[0]["dataFactoryName"].ToString(), plConfig.Rows[0]["PipelineName"].ToString());
        //        string status= pipeline.GetPipelineRunStatus();
        //        if (status == "InProgress")
        //            HIMACSScheduleEngineBO.UpdateRunStatus(jobid, "Running");

        //        if(status == "Queued")
        //            HIMACSScheduleEngineBO.UpdateRunStatus(jobid, "Idle with Exception");

        //        else
        //            HIMACSScheduleEngineBO.UpdateRunStatusAndNextRunOnCompletion(jobid, "Idle", DateTime.MaxValue);
        //    }
        //}
        //#endregion
    }
}