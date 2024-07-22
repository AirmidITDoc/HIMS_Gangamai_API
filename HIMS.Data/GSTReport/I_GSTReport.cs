using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HIMS.Data.GSTReports
{
    public interface I_GSTReport
    {
        string ViewSalesProfitSummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string HeaderName);
       
        string ViewSalesProfitBillReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewSalesProfitItemWiseSummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
    }
}
