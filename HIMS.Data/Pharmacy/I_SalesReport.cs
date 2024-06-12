using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_SalesReport
    {

        string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);

        string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);

        string ViewSalesReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy, string htmlFilePath, string HeaderName);
        string ViewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string Header2);
        string ViewSalesReportPatientWise(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string Header2);
        string ViewSalesReturnSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string Header2);

        string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string Header2);
       
        string ViewSalesReturnReceipt(int SalesReturnID, int OP_IP_Type, string htmlFilePath, string HeaderName);
        string ViewSalesCreditReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId, string htmlFilePath, string Header2);

        string ViewSalesTaxReceipt(int SalesID, int OP_IP_Type, string htmlFilePath, string HeaderName);

        string ViewPharSalesCashBookReport(DateTime FromDate, DateTime ToDate, string PaymentMode, int StoreId, string htmlFilePath, string HeaderName);

        string ViewPharmsDailyCollectionsummaryDayandUserwise(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);

        string ViewPharOPExtcountdailyReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string HeaderName);

        string ViewPharCompanycreditlistReport(int StoreId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);


        string ViewPharcomwisepatientcreditReceipt(int StoreId, string htmlFilePath, string HeaderName);
        string ViewPharSalesstatement(int OP_IP_ID, int StoreId, string htmlFilePath, string HeaderName);
    }
}
