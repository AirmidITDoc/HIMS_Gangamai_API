using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
   public  class OPbillingparams
    {
        public List<InsertPathologyReportHeader> InsertPathologyReportHeader { get; set; }

        public List<InsertRadiologyReportHeader> InsertRadiologyReportHeader { get; set; }
        public  InsertBillupdatewithbillno InsertBillupdatewithbillno { get; set; }

        public List<OpBillDetailsInsert> OpBillDetailsInsert { get; set; }
        
     //  public OPoctorShareGroupAdmChargeDoc OPoctorShareGroupAdmChargeDoc { get; set; }
        
        public OPCalDiscAmountBill OPCalDiscAmountBill { get; set; }
       
        public OPInsertPayment OPInsertPayment { get; set; }
    }
   
    public class InsertPathologyReportHeader
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

    public class InsertRadiologyReportHeader
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
    public class InsertBillupdatewithbillno
    {
       // public int BillNo { get; set; }
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
        public int CompDiscAmt { get; set; }
        public string DiscComments { get; set; }
    }

    public class OpBillDetailsInsert
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }

       // public int BillDetailId { get; set; }
    }
   
   

    public class OPoctorShareGroupAdmChargeDoc
    {
        public int BillNo { get; set; }

    }
  
    public class OPCalDiscAmountBill
    {
        public int BillNo { get; set; }

    }

   
    public class OPInsertPayment
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
}
