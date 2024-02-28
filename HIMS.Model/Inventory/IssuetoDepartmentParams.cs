using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class IssuetoDepartmentParams
    {
        public InsertIssuetoDepartmentHeader InsertIssuetoDepartmentHeader { get; set; }
        public List<InsertIssuetoDepartmentDetail> InsertIssuetoDepartmentDetail { get; set; }
        public List<updateissuetoDepartmentStock> updateissuetoDepartmentStock { get; set; }

    }
    public class InsertIssuetoDepartmentHeader
    {
        public DateTime IssueDate { get; set; }
        public DateTime IssueTime { get; set; }
        public int FromStoreId { get; set; }
        public int ToStoreId { get; set; }
        public float TotalAmount { get; set; }
        public float TotalVatAmount { get; set; }
        public float NetAmount { get; set; }
        public string Remark { get; set; }
        public int Addedby { get; set; }
        public bool IsVerified { get; set; }
        public bool Isclosed { get; set; }
        public int IndentId { get; set; }
        public int IssueId { get; set; }
    }
    public class InsertIssuetoDepartmentDetail
    {
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public int IssueQty { get; set; }
        public float PerUnitLandedRate { get; set; }
        public float LandedTotalAmount { get; set; }
        public float UnitMRP { get; set; }
        public float MRPTotalAmount { get; set; }
        public float UnitPurRate { get; set; }
        public float PurTotalAmount { get; set; }       
        public int VatPercentage { get; set; }
        public float VatAmount { get; set; }
        public int StkId { get; set; }

    }
    public class updateissuetoDepartmentStock
    {
        public int ItemId { get; set; }
        public int IssueQty { get; set; }
        public int StkId { get; set; }
        public int StoreID { get; set; }
    }
}
