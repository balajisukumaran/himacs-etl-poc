using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HIMACSScheduleDataTier;
using HIMACSSchedulerBusinessTier;
using System.Data;

namespace HIMACS_Scheduler.Pages
{
    public class JobHistoryModel : PageModel
    {
        public List<SelectListItem> Options { get; set; }
        public IEnumerable<Job> Jobs = new List<Job>();
        
        public void OnGet()
        {

            BindJobModel();
            Options = Jobs.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.JobId.ToString(),
                                      Text = a.JobName
                                  }).ToList();
        }
        private void BindJobModel()
        {
            Jobs= HIMACSScheduleEngineBO.GetJobs();
        }

        public void OnPostPopulateTable()
        {
            string a = Request.Form["jobId"].ToString();

            BindJobModel();
            Options = Jobs.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.JobId.ToString(),
                                      Text = a.JobName
                                  }).ToList();
        }
    }
}
