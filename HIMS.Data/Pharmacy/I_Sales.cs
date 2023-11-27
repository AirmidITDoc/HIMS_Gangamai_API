using HIMS.Model.Pharmacy;
using System;

namespace HIMS.Data.Pharmacy
{
    public interface I_Sales
    {
        public string InsertSales(SalesParams SalesParams);
        public string InsertSalesWithCredit(SalesCreditParams salesCreditParams);
        public bool PaymentSettlement(SalesParams SalesParams);
        string ViewBill(int SalesID, int OP_IP_Type,string htmlFilePath);
        string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);

        string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath,string HeaderName);

        string ViewSalesReport(DateTime FromDate, DateTime ToDate,string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy, string htmlFilePath, string HeaderName);
        string ViewSalesReportCharitableTrust(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId,string htmlFilePath, string Header2);
        string ViewSalesReportPatientWiseCharitableTrust(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId,string htmlFilePath, string Header2);
        string ViewSalesReturnReportCharitableTrust(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId,string htmlFilePath, string Header2);

        string ViewSalesReturnSummaryReportCharitableTrust(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string Header2);
        string ViewSalesCreditReportCharitableTrust(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber,int CreditReasonId, int StoreId, string htmlFilePath, string Header2);
        string GetFilePath();
    }
}
