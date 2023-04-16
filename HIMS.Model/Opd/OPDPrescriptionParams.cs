using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
    public class OPDPrescriptionParams
    {
        public UpdateOPDPrescription UpdateOPDPrescription { get; set; }
        public InsertOPDPrescription InsertOPDPrescription { get; set; }
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
         public int RefDoctorId { get; set; }
    

    }

    public class UpdateOPDPrescription
    {
        public int OPD_IPD_IP { get; set; }

    }

}