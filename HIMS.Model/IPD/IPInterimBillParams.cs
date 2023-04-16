using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPInterimBillParams
    {
        public InterimBillChargesUpdate InterimBillChargesUpdate { get; set; }
        public InsertBillUpdateBillNo1 InsertBillUpdateBillNo1 { get; set; }
       
        public List<BillDetailsInsert1> BillDetailsInsert1 { get; set; }
        
        public IPIntremPaymentInsert IPIntremPaymentInsert { get; set; }
      //  public BillIPInterimBillingUpdate BillIPInterimBillingUpdate { get; set; }        
    }
    public class InterimBillChargesUpdate
    {
        public int ChargesID { get; set; }
    }

    public class InsertBillUpdateBillNo1
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
        public bool IsSettled { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsFree { get; set; }
        public int CompanyId { get; set; }
        public int TariffId { get; set; }
        public int UnitId { get; set; }
        public int InterimOrFinal { get; set; }
        public int CompanyRefNo { get; set; }
        public int ConcessionAuthorizationName { get; set; }
        public float TaxPer { get; set; }
        public float TaxAmount { get; set; }
        public string DiscComments { get; set; }

        public float CompDiscAmt {get; set;}
}
    public class BillDetailsInsert1
    {
        public int BillNo { get; set; }
        public int ChargesID { get; set; }
    }
    public class IPIntremPaymentInsert
    {
        /*   public int PaymentId { get; set; }
           public int BillNo { get; set; }
          // public string ReceiptNo { get; set; }
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
           public int OPD_IPD_Type { get; set; }
           public float NEFTPayAmount { get; set; }
           public string NEFTNo { get; set; }
           public string NEFTBankMaster { get; set; }
           public DateTime NEFTDate { get; set; }
           public float PayTMAmount { get; set; }
           public string PayTMTranNo { get; set; }
           public DateTime PayTMDate { get; set; }*/
        // public int PaymentId { get; set; }
        public int BillNo { get; set; }
        public String ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public long CashPayAmount { get; set; }
        public long ChequePayAmount { get; set; }
        public String ChequeNo { get; set; }
        public String BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public long CardPayAmount { get; set; }
        public String CardNo { get; set; }
        public String CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public long AdvanceUsedAmount { get; set; }
        public int AdvanceId { get; set; }
        public int RefundId { get; set; }
        public int TransactionType { get; set; }
        public String Remark { get; set; }
        public int AddBy { get; set; }
        public int IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        // public int OPD_IPD_Type { get; set; }
        public long NEFTPayAmount { get; set; }
        public String NEFTNo { get; set; }
        public String NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }
        public long PayTMAmount { get; set; }
        public String PayTMTranNo { get; set; }
        public DateTime PayTMDate { get; set; }
    }
    public class BillIPInterimBillingUpdate
    {
        public int BillNo { get; set; }
    }
}
