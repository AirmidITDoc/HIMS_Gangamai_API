using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class LAD_RADParams
    {
        public InsertH_LabRequest InsertH_LabRequest { get; set; }
        public InsertD_LabRequest InsertD_LabRequest { get; set; }
    }
    public class InsertH_LabRequest
    {
        public int RequestId { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public int OP_IP_ID { get; set; }
        public int OP_IP_Type { get; set; }
        public int IsAddedBy { get; set; }
        public int IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public DateTime IsCancelledTime { get; set; }
 
    }

    public class InsertD_LabRequest
    {
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public Boolean IsStatus { get; set; }

    }
}
