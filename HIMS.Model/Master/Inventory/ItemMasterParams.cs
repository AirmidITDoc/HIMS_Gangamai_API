using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ItemMasterParams 
    {
        public InsertItemMaster InsertItemMaster { get; set; }
        public UpdateItemMaster UpdateItemMaster { get; set; }
        public DeleteAssignItemToStore DeleteAssignItemToStore { get; set; }
        public List<InsertAssignItemToStore> InsertAssignItemToStore { get; set; }
        
    }

    public class InsertItemMaster
    {
        public string ItemName { get; set; }
        // public string ItemShortName { get; set; }
        public long ItemTypeId { get; set; }
        public long ItemCategoryId { get; set; }
        public long ItemGenericNameId { get; set; }
        public long ItemClassId { get; set; }
        public long PurchaseUOMId { get; set; }
        public long StockUOMId { get; set; }
        public string ConversionFactor { get; set; }
        public long CurrencyId { get; set; }
        public float TaxPer { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public bool IsBatchRequired { get; set; }
        public float MinQty { get; set; }
        public float MaxQty { get; set; }
        public float Reorder { get; set; }

        public bool IsNursingFlag { get; set; }
        public string HSNcode { get; set; }
        public float CGST { get; set; }
        public float SGST { get; set; }
        public float IGST { get; set; }

        public bool IsNarcotic { get; set; }
        public long ManufId { get; set; }

        public string ProdLocation { get; set; }
        public bool IsH1Drug { get; set; }
        public bool IsScheduleH { get; set; }
        public bool IsHighRisk { get; set; }
        public bool IsScheduleX { get; set; }
        public bool IsLASA { get; set; }
        public bool IsEmgerency { get; set; }
        public int ItemID { get; set; }

        public int DrugType {get;set;}

    public string DrugTypeName { get; set; }
    public int ItemCompnayId { get; set; }
    public DateTime IsCreatedBy { get; set; }

   
    }


   
    public class UpdateItemMaster
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemShortName { get; set; }
        public long ItemTypeID { get; set; }
        public long ItemCategoryId { get; set; }
        public long ItemGenericNameId { get; set; }
        public long ItemClassId { get; set; }
        public long PurchaseUOMId { get; set; }
        public long StockUOMId { get; set; }
        public string ConversionFactor { get; set; }
        public long CurrencyId { get; set; }
        public float TaxPer { get; set; }
        public bool IsDeleted { get; set; }
        public long UpDatedBy { get; set; }
        public bool IsBatchRequired { get; set; }
        public float MinQty { get; set; }
        public float MaxQty { get; set; }
        public float Reorder { get; set; }

        public bool IsNursingFlag { get; set; }
        public string HSNcode { get; set; }
        public float CGST { get; set; }
        public float SGST { get; set; }
        public float IGST { get; set; }

        public bool IsNarcotic { get; set; }
        public long ManufId { get; set; }

        public string ProdLocation { get; set; }
        public bool IsH1Drug { get; set; }
        public bool IsScheduleH { get; set; }
        public bool IsHighRisk { get; set; }
        public bool IsScheduleX { get; set; }
        public bool IsLASA { get; set; }
        public bool IsEmgerency { get; set; }
       

    }

    public class DeleteAssignItemToStore
    {
        public long ItemId { get; set; }
    }
    public class InsertAssignItemToStore 
    {
        public long StoreId { get; set; }
        public long ItemId { get; set; }
    }
   
}
