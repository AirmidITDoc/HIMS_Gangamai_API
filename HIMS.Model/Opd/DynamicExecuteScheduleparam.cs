using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class DynamicExecuteScheduleparam
    {
        public InsertDynamicExecuteSchedule InsertDynamicExecuteSchedule { get; set; }
        public UpdateDynamicExecuteSchedule UpdateDynamicExecuteSchedule { get; set; }
    }

    public class InsertDynamicExecuteSchedule
    {
        //  public int ScheduleId { get; set; }
        public String ScheduleExecuteType { get; set; }
        public int MonthDay { get; set; }
        public String WeekDayName { get; set; }
        public DateTime ExecuteTime { get; set; }
        public String Query { get; set; }
        public bool IsDelete { get; set; }
    }

    public class UpdateDynamicExecuteSchedule
    {
        public int ScheduleId { get; set; }
        public String ScheduleExecuteType { get; set; }
        public int MonthDay { get; set; }
        public String WeekDayName { get; set; }
        public DateTime ExecuteTime { get; set; }
        public String Query { get; set; }
        public bool IsDelete { get; set; }
    }
}