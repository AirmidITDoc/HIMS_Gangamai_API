using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IPRefundofBilllparams
    {
        public InsertIPRefundofNew InsertIPRefundofNew { get; set; }

        public List<InsertRefundDetails> InsertRefundDetails { get; set; }
      
        public List<UpdateAddChargesDetails> UpdateAddChargesDetails { get; set; }
    //    public IPDocShareGroupAdmRefundofBillDoc IPDocShareGroupAdmRefundofBillDoc { get; set; }

        public  IPDInsertPayment IPDInsertPayment { get; set; }

    }

    public class InsertIPRefundofNew
    {
        public String RefundNo { get; set; }
        public DateTime RefundDate { get; set; }
        public DateTime RefundTime { get; set; }
        public int BillId { get; set; }
        public int AdvanceId { get; set; }
        public int OPD_IPD_Type { get; set; }
        public int OPD_IPD_ID { get; set; }
        public float RefundAmount { get; set; }
        public string Remark { get; set; }
        public int TransactionId { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
      //  public Boolean IsRefundFlag { get; set; }

        public int RefundId { get; set; }

    }


    public class InsertRefundDetails
    {
        public int RefundID { get; set; }
        public int ServiceId { get; set; }
        public float ServiceAmount { get; set; }
        public float RefundAmount { get; set; }
        public int DoctorId { get; set; }
        public string Remark { get; set; }
        public int AddBy { get; set; }
        public int ChargesId { get; set; }

    }

     public class UpdateAddChargesDetails
    {
        public int ChargesId { get; set; }
        public int RefundAmount { get; set; }

    }


    public class IPDocShareGroupAdmRefundofBillDoc
    {
        public int RefundId { get; set; }

    }

    public class IPDInsertPayment
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

