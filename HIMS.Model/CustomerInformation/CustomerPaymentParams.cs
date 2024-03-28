using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CustomerPaymentParams
    {
        public CustomerPaymentInsert CustomerPaymentInsert { get; set; }

        public CustomerPaymentUpdate CustomerPaymentUpdate { get; set; }
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

}

