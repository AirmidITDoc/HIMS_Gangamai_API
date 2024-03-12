using HIMS.Model.Inventory;
using System;

namespace HIMS.Data.Inventory
{
    public interface I_IssuetoDepartment
    {
        public string InsertIssuetoDepartment(IssuetoDepartmentParams issuetoDepartmentParams);

        string ViewIssuetoDeptIssuewise(int IssueId, string htmlFilePath, string HeaderName);

        string ViewReturnfromDeptReturnIdwise(int ReturnId, string htmlFilePath, string HeaderName);


        string ViewReturnfrdeptdatewise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string HeaderName);

    }
}
