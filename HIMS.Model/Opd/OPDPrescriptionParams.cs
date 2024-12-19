using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OPDPrescriptionParams
    {
        public delete_OPPrescription delete_OPPrescription { get; set; }
        public List<InsertOPDPrescription> InsertOPDPrescription { get; set; }
        public Update_VisitFollowupDate Update_VisitFollowupDate { get; set; }
        public List<OPRequestList> OPRequestList { get; set; }
        public List<OPCasepaperDignosisMaster> OPCasepaperDignosisMaster { get; set; }
    }

    public class InsertOPDPrescription
    {
       // public int PrecriptionId { get; set; }
        public int OPD_IPD_IP { get; set; }
        public int OPD_IPD_Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime PTime { get; set; }
        public int ClassID { get; set; }
        public int GenericId { get; set; }
        public int DrugId { get; set; }
        public int DoseId { get; set; }
        public int Days { get; set; }
        public string Instruction { get; set; }
        public string Remark { get; set; }
        public int DoseOption2 { get; set; }
        public int DaysOption2 { get; set; }
        public int DoseOption3 { get; set; }
        public int DaysOption3 { get; set; }
        public int InstructionId { get; set; }
        public long QtyPerDay { get; set; }
        public long TotalQty { get; set; }
        public bool IsClosed { get; set; }
        public bool IsEnglishOrIsMarathi { get; set; }
        public String ChiefComplaint { get; set; }
        public String Diagnosis { get; set; }
        public String Examination { get; set; }
        public string Height { get; set; }
        public string PWeight { get; set; }
        public string BMI { get; set; }
        public string BSL { get; set; }
        public string SpO2 { get; set; }
        public string Temp { get; set; }
        public String Pulse { get; set; }
        public String BP { get; set; }
        public int StoreId { get; set; }
        public int PatientReferDocId { get; set; }
        public string Advice { get; set; }
        public int IsAddBy { get; set; }
    }

    public class Update_VisitFollowupDate
    {
        public int VisitId { get; set; }
        public DateTime FollowupDate { get; set; }
    }

    public class OPRequestList
    {
        public int OP_IP_ID { get; set; }
        public long ServiceId { get; set; }
    }
    public class OPCasepaperDignosisMaster
    {
        public string DescriptionType { get; set; }
        public string DescriptionName { get; set; }
    }
    public class delete_OPPrescription
    {
        public int OP_IP_ID { get; set; }
    }

}