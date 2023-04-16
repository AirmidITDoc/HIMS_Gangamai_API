using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
  
        public class IPRefundofAdvanceParams
        {
            public InsertIPRefundofAdvance InsertIPRefundofAdvance { get; set; }

            public UpdateAdvanceHeader UpdateAdvanceHeader { get; set; }
            public InsertIPRefundofAdvanceDetail InsertIPRefundofAdvanceDetail { get; set; }
            public UpdateAdvanceDetailBalAmount UpdateAdvanceDetailBalAmount { get; set; }
            public InsertPayment InsertPayment { get; set; }

        
    }

           public class InsertIPRefundofAdvance
        {
            public DateTime RefundDate { get; set; }
            public DateTime RefundTime { get; set; }
          //  public String RefundNo { get; set; }
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
   

          public class UpdateAdvanceHeader
         {
            public int AdvanceId { get; set; }
            public long AdvanceUsedAmount { get; set; }
            public long BalanceAmount { get; set; }

        }
   
         public class InsertIPRefundofAdvanceDetail
        {
         //   public int AdvRefId { get; set; }
            public int AdvDetailId { get; set; }
            public DateTime RefundDate { get; set; }
            public DateTime RefundTime { get; set; }
            public long AdvRefundAmt { get; set; }
        }

          public class UpdateAdvanceDetailBalAmount
        {
        public int AdvanceDetailID { get; set; }
        public long BalanceAmount { get; set; }
        public long RefundAmount { get; set; }
       
        }
    
       
        public class InsertPayment
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