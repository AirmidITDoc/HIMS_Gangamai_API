using HIMS.Model.Pharmacy;
using System;

namespace HIMS.Data.Pharmacy
{
    public interface I_Sales
    {
        public string InsertSales(SalesParams SalesParams);
        public string InsertSalesWithCredit(SalesCreditParams salesCreditParams);
        public bool PaymentSettlement(SalesParams SalesParams);
        String ViewBill(int SalesID, int OP_IP_Type,string htmlFilePath);
        string GetFilePath();
    }
}
