using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CustomerPaymentParams
    {
        public CustomerPaymentInsert CustomerPaymentInsert { get; set; }
        public CustomerPaymentUpdate CustomerPaymentUpdate { get; set; }
        public CustomerPaymentCancel CustomerPaymentCancel { get; set; }
    }
    public class CustomerPaymentInsert
    {
        public long PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public long CustomerId { get; set; }
        public long Amount { get; set; }
        public string Comments { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedByDateTime { get; set; }
        public long TranId { get; set; }
        public string TranType { get; set; }
    }
    public class CustomerPaymentUpdate
    {
        public long PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentTime { get; set; }
        public long CustomerId { get; set; }
        public long Amount { get; set; }
        public string Comments { get; set; }
        public long CreatedBy { get; set; }

    }

    public class CustomerPaymentCancel
    {
        public long PaymentId { get; set; }
        public long IsCancelledBy { get; set; }

    }


}

