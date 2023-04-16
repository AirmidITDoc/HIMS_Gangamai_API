using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pathology
{
   public class pathresultentryparam
    {
        public Deletepathreportheader Deletepathreportheader { get; set; }

        public List<Insertpathreportdetail> Insertpathreportdetail { get; set; }
        public Updatepathreportheader Updatepathreportheader { get; set; }
    }


    public class Deletepathreportheader
    {
        public int PathReportID { get; set; }
    }
    public class Insertpathreportdetail
    {
        public int PathReportId { get; set; }
        public int CategoryID { get; set; }
        public int TestID { get; set; }
        public int SubTestId { get; set; }
        public int ParameterId { get; set; }
        public String ResultValue { get; set; }
        public int UnitId { get; set; }
        public String NormalRange { get; set; }
        public int PrintOrder { get; set; }
        public int PIsNumeric { get; set; }
        public String CategoryName { get; set; }
        public String TestName { get; set; }
        public String SubTestName { get; set; }
        public String ParameterName { get; set; }
        public String UnitName { get; set; }
        public String PatientName { get; set; }
        public String RegNo { get; set; }
        public String SampleID { get; set; }
    }

    public class Updatepathreportheader
    {
        public int PathReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime ReportTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPrinted { get; set; }
        public int PathResultDr1 { get; set; }
        public int PathResultDr2 { get; set; }
        public int PathResultDr3 { get; set; }
        public bool IsTemplateTest { get; set; }
        public string SuggestionNotes { get; set; }
        public int AdmVisitDoctorID { get; set; }
        public int RefDoctorID { get; set; }
    }
}


