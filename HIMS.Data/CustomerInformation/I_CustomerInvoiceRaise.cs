using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.CustomerInformation
{
    public  interface I_CustomerInvoiceRaise
    {
        public string CustomerInvoiceRaiseInsert(CustomerInvoiceRaiseParam CustomerInvoiceRaiseParam);

        public bool CustomerInvoiceRaiseUpdate(CustomerInvoiceRaiseParam CustomerInvoiceRaiseParam);
    }
}
