using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
   public class ReturnfrdeptParam
    {
        public InsertReturnDepartmentHeader InsertReturnDepartmentHeader { get; set; }
        public List<InsertReturnDepartmentDetail> InsertReturnDepartmentDetail { get; set; }
    }



public class InsertReturnDepartmentHeader
    {
        public DateTime ReturnDate { get; set; }
        public DateTime ReturnTime { get; set; }
        public int FromStoreId { get; set; }
        public int ToStoreId { get; set; }
        public float LandedRateTotalAmount { get; set; }
        public float MRPTotalAmount { get; set; }
        public float PurchaseTotalAmount { get; set; }
        public float TotalVATAmount { get; set; }
        public string Remark { get; set; }
        public int AddedBy { get; set; }
      
        public int ReturnId { get; set; }
    }
  public class InsertReturnDepartmentDetail
    {

        public int ReturnId { get; set; }
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }

        public int BalQty { get; set; }
        public int ReturnQty { get; set; }

        public int RemainingQty { get; set; }
        public float UnitLandedRate { get; set; }
        public float TotalLandedRate { get; set; }
        public float UnitPurchaseRate { get; set; }
        public float TotalPurAmount { get; set; }
        public float UnitMRP { get; set; }
        public float TotalMRPAmount { get; set; }
        public int VatPer { get; set; }
        public float VatAmount { get; set; }
        public string Remark { get; set; }
    }
}
