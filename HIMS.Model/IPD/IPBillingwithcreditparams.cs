using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IPBillingwithcreditparams
    {
        public InsertBillcreditUpdateBillNo InsertBillcreditUpdateBillNo { get; set; }
        public List<BillDetailscreditInsert> BillDetailscreditInsert { get; set; }
        public Cal_DiscAmount_IPBillcredit Cal_DiscAmount_IPBillcredit { get; set; }
        public AdmissionIPBillingcreditUpdate AdmissionIPBillingcreditUpdate { get; set; }
     
        public IPBillBalAmountcredit IPBillBalAmountcredit { get; set; }


        public List<IPAdvanceDetailUpdatecedit> IPAdvanceDetailUpdatecedit { get; set; }

        public IPAdvanceHeaderUpdatecredit IPAdvanceHeaderUpdatecredit { get; set; }
    }



         public class InsertBillcreditUpdateBillNo
    {
        public int BillNo { get; set; }
        public int OPD_IPD_ID { get; set; }
        public float TotalAmt { get; set; }
        public float ConcessionAmt { get; set; }
        public float NetPayableAmt { get; set; }
        public float PaidAmt { get; set; }
        public float BalanceAmt { get; set; }
        public DateTime BillDate { get; set; }
        public int OPD_IPD_Type { get; set; }
        public int AddedBy { get; set; }
        public float TotalAdvanceAmount { get; set; }
        public DateTime BillTime { get; set; }
        public int ConcessionReasonId { get; set; }
        public Boolean IsSettled { get; set; }
        public Boolean IsPrinted { get; set; }
        public Boolean IsFree { get; set; }
        public int CompanyId { get; set; }
        public int TariffId { get; set; }
        public int UnitId { get; set; }
        public int InterimOrFinal { get; set; }
        public int CompanyRefNo { get; set; }
        public int ConcessionAuthorizationName { get; set; }
        public float TaxPer { get; set; }
        public float TaxAmount { get; set; }
        //public string BillRemark { get; set; }
        public string DiscComments { get; set; }

    }

    public class BillDetailscreditInsert
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class Cal_DiscAmount_IPBillcredit
    {
        public int BillNo { get; set; }

    }

    public class AdmissionIPBillingcreditUpdate
    {
        public int AdmissionID { get; set; }
    }
   

    public class IPBillBalAmountcredit
    {
        public int BillNo { get; set; }
        public long BillBalAmount { get; set; }

    }

    public class IPAdvanceDetailUpdatecedit
    {
        public long AdvanceDetailID { get; set; }
        public long UsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }


    public class IPAdvanceHeaderUpdatecredit
    {

        public long AdvanceId { get; set; }
        public long AdvanceUsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }
}
