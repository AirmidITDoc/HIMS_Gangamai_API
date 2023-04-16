using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Radiology

{
    public class RadiologyTemplateResultParams
    {
        public RadiologyReportHeaderUpdate RadiologyReportHeaderUpdate { get; set; }
    }
 
     public class RadiologyReportHeaderUpdate
     {
        public int RadReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime ReportTime { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean IsPrinted { get; set; }
        public int RadResultDr1 { get; set; }
        public int RadResultDr2 { get; set; }
        public int RadResultDr3 { get; set; }
        public string SuggestionNotes { get; set; }
        public int AdmVisitDoctorID { get; set; }
        public int RefDoctorID { get; set; }
        public string ResultEntry { get; set; }
    }
    
}
