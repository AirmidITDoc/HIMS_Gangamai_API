using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
        public class CanteenRequestParams
        {
            public CanteenRequestHeaderInsert CanteenRequestHeaderInsert { get; set; }
            public List<CanteenRequestDetailsInsert> CanteenRequestDetailsInsert { get; set; }
        }
        public class CanteenRequestHeaderInsert
        {

            public DateTime Date { get; set; }
            public DateTime Time { get; set; }
            public int OP_IP_ID { get; set; }
            public int OP_IP_Type { get; set; }
            public int WardId { get; set; }
            public int CashCounterID { get; set; }
            public bool IsFree { get; set; }
            public int UnitID { get; set; }
            public bool IsBillGenerated { get; set; }
            public int AddedBy { get; set; }
            public bool IsPrint { get; set; }
            public int ReqId { get; set; }

        }

        public class CanteenRequestDetailsInsert
        {
            public int ReqId { get; set; }
            public int ItemId { get; set; }
            public float UnitMRP { get; set; }
            public int Qty { get; set; }
            public float TotalAmount { get; set; }
        }
}
