using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMACSSchedulerBusinessTier;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HIMACS_SchedulerUI.Controllers
{
    public class PipelineConfigController : Controller
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
        public IActionResult Edit(int jobId)
        {
            PipelineConfig config = HIMACSScheduleEngineBO.GetPipelineConfigAsConfigObject(jobId);
            return View(config);
        }

        [HttpPost]
        public IActionResult Edit(PipelineConfig config)
        {
            HIMACSScheduleEngineBO.UpdatePipelineConfig(config);
            BindJobModel();
            return View("Index",Jobs);
        }
    }
}
