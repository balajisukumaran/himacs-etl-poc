using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class JobSchedule
    {
        public int Jobid { get; set; }
        public string  Schedule { get; set; }
        public string  Job { get; set; }
        public String  NextRunDate{ get; set; }
        public string  Status { get; set; }
    }
}
