using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class PharRefundofAdvanceParams
    {
        public InsertPharRefundofAdvance InsertPharRefundofAdvance { get; set; }
        public UpdatePharAdvanceHeader UpdatePharAdvanceHeader { get; set; }
        public List<InsertPharRefundofAdvanceDetail> InsertPharRefundofAdvanceDetail { get; set; }
        public List<UpdatePharAdvanceDetailBalAmount> UpdatePharAdvanceDetailBalAmount { get; set; }
        public InsertPharPayment InsertPharPayment { get; set; }
    }
    public class InsertPharRefundofAdvance
    {
        public DateTime RefundDate { get; set; }
        public DateTime RefundTime { get; set; }
        public int BillId { get; set; }
        public long AdvanceId { get; set; }
        public int OPD_IPD_Type { get; set; }
        public long OPD_IPD_ID { get; set; }
        public long RefundAmount { get; set; }
        public String Remark { get; set; }
        public int TransactionId { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public int RefundId { get; set; }

    }


    public class UpdatePharAdvanceHeader
    {
        public int AdvanceId { get; set; }
        public long AdvanceUsedAmount { get; set; }
        public long BalanceAmount { get; set; }

    }

    public class InsertPharRefundofAdvanceDetail
    {
        //   public int AdvRefId { get; set; }
        public int AdvDetailId { get; set; }
        public DateTime RefundDate { get; set; }
        public DateTime RefundTime { get; set; }
        public long AdvRefundAmt { get; set; }
    }

    public class UpdatePharAdvanceDetailBalAmount
    {
        public int AdvanceDetailID { get; set; }
        public long BalanceAmount { get; set; }
        public long RefundAmount { get; set; }

    }


    public class InsertPharPayment
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
        //public float TDSAmount { get; set; }

    }
}
