using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.OnlinePayment
{
    public class OnlinePaymentParams
    {
        public string TransactionNumber { get; set; }
        public int SequenceNumber { get; set; }
        public string AllowedPaymentMode { get; set; }
        public string MerchantStorePosCode { get; set; }
        public string Amount { get; set; }
        public string UserID { get; set; }
        public string MerchantID { get; set; }
        public string SecurityToken { get; set; }
        public string IMEI { get; set; }
        public int AutoCancelDurationInMinutes { get; set; }
    }

    public class OnlinePaymentStatusParams
    {
        public string MerchantID { get; set; }
        public string SecurityToken { get; set; }
        public string IMEI { get; set; }
        public string MerchantStorePosCode { get; set; }
        public int PlutusTransactionReferenceID { get; set; }
    }


}
