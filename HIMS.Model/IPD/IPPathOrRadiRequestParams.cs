using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPPathOrRadiRequestParams
    {
        public IPPathOrRadiRequestInsert IPPathOrRadiRequestInsert { get; set; }
        public List<IPPathOrRadiRequestLabRequestInsert> IPPathOrRadiRequestLabRequestInsert { get; set; }
    }

    public class IPPathOrRadiRequestInsert
    {
       
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public int OP_IP_ID { get; set; }
        public int OP_IP_Type { get; set; }
        public int IsAddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }

        public DateTime IsCancelledTime { get; set; }
        public bool IsOnFileTest { get; set; }
        public int RequestId { get; set; }
    }

    public class IPPathOrRadiRequestLabRequestInsert
    {
        
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public int Price { get; set; }
        public bool IsStatus { get; set; }
        public bool IsOnFileTest { get; set; }
    }
}