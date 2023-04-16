using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OpdCasePaperParams
    {
        public InsertOpdCasePaper InsertOpdCasePaper { get; set; }
        public UpdateOpdCasePaper UpdateOpdCasePaper { get; set; }

    }
    public class InsertOpdCasePaper 
    {
        public long VisitId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Pluse { get; set; }
        public string BP { get; set; }
        public string PastHistory { get; set; }
        public string PresentHistory { get; set; }
        public string Complaint { get; set; }
        public string Finding { get; set; }
        public string Diagnosis { get; set; }
        public string Investigations { get; set; }
        public string BSL { get; set; }
        public string SpO2 { get; set; }
        public string PersonalDetails { get; set; }

        public long CasePaperID { get; set; }
    }
    public class UpdateOpdCasePaper 
    {
        public long CasePaperID { get; set; }
        public long VisitId { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Pluse { get; set; }
        public string BP { get; set; }
        public string PastHistory { get; set; }
        public string PresentHistory { get; set; }
        public string Complaint { get; set; }
        public string Finding { get; set; }
        public string Diagnosis { get; set; }
        public string Investigations { get; set; }
        public string BSL { get; set; }
        public string SpO2 { get; set; }

        public string PersonalDetails { get; set; }
    }

}
