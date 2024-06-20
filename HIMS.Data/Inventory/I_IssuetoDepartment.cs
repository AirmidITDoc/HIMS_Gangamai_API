using HIMS.Model.Inventory;
using System;

namespace HIMS.Data.Inventory
{
    public interface I_IssuetoDepartment
    {
        public string InsertIssuetoDepartment(IssuetoDepartmentParams issuetoDepartmentParams);

        string ViewIssuetoDeptIssuewise(int IssueId, string htmlFilePath, string HeaderName);

        string ViewIssuetoDeptItemwise(DateTime FromDate, DateTime ToDate, int FromStoreId,int ToStoreId,int ItemId, string htmlFilePath, string HeaderName);
        string ViewIssuetodeptsummary(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string HeaderName);

        string ViewItemwiseSupplierlist(int StoreId, int SupplierId,int ItemId , DateTime FromDate, DateTime ToDate, string htmlFilePath, string HeaderName);
        string ViewReturnfromDeptReturnIdwise(int ReturnId, string htmlFilePath, string HeaderName);


        string ViewReturnfrdeptdatewise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string HeaderName);


        public string ViewNonMovingItem(int NonMovingDay, int StoreId, string htmlFilePath, string HeaderName);

        public string ViewExpItemlist(int ExpMonth, int ExpYear,int StoreID, string htmlFilePath, string HeaderName);
    }
}
