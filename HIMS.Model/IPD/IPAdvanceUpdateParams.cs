using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPAdvanceUpdateParams
    {
        public AdvanceHeaderUpdate AdvanceHeaderUpdate { get; set; }
        public AdvanceDetailInsert1 AdvanceDetailInsert1 { get; set; }
        public IPPaymentInsert1 IPPaymentInsert1 { get; set; }
    }

    public class AdvanceDetailInsert1
    {
        public int AdvanceDetailID { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int AdvanceId { get; set; }
        public int RefId { get; set; }
        public int TransactionId { get; set; }
        public int OPD_IPD_Id { get; set; }
        public int OPD_IPD_Type { get; set; }
        public float AdvanceAmount { get; set; }
        public float UsedAmount { get; set; }
        public float BalanceAmount { get; set; }
        public float RefundAmount { get; set; }
        public int ReasonOfAdvanceId { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public string Reason { get; set; }

    }

    public class IPPaymentInsert1
    {
        // public int PaymentId { get; set; }
        public int BillNo { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public float CashPayAmount { get; set; }
        public float ChequePayAmount { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public float CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public float AdvanceUsedAmount { get; set; }
        public int AdvanceId { get; set; }
        public int RefundId { get; set; }
        public int TransactionType { get; set; }
        public string Remark { get; set; }
        public int AddBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }

        public DateTime IsCancelledDate { get; set; }
        /*  public int CashCounterId { get; set; }
          public int IsSelfORCompany { get; set; }
          public int CompanyId { get; set; }*/

        public float NEFTPayAmount { get; set; }
        public string NEFTNo { get; set; }
        public string NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }
        public float PayTMAmount { get; set; }
        public string PayTMTranNo { get; set; }
        public DateTime PayTMDate { get; set; }


    }

    public class AdvanceHeaderUpdate
    {
      public int AdvanceId { get; set; }   
      public float AdvanceAmount { get; set; }
      
    }
}
