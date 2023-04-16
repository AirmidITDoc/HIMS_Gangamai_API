using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class pathologyReportDetailParams
    {
        public DeletePathologyReportDetail DeletePathologyReportDetail { get; set; }
        public InsertPathologyReportDetail InsertPathologyReportDetail { get; set; }
        public UpdatePathologyReportHeader UpdatePathologyReportHeader { get; set; }

     }


    public class InsertPathologyReportDetail
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
        public long MinValue { get; set; }

        public long MaxValue { get; set; }
    }

    public class UpdatePathologyReportHeader
    {
        public int PathReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime ReportTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPrinted { get; set; }
        public int PathResultDr1 { get; set; }
        public int PathResultDr2 { get; set; }
        public int PathResultDr3 { get; set; }
        public int IsTemplateTest { get; set; }
        public String SuggestionNotes { get; set; }
        public int AdmVisitDoctorID { get; set; }
        public int RefDoctorID { get; set; }
        public int AddedBy { get; set; }
    }
    public class DeletePathologyReportDetail
    {
        public int PathReportId { get; set; }
    }

}

