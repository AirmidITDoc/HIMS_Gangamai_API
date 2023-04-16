using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPBillingParams
    {
        //  public List<IpInsertPathologyReportHeader> IpInsertPathologyReportHeader { get; set; }

        //  public List<IpInsertRadiologyReportHeader> IpInsertRadiologyReportHeader { get; set; }
        public InsertBillUpdateBillNo InsertBillUpdateBillNo { get; set; }
        public List<BillDetailsInsert> BillDetailsInsert { get; set; }
        public Cal_DiscAmount_IPBill Cal_DiscAmount_IPBill { get; set; }
        public AdmissionIPBillingUpdate AdmissionIPBillingUpdate { get; set; }
        public IPInsertPayment IPInsertPayment { get; set; }

        public IPBillBalAmount IPBillBalAmount { get; set; }


        public List<IPAdvanceDetailUpdate> IPAdvanceDetailUpdate { get; set; }

        public IPAdvanceHeaderUpdate IPAdvanceHeaderUpdate { get; set; }

           


    }


    public class IpInsertPathologyReportHeader
    {
        public DateTime PathDate { get; set; }
        public DateTime PathTime { get; set; }
        public Boolean OPD_IPD_Type { get; set; }
        public int OPD_IPD_Id { get; set; }
        public int PathTestID { get; set; }
        public int AddedBy { get; set; }
        public int ChargeID { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean IsPrinted { get; set; }
        public Boolean IsSampleCollection { get; set; }
        public Boolean TestType { get; set; }
    }

    public class IpInsertRadiologyReportHeader
    {
        public DateTime RadDate { get; set; }
        public DateTime RadTime { get; set; }
        public Boolean OPD_IPD_Type { get; set; }
        public int OPD_IPD_Id { get; set; }
        public int RadTestID { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int ChargeID { get; set; }
        public Boolean IsPrinted { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean TestType { get; set; }
    }
    public class InsertBillUpdateBillNo
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
       
        public float CompDiscAmt { get; set; }
        public string DiscComments { get; set; }

    }

    public class BillDetailsInsert
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class Cal_DiscAmount_IPBill
    {
        public int BillNo { get; set; }

    }

    public class AdmissionIPBillingUpdate
    {
        public int AdmissionID { get; set; }
    }
    public class IPInsertPayment
    {
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

    public class IPBillBalAmount
    {
        public int BillNo { get; set; }
        public long BillBalAmount { get; set; }

    }

    public class IPAdvanceDetailUpdate
    {
        public long AdvanceDetailID { get; set; }
        public long UsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }


    public class IPAdvanceHeaderUpdate
    {

        public long AdvanceId { get; set; }
        public long AdvanceUsedAmount { get; set; }
        public long BalanceAmount { get; set; }
    }


   
   

}