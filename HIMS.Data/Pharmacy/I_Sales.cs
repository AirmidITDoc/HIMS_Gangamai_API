using HIMS.Model.Pharmacy;
using System;

namespace HIMS.Data.Pharmacy
{
    public interface I_Sales
    {
        public string InsertSales(SalesParams SalesParams);
        public string InsertSalesDraftBill(SalesParams SalesParams);
        public string InsertSalesWithCredit(SalesCreditParams salesCreditParams);
        public bool PaymentSettlement(SalesParams SalesParams);
        string ViewBill(int SalesID, int OP_IP_Type,string htmlFilePath);
        string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);

        string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath,string HeaderName);

        string ViewSalesReport(DateTime FromDate, DateTime ToDate,string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy, string htmlFilePath, string HeaderName);
        string ViewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId,string htmlFilePath, string Header2);
        string ViewSalesReportPatientWise(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId,string htmlFilePath, string Header2);
        string ViewSalesReturnSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId,string htmlFilePath, string Header2);

        string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string Header2);
        string ViewSalesCreditReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber,int CreditReasonId, int StoreId, string htmlFilePath, string Header2);


        string ViewSalesTaxReceipt(int SalesID, int OP_IP_Type, string htmlFilePath, string HeaderName);

        string ViewSalesReturnReceipt(int SalesID, int OP_IP_Type, string htmlFilePath, string HeaderName);

        string ViewPurchaseorderReceipt(int PurchaseID, string htmlFilePath, string HeaderName);

        string ViewPharmsDailyCollectionsummaryDayandUserwise(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);
        
        string ViewOPBillingpatientwiseReport(string PBillNo,string htmlFilePath, string Header2);
        
        string GetFilePath();
    }
}
