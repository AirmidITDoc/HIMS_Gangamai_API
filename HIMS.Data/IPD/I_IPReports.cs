using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPReports
    {
        string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string HeaderName);

        string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string HeaderName);

        string ViewOPIPBillSummaryReceipt(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

        string ViewIPCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);

    }
}
