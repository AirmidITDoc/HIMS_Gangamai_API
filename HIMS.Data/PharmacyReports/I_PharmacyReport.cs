using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.Data.PharmacyReports
{
    public interface I_PharmacyReports
    {
        string ViewPharmacyDailycollectionReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName);
        string ViewSCHEDULEH1Report(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId, string htmlFilePath, string HeaderName);
        string ViewSCHEDULEH1SalesSummaryReport(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId,  string htmlFilePath, string HeaderName);
        string ViewSalesH1DrugCountReport(DateTime FromDate, DateTime ToDate, int StoreId,  string htmlFilePath, string HeaderName);
        //string ViewSalesReturnH1DrugCountReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string HeaderName);
        string ViewWardWiseHighRiskDrugList(DateTime FromDate, DateTime ToDate, int StoreId,  string htmlFilePath, string HeaderName);
        string ViewPurchaseReOrderList(int StoreId, DateTime FromDate, DateTime ToDate,   string htmlFilePath, string HeaderName);
        string ViewPharmacyBillSummaryReport(int StoreId, DateTime FromDate, DateTime ToDate,  string htmlFilePath, string HeaderName);

        string ViewItemWiseDailySalesReport(DateTime FromDate, DateTime ToDate,int ItemId,int RegNo, int StoreId, string htmlFilePath, string HeaderName);


        string ViewDoctorWiseProfitReport( DateTime FromDate, DateTime ToDate,int DoctorId, int OP_IP_Type,string htmlFilePath, string HeaderName);
        string ViewDoctorWiseSalesReport(DateTime FromDate, DateTime ToDate, int StoreId, int DoctorId, int OP_IP_Type, string htmlFilePath, string HeaderName);
        string ViewPharmacySalesDoctorWiseProfitDetailsReportOPIP(DateTime FromDate, DateTime ToDate, int StoreId, int DoctorId,int OP_IP_Type, string htmlFilePath, string HeaderName);
        string ViewPharmacySalesDoctorWiseProfitReportSummaryOPIP(DateTime FromDate, DateTime ToDate, int DoctorId, int OP_IP_Type,string htmlFilePath, string HeaderName);
        string ViewSalesDraftBill(int DSalesId, string htmlFilePath, string HeaderName);
    }
}
