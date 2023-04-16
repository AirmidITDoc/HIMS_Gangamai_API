using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPPrescriptionParams
    {
        public DeleteIP_Prescription DeleteIP_Prescription { get; set; }
        public InsertIP_MedicalRecord InsertIP_MedicalRecord { get; set; }
        public List<InsertIP_Prescription> InsertIP_Prescription { get; set; }
    }

    public class DeleteIP_Prescription
    {
        public int OP_IP_ID { get; set; }
    }

    public class InsertIP_MedicalRecord
    {
        public int MedicalRecoredId { get; set; }
        public int AdmissionId { get; set; }
        public DateTime RoundVisitDate { get; set; }
        public DateTime RoundVisitTime { get; set; }
        public int InHouseFlag { get; set; }
 
    }
    

    public class InsertIP_Prescription
    {
        public int IPMedID { get; set; }
        public int OP_IP_ID { get; set; }
        public int OPD_IPD_Type { get; set; }
        public DateTime PDate { get; set; }
        public DateTime PTime { get; set; }
        public int ClassID { get; set; }
        public int GenericId { get; set; }
        public int DrugId { get; set; }
        public int DoseId { get; set; }
        public int Days { get; set; }
        public float QtyPerDay { get; set; }
        public float TotalQty { get; set; }
        public string Remark { get; set; }
        public Boolean IsClosed { get; set; }
        public int IsAddBy { get; set; }
        public int StoreId { get; set; }
        public int WardID { get; set; }
    }
}
