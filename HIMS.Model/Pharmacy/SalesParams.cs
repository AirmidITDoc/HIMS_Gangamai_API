using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class SalesParams
    {
            public SalesInsert SalesInsert { get; set; }
            public List<SalesDetailInsert> SalesDetailInsert { get; set; }
            public List<UpdateCurStkSales> UpdateCurStkSales { get; set; }
            public Cal_DiscAmount_Sales Cal_DiscAmount_Sales { get; set; }
            public Cal_GSTAmount_Sales Cal_GSTAmount_Sales { get; set; }
    }
    public class SalesInsert
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }               
        public int OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public float TotalAmount { get; set; }
        public float VatAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public float PaidAmount { get; set; }
        public float BalanceAmount { get; set; }
        public long ConcessionReasonID { get; set; }
        public long ConcessionAuthorizationId { get; set; }
        public bool IsSellted { get; set; }
        public bool IsPrint { get; set; }
        public bool IsFree { get; set; }
        public long UnitID { get; set; }
        public long AddedBy { get; set; }
        public String ExternalPatientName { get; set; }
        public String DoctorName { get; set; }
        public long StoreId { get; set;}
        public bool IsPrescription { get; set; }
        public String CreditReason { get; set; }
        public long CreditReasonID { get; set; }
        public long WardId { get; set; }
        public long BedID { get; set; }
        public float Discper_H { get; set; }
        public bool IsPurBill { get; set; }
        public bool IsBillCheck { get; set; }
        public string SalesHeadName { get; set; }
        public long SalesTypeId { get; set; }
        public long SalesId { get; set; }
    }

    public class SalesDetailInsert
    {
        public long SalesID { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public float UnitMRP { get; set; }
        public long Qty { get; set; }
        public float TotalAmount { get; set; }
        public float VatPer { get; set; }
        public float VatAmount { get; set; }
        public float @DiscPer { get; set; }
        public float @DiscAmount { get; set; }
        public float @GrossAmount { get; set; }
        public float @LandedPrice { get; set; }
        public float @TotalLandedAmount { get; set; }
        public float @PurRateWf { get; set; }
        public float @PurTotAmt { get; set; }
        public float @CGSTPer { get; set; }
        public float @CGSTAmt { get; set; }

        public float @SGSTPer { get; set; }
        public float @SGSTAmt { get; set; }
        public float @IGSTPer { get; set; }
        public float @IGSTAmt { get; set; }
        public bool @IsPurRate { get; set; }
        public long @StkID { get; set; }

    }
    public class UpdateCurStkSales
    {
        public long ItemId { get; set; }
        public long IssueQty { get; set; }
        public long StoreID { get; set; }
        public long StkID { get; set; }
    }
    public class Cal_DiscAmount_Sales
    {
        public long SalesID { get; set; }
    }
    public class Cal_GSTAmount_Sales
    {
        public long SalesID { get; set; }
    }

}
