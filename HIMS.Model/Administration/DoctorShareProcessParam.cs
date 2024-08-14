using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Administration
{
    public class DoctorShareProcessParam
    {
        public ProcessDoctorShareParam ProcessDoctorShareParam { get; set; }
    }
    public class ProcessDoctorShareParam
        {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
