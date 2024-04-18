using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class IssueToDepartmentIndentParam
    {

        public InsertIssuetoDepartmentHeader1 insertIssuetoDepartmentHeader1 { get; set; }
        public List<InsertIssuetoDepartmentDetail1> InsertIssuetoDepartmentDetail1 { get; set; }
        public List<updateissuetoDepartmentStock1> UpdateissuetoDepartmentStock1 { get; set; }

        public Update_IndentHeader_Status Update_IndentHeader_Status { get; set; }
        public List<UpdateIndentStatusIndentDetails> updateIndentStatusIndentDetails { get; set; }




    }
    public class InsertIssuetoDepartmentHeader1
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
    public class InsertIssuetoDepartmentDetail1
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
    public class updateissuetoDepartmentStock1
    {
        public int ItemId { get; set; }
        public int IssueQty { get; set; }
        public int StkId { get; set; }
        public int StoreID { get; set; }
    }

    public class UpdateIndentStatusIndentDetails
    {
        public long IndentId { get; set; }
        public long IndDetID { get; set; }
        public bool IsClosed { get; set; }
        public float IndQty { get; set; }
    }
    public class Update_IndentHeader_Status
    {
        public long IndentId { get; set; }
        public bool IsClosed { get; set; }
    }
}

