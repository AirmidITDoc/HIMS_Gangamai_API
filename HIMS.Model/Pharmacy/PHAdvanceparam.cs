using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class PHAdvanceparam
    {

        public InsertPHAdvance InsertPHAdvance { get; set; }

        public List<InsertPHAdvanceDetail> InsertPHAdvanceDetail { get; set; }
        public UpdatePHAdvance UpdatePHAdvance { get; set; }

        public InsertPHPayment InsertPHPayment { get; set; }
    }

    public class InsertPHAdvance
    {

        public DateTime Date { get; set; }
        public int RefId { get; set; }
        public int OPD_IPD_Type { get; set; }

        public int OPD_IPD_Id { get; set; }
        public long AdvanceAmount { get; set; }

        public long AdvanceUsedAmount { get; set; }
        public long BalanceAmount { get; set; }
        public int AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }

        public int AdvanceID { get; set; }

    }


    public class InsertPHAdvanceDetail
    {

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public int AdvanceId { get; set; }
        public int RefId { get; set; }
        public int TransactionId { get; set; }
        public int OPD_IPD_Type { get; set; }

        public int OPD_IPD_Id { get; set; }
        public long AdvanceAmount { get; set; }

        public long UsedAmount { get; set; }
        public long BalanceAmount { get; set; }

        public long RefundAmount { get; set; }
        public int ReasonOfAdvanceId { get; set; }
        public int AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }

        public String Reason { get; set; }

        public int StoreId { get; set; }
        public int AdvanceDetailID { get; set; }

    }
    public class UpdatePHAdvance
    {
    
        public long AdvanceAmount { get; set; }
        public long BalanceAmount { get; set; }

        public int AdvanceId { get; set; }
    }

    public class InsertPHPayment
    {
        public long BillNo { get; set; }

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
        public long AdvanceId { get; set; }
        public long RefundId { get; set; }
        public long TransactionType { get; set; }
        public string Remark { get; set; }
        public long AddBy { get; set; }
        public bool IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
      //  public long OPD_IPD_Type { get; set; }
        public float NEFTPayAmount { get; set; }
        public string NEFTNo { get; set; }
        public string NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }
        public float PayTMAmount { get; set; }
        public string PayTMTranNo { get; set; }
        public DateTime PayTMDate { get; set; }
       // public long PaymentId { get; set; }
    }
}