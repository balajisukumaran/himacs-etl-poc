using System;
using System.Collections.Generic;
using System.Data;
using HIMACSScheduleDataTier;
using Models;

namespace HIMACSSchedulerBusinessTier
{
    public class HIMACSScheduleEngineBO
    {
        public static DataTable GetActiveJobSchedule()
        {
            return HIMACSScheduleEngineDO.GetActiveJobSchedule();
        }
        public static IEnumerable<JobSchedule> GetJobSchedule(int id)
        {
            DataTable dt=HIMACSScheduleEngineDO.GetJobSchedule(id);
            List<JobSchedule> schedules = new List<JobSchedule>();
            foreach(DataRow dr in dt.Rows)
            {
                JobSchedule schedule = new JobSchedule();
                schedule.Jobid = Convert.ToInt32(dr["jobid"]);
                schedule.Schedule = Convert.ToString(dr["Schedule"]);
                schedule.Job = Convert.ToString(dr["Job"]);
                schedule.NextRunDate= Convert.ToDateTime(dr["Nextrundate"]).ToString("MM/dd/yyyy HH:mm");
                schedule.Status = Convert.ToString(dr["status"]);
                schedules.Add(schedule);
            }
            return schedules;
        }
        public static JobSchedule GetOneJobSchedule(int id)
        {
            DataTable dt = HIMACSScheduleEngineDO.GetJobSchedule(id);
            JobSchedule schedule = new JobSchedule();
            foreach (DataRow dr in dt.Rows)
            {
                schedule.Jobid = Convert.ToInt32(dr["jobid"]);
                schedule.Schedule = Convert.ToString(dr["Schedule"]);
                schedule.Job = Convert.ToString(dr["Job"]);
                schedule.NextRunDate = Convert.ToDateTime(dr["Nextrundate"]).ToString("MM/dd/yyyy HH:mm");
                schedule.Status = Convert.ToString(dr["status"]);
            }
            return schedule;
        }

        public static DataTable GetRunningJobSchedule()
        {
            return HIMACSScheduleEngineDO.GetRunningJobSchedule();
        }

        public static DataTable GetPipelineConfig(int jobid)
        {
           return HIMACSScheduleEngineDO.GetPipelineConfig(jobid);
        }
        public static PipelineConfig GetPipelineConfigAsConfigObject(int jobid)
        {
            DataTable dt=HIMACSScheduleEngineDO.GetPipelineConfig(jobid);
            PipelineConfig config = new PipelineConfig();
            config.JobId = Convert.ToInt32(dt.Rows[0]["job_id"]);
            config.TenantID = Convert.ToString(dt.Rows[0]["TenantID"]);
            config.ApplicationId = Convert.ToString(dt.Rows[0]["ApplicationId"]);
            config.AuthenticationKey= Convert.ToString(dt.Rows[0]["AuthenticationKey"]);
            config.SubscriptionId= Convert.ToString(dt.Rows[0]["SubscriptionId"]);
            config.ResourceGroup = Convert.ToString(dt.Rows[0]["ResourceGroup"]);
            config.DataFactoryName = Convert.ToString(dt.Rows[0]["DataFactoryName"]);
            config.PipelineName = Convert.ToString(dt.Rows[0]["PipelineName"]);
            return config;
        }

        public static void UpdatePipelineConfig(PipelineConfig config)
        {
            HIMACSScheduleEngineDO.
                UpdatePipelineConfig(config.JobId, config.TenantID, config.ApplicationId, config.AuthenticationKey, config.SubscriptionId, config.ResourceGroup, config.DataFactoryName, config.PipelineName);
        }

        public static void UpdateRunStatus(int jobid, string status)
        {
            HIMACSScheduleEngineDO.UpdateRunStatus(jobid, status);
        }

        public static IEnumerable<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();
            DataTable dt = HIMACSScheduleEngineDO.GetJobs();
            foreach(DataRow dr in dt.Rows)
            {
                Job job = new Job();
                job.JobId= Convert.ToInt32(dr["Job_id"]);
                job.JobName = Convert.ToString(dr["name"]);
                jobs.Add(job);
            }
            return jobs;
        }

        public static JobSpecificConfig GetJobSpecificConfigByIDAndKey(int id, string key)
        {
            DataTable config= HIMACSScheduleEngineDO.GetJobSpecificConfigByIDAndKey(id, key);
            JobSpecificConfig Jobconfig = new JobSpecificConfig();
            Jobconfig.JobId = Convert.ToInt32 (config.Rows[0]["job_id"]);
            Jobconfig.Key = Convert.ToString(config.Rows[0]["key"]);
            Jobconfig.Value = Convert.ToString(config.Rows[0]["value"]);
            return Jobconfig;
        }

        public static void UpdateRunStatusAndNextRunOnCompletion(int jobid, string status, DateTime nextRunDate)
        {
            HIMACSScheduleEngineDO.UpdateRunStatusAndNextRunOnCompletion(jobid, status, nextRunDate);
        }

        public static void UpdateJobSpecificConfigByIDAndKey(JobSpecificConfig jobspecific)
        {
            HIMACSScheduleEngineDO.UpdateJobSpecificConfigByIDAndKey(jobspecific.JobId,jobspecific.Key,jobspecific.Value);
        }

        public static IEnumerable<JobSpecificConfig> GetJobSpecificConfig(int jobid)
        {
            List<JobSpecificConfig> jobspecificlist = new List<JobSpecificConfig>();
            DataTable dt = HIMACSScheduleEngineDO.GetJobSpecificConfig(jobid);
            foreach (DataRow dr in dt.Rows)
            {
                JobSpecificConfig jobSpecific = new JobSpecificConfig();
                jobSpecific.JobId = Convert.ToInt32(dr["job_id"]);
                jobSpecific.Key = Convert.ToString(dr["key"]);
                jobSpecific.Value = Convert.ToString(dr["value"]);
                jobspecificlist.Add(jobSpecific);
            }
            return jobspecificlist;
        }

        public static List<JobHistory> Get_JobHistory(int id)
        {
            DataTable dt = HIMACSScheduleEngineDO.Get_JobHistory(id);
            List<JobHistory> jobHistoryList = new List<JobHistory>();
            foreach(DataRow dr in dt.Rows)
            {
                JobHistory jobHistory = new JobHistory();
                jobHistory.JobId = Convert.ToInt32(dr["job_id"]);
                jobHistory.Status = Convert.ToString(dr["status"]);
                jobHistory.DateTime = Convert.ToString(dr["onDateTime"]);
                jobHistoryList.Add(jobHistory);
            }
            return jobHistoryList;
        }
    }
}
