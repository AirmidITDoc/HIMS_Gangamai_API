using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class NursingSugarLevelParam
    {
        public SaveNursingSugarLevelParam SaveNursingSugarLevelParams { get; set; }
        public UpdateNursingSugarLevelParam UpdateNursingSugarLevelParams { get; set; }
    }
    public class SaveNursingSugarLevelParam
    {
       
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public string BSL { get; set; }
        public string UrineSugar { get; set; }
        public string ETTPressure { get; set; }
        public string UrineKetone { get; set; }
        public string Bodies { get; set; }
        public long IntakeMode { get; set; }
        public string ReportedToRMO { get; set; }
      
        public long CreatedBy { get; set; }
       

    }

    public class UpdateNursingSugarLevelParam
    {
        public long Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime EntryTime { get; set; }
        public long AdmissionId { get; set; }
        public string BSL { get; set; }
        public string UrineSugar { get; set; }
        public string ETTPressure { get; set; }
        public string UrineKetone { get; set; }
        public string Bodies { get; set; }
        public long IntakeMode { get; set; }
        public string ReportedToRMO { get; set; }
        public long ModifiedBy { get; set; }
    }
}
