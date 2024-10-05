
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPBilling
    {
        public String Insert(IPBillingParams IPBillingParams);
        public String InsertCashCounter(IPBillingParams IPBillingParams);
        string ViewIPBillReceipt(int BillNo, string htmlFilePath, string HeaderName);

        string ViewIPBillReceiptclasswise(int BillNo, string htmlFilePath, string HeaderName);

        string ViewIPBillDatewiseReceipt(int BillNo, string htmlFilePath, string HeaderName);

        string ViewIPBillWardwiseReceipt(int BillNo, string htmlFilePath, string HeaderName);

        public bool BillDiscountAfterUpdate(BillDiscountAfterParams billDiscountAfterParams);
        string ViewIPBillReceiptclassServicewise(int BillNo, string htmlFilePath, string HeaderName);

    }
}
