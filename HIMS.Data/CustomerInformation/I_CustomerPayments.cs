using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.CustomerPayment
{
    public interface I_CustomerPayments
    {
        public string CustomerPaymentInsert(CustomerPaymentParams CustomerPaymentParams);

        public bool CustomerPaymentUpdate(CustomerPaymentParams CustomerPaymentParams);

    }
}
