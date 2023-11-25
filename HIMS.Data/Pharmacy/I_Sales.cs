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
        string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath);
        string GetFilePath();
    }
}
