using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master
{
    public class ScheduleMaster
    {
        public int Id { get; set; }
        public SchedulerTypes ScheduleType { get; set; }
        public string Hours { get; set; }
        public string Days { get; set; }
        public string Dates { get; set; }
        public string Custom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Query { get; set; }
    }
    public enum SchedulerTypes
    {
        Daily = 1, Weekly = 2, Monthly = 3, Custom = 4
    }
}
