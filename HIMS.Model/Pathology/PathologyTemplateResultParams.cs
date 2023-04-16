using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pathology
{
    public class PathologyTemplateResultParams
    {
        public DeletePathologyReportTemplateDetails DeletePathologyReportTemplateDetails { get; set; }
        public List<InsertPathologyReportTemplateDetails> InsertPathologyReportTemplateDetails { get; set; }
        public UpdatePathTemplateReportTemplateHeader UpdatePathTemplateReportHeader { get; set; }
    }

    public class DeletePathologyReportTemplateDetails
    {
        public int PathReportId { get; set; }
    }

    public class InsertPathologyReportTemplateDetails
    {
        public int PathReportId { get; set; }
        public int PathTemplateId { get; set; }
        public string PathTemplateDetailsResult { get; set; }
        public int TestId { get; set; }
    }

    public class UpdatePathTemplateReportTemplateHeader
    {
        public int PathReportID { get; set; } 
        public DateTime ReportDate { get; set; }
        public DateTime ReportTime { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean IsPrinted { get; set; }
        public int PathResultDr1 { get; set; }
        public int PathResultDr2 { get; set; }
        public int PathResultDr3 { get; set; }
        public int IsTemplateTest { get; set; }
        public string SuggestionNotes { get; set; }
        public int AdmVisitDoctorID { get; set; }
        public int RefDoctorID { get; set; }
       // public int AddedBy { get; set; }
    }

}
