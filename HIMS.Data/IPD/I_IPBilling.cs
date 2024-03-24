
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

        string ViewIPBillDatewiseReceipt(int BillNo, string htmlFilePath, string HeaderName);

        string ViewIPBillWardwiseReceipt(int BillNo, string htmlFilePath, string HeaderName);

        string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string HeaderName);

        string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName);

    }
}
