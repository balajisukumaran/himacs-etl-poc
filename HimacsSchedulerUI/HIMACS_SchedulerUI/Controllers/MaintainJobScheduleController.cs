using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMACSScheduleDataTier;
using HIMACSSchedulerBusinessTier;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HIMACS_SchedulerUI.Controllers
{
    public class MaintainJobScheduleController : Controller
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
        public IActionResult List(int id)
        {
            IEnumerable<JobSchedule> jobschedule = HIMACSScheduleEngineBO.GetJobSchedule(id);
            ViewData["JobName"] = HIMACSScheduleEngineDO.JobNameById(id).Rows[0]["Name"].ToString();
            return View(jobschedule);
        }
        public IActionResult Edit(int id)
        {
            JobSchedule jobschedule = HIMACSScheduleEngineBO.GetOneJobSchedule(id);
            ViewData["JobName"] = HIMACSScheduleEngineDO.JobNameById(id).Rows[0]["Name"].ToString();
            return View(jobschedule);
        }

        [HttpPost]
        public IActionResult Edit(JobSchedule schedul)
        {
            HIMACSScheduleEngineDO.UpdateRunStatusAndNextRunOnCompletion(schedul.Jobid, schedul.Status,Convert.ToDateTime(schedul.NextRunDate));
            BindJobModel();
            return View("Index",Jobs);
        }

        public IActionResult Delete(int id)
        {
            IEnumerable<JobSchedule> jobschedule = HIMACSScheduleEngineBO.GetJobSchedule(id);
            ViewData["JobName"] = HIMACSScheduleEngineDO.JobNameById(id).Rows[0]["Name"].ToString();
            return View(jobschedule);
        }
    }
}
