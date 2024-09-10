using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class SupplierPayment
    {
        public TGRNsupppayInsert TGRNsupppayInsert { get; set; }
        public List<TGRNHeaderPayStatus> TGRNHeaderPayStatus { get; set; }
        public List<TSupPayDetPayStatus> TSupPayDetPayStatus { get; set; }
    }

    public class TGRNsupppayInsert
    {
        public int SupPayId { get; set; }
        public DateTime SupPayTime { get; set; }
        public DateTime SupPayDate { get; set; }
        public int GrnId { get; set; }
        public long CashPayAmount { get; set; }
        public float ChequePayAmount { get; set; }
        public DateTime ChequePayDate { get; set; }
        public string ChequeBankName { get; set; }
        public string ChequeNo { get; set; }
        public string Remark { get; set; }
        public int IsAddedBy { get; set; }
        public int IsUpdatedBy { get; set; }
        public int IsCancelled { get; set; }
        public int IsCancelledBy { get; set; }
        public DateTime IsCancelledDate { get; set; }
        public string PartyReceiptNo { get; set; }
        public float NEFTPayAmount { get; set; }
        public string NEFTNo { get; set; }
        public string NEFTBankMaster { get; set; }
        public DateTime NEFTDate { get; set; }

        public float CardPayAmt { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardPayDate { get; set; }
        public float PayTMAmount { get; set; }
        public string PayTMTranNo { get; set; }

       public DateTime PayTMDate { get; set; }

    }
    public class TGRNHeaderPayStatus
    {
      
             public int GrnId { get; set; }
        public float PaidAmount { get; set; }
        public float BalAmount { get; set; }

    }

    public class TSupPayDetPayStatus
    {
        public int SupPayId { get; set; }
        public int SupGrnId { get; set; }
    }
}
