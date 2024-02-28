using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class PharmPaymentMode
    {
        public PaymentModeUpdate PaymentModeUpdate { get; set; }
    }

   public class PaymentModeUpdate
    {
        public int PaymentId { get; set; }
        public long CashPayAmt { get; set; }
        public float CardPayAmt { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public float ChequePayAmt { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeBankName { get; set; }
        public long NEFTPayAmount { get; set; }
        public string NEFTNo { get; set; }
        public string NEFTBankMaster { get; set; }
        public long PayTMAmount { get; set; }
        public string PayTMTranNo { get; set; }
    }
}
