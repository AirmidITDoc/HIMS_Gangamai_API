using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
              public class CasePaperPrescriptionParams
            {

            public InsertOpCasePaper InsertOpCasePaper { get; set; }
            public List<InsertOPPrescription> InsertOPPrescription { get; set; }

            }

            public class InsertOpCasePaper
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

        public class InsertOPPrescription
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
            public int InstructionId { get; set; }
            public long QtyPerDay { get; set; }
            public long TotalQty { get; set; }
            public string Instruction { get; set; }
            public string Remark { get; set; }
            public bool IsEnglishOrIsMarathi { get; set; }
            public string PWeight { get; set; }
            public string Pulse { get; set; }
            public string BP { get; set; }
            public String BSL { get; set; }
            public String ChiefComplaint { get; set; }
            public int IsAddBy { get; set; }
            public String SpO2 { get; set; }
            public int StoreId { get; set; }
            public int DoseOption2 { get; set; }
        public int DaysOption2 { get; set; }
        public int DoseOption3 { get; set; }
        public int DaysOption3 { get; set; }


    }
    }
