using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Inventory;

namespace HIMS.Data.Inventory
{
    public interface I_Indent
    {
        public string Insert(IndentParams indentparams);
        public bool Update(IndentParams indentParams);
        public bool IndentVerify(IndentParams indentParams);

        string ViewCurrentStock(string ItemName, int StoreId, string htmlFilePath, string HeaderName);

        string ViewItemwiseStock( DateTime FromDate, DateTime todate,int StoreId, string htmlFilePath, string HeaderName);

        string ViewDaywisestock(DateTime LedgerDate,int StoreId, string htmlFilePath, string HeaderName);

        string ViewItemWisePurchase(DateTime FromDate, DateTime todate,int StoreId, string htmlFilePath, string HeaderName);

    }
}
