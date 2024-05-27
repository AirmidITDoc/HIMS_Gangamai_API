using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
   public class GRNReturnParam
    {

        public GRNReturnSave GRNReturnSave { get; set; }
        public List<GRNReturnDetailSave> GRNReturnDetailSave { get; set; }
        public List<GRNReturnUpdateCurrentStock> GRNReturnUpdateCurrentStock { get; set; }
        public List<GRNReturnUpateReturnQty> GRNReturnUpateReturnQty { get; set; }
        public UpdateGRNReturnVerifyStatus UpdateGRNReturnVerifyStatus { get; set; }
    }


    public class GRNReturnSave
    {

        public long GrnId { get; set; }
        public DateTime GRNReturnDate { get; set; }
        public DateTime GRNReturnTime { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public float TotalAmount { get; set; }
        public float GrnReturnAmount { get; set; }
        public float TotalDiscAmount { get; set; }
        public float TotalVATAmount { get; set; }
        public float TotalOtherTaxAmount { get; set; }
        public float TotalOctroiAmount { get; set; }
        public float NetAmount { get; set; }
        public bool Cash_Credit { get; set; }
        public string Remark { get; set; }
        public bool IsVerified { get; set; }
        public bool IsClosed { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public string GrnType { get; set; }
        public bool IsGrnTypeFlag { get; set; }
        public int GRNReturnId { get; set; }
       
    }

    public class GRNReturnDetailSave
    {
       
        public long GrnReturnId { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpiryDate { get; set; }
        public float ReturnQty { get; set; }
        public float LandedRate { get; set; }
        public float Mrp { get; set; }
        public float UnitPurchaseRate { get; set; }
        public float VatPercentage { get; set; }
        public float VatAmount { get; set; }
        public float TaxAmount { get; set; }
        public float OtherTaxAmount { get; set; }
      
        public float OctroiPer { get; set; }
        public float OctroiAmt { get; set; }
        public float LandedTotalAmount { get; set; }
        public float MrpTotalAmount { get; set; }
        public float PurchaseTotalAmount { get; set; }
        public int Conversion { get; set; }
        public string Remarks { get; set; }
        public int StkId { get; set; }
        public float Cf { get; set; }
        public float TotalQty { get; set; }
        public long GrnId { get; set; }

    }

    public class GRNReturnUpdateCurrentStock
    {
        public int ItemId { get; set; }
        public float IssueQty { get; set; }
        public int StkId { get; set; }
        public int StoreID { get; set; }
    }

    public class GRNReturnUpateReturnQty
    {
        public int GRNDetID { get; set; }
        public float ReturnQty { get; set; }
       
    }

    public class UpdateGRNReturnVerifyStatus
    {
        public long GRNReturnId { get; set; }
        public long IsVerifiedUserId { get; set; }
    }
}
