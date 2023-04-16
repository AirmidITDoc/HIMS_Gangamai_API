using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPLabrequestChangeParam
    {
        public InsertIPRequestLabcharges InsertIPRequestLabcharges { get; set; }
    }
  
    public class InsertIPRequestLabcharges
    {
        public int OP_IP_ID { get; set; }
        public int ClassID { get; set; }
         public int ServiceId { get; set; }
        public int TraiffId { get; set; }
        public int ReqDetId { get; set; }
        public int UserId { get; set; }
        public DateTime ChargesDate { get; set; }

    }
}
