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
    public class JobHistoryController : Controller
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
            jobHistory = HIMACSScheduleEngineBO.Get_JobHistory(id);
            ViewData["JobName"] =  HIMACSScheduleEngineDO.JobNameById(id).Rows[0]["Name"].ToString();
            return View(jobHistory);
        }
    }
}
