using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMACSSchedulerBusinessTier;
using Microsoft.AspNetCore.Mvc;
using Models;
using HIMACSScheduleDataTier;

namespace HIMACS_SchedulerUI.Controllers
{
    public class JobSpecificConfigController : Controller
    {
        public IEnumerable<Job> Jobs;
        public IEnumerable<JobHistory> jobHistory;
        private void BindJobModel()
        {
            Jobs = HIMACSScheduleEngineBO.GetJobs();
        }
        public IActionResult Index()
        {
            BindJobModel();
            return View(Jobs);
        }

        public IActionResult Details(int id)
        {
            IEnumerable<JobSpecificConfig> jobSpecific = HIMACSScheduleEngineBO.GetJobSpecificConfig(id);
            ViewData["JobName"] = HIMACSScheduleEngineDO.JobNameById(id).Rows[0]["Name"].ToString();
            return View(jobSpecific);
        }

        public IActionResult Edit(int id, string key)
        {
            JobSpecificConfig jobSpecific = HIMACSScheduleEngineBO.GetJobSpecificConfigByIDAndKey(id, key);
            return View(jobSpecific);
        }
        [HttpPost]
        public IActionResult Edit(JobSpecificConfig jobspecific)
        {
            HIMACSScheduleEngineBO.UpdateJobSpecificConfigByIDAndKey(jobspecific);

            IEnumerable<JobSpecificConfig> jobSpecific = HIMACSScheduleEngineBO.GetJobSpecificConfig(jobspecific.JobId);
            ViewData["JobName"] = HIMACSScheduleEngineDO.JobNameById(jobspecific.JobId).Rows[0]["Name"].ToString();
            return View("Details",jobSpecific);
        }

    }
}
