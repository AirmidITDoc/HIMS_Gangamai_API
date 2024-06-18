using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class CanteenSalesParams
    {
        public CatenBilHederInsert CatenBilHederInsert { get; set; }
        public List<CatenBilDetInsert> CatenBilDetInsert { get; set; }
    }
    public class CatenBilHederInsert
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int StoreId { get; set; }
        public int OP_IP_ID { get; set; }
        public string CustomerName { get; set; }
        public float TotalAmount { get; set; }
        public float GSTPer { get; set; }
        public float GSTAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public float PaidAmount { get; set; }
        public float BalanceAmount { get; set; }
        public long ConcessionReasonID { get; set; }
        public long ConcessionAuthorizationId { get; set; }
        public bool IsPrint { get; set; }
        public bool IsFree { get; set; }
        public long UnitID { get; set; }
        public long AddedBy { get; set; }
        public long ReqId { get; set; }
        public bool IsOtherOrIsEmpBill { get; set; }
        public long BillNo { get; set; }
    }
    public class CatenBilDetInsert
    {
        public long BillNo { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public float UnitMRP { get; set; }
        public long Qty { get; set; }
        public float TotalAmount { get; set; }
        public float GSTPer { get; set; }
        public float GSTAmount { get; set; }
        public float DiscPer { get; set; }
        public float DiscAmount { get; set; }
        public float GrossAmount { get; set; }
        public float LandedPrice { get; set; }
        public float TotalLandedAmount { get; set; }
        public long ReqId { get; set; }
        public long ReqDetId { get; set; }
    }

}
