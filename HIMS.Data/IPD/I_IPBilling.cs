
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPBilling
    {
        public String Insert(IPBillingParams IPBillingParams);

        string ViewIPBillReceipt(int BillNo, string htmlFilePath, string HeaderName);

    }
}
