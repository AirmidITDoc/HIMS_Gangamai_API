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


        string ViewPurchaseGSTReportSupplierWiseGST(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewPurchaseGSTReportSupplierWiseWithoutGST(DateTime FromDate, DateTime ToDate, int StoreID, string htmlFilePath, string htmlHeader);
        string ViewPurchaseGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewPurchaseGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewPurchaseRetumGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewPurchaseReturnGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        //string ViewPurchaseGSTSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewSalesGSTReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewSalesGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewSalesReturnGSTReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewSalesReturnGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        //string ViewSalesGSTSummaryConsolidated(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewHSNCodeWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        //string ViewGSTB2CSReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        //string ViewGSTB2GSReportConsolidated(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader);
        string ViewGSTRZAPurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);
        string ViewGSTR2ASupplierWisePurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader);

        string ViewSalesProfitDetailDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader);
        string ViewSalesProfitSummaryDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader);

    }
}
