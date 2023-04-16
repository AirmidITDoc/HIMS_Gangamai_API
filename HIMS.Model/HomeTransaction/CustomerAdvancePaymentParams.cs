using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class CustomerAdvancePaymentParams
    {
           public CustomerAdvancePaymentInsert CustomerAdvancePaymentInsert  { get; set; }
            public CustomerAdvancePaymentUpdate CustomerAdvancePaymentUpdate  { get; set; }
     }

        public class CustomerAdvancePaymentInsert
    {
            public DateTime TranDate { get; set; }
            public DateTime TranTime { get; set; }
            public int CustomerID { get; set; }
            public string TransctionType { get; set; }
            public int Amount { get; set; }
            public string PaymentStatus { get; set; }
            public long AddedBy { get; set; }
            public long ProjectId { get; set; }
            public long TranTypeId { get; set; }

    }
         public class CustomerAdvancePaymentUpdate
           {
            public int @CustTranId { get; set; }
            public DateTime TranDate { get; set; }
            public DateTime TranTime { get; set; }
            public int CustomerID { get; set; }
            public string TransctionType { get; set; }
            public int Amount { get; set; }
            public string PaymentStatus { get; set; }
            public long UpdatedBy { get; set; }
            public long ProjectId { get; set; }
            public long TranTypeId { get; set; }

    }
    
}
