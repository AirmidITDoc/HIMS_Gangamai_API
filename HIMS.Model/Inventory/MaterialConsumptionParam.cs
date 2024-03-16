using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
   public class MaterialConsumptionParam
    {
        public InsertMaterialConsumption InsertMaterialConsumption { get; set; }
        public List<InsertMaterialConsDetail> InsertMaterialConsDetail { get; set; }
        public UpdateCurrentStock UpdateCurrentStock { get; set; }
    }
   
   public class InsertMaterialConsumption{
      
        public DateTime ConsumptionDate { get; set; }
        public DateTime ConsumptionTime { get; set; }
        public int FromStoreId { get; set; }
        public long LandedTotalAmount { get; set; }
        public long PurchaseTotal { get; set; }

        public long MRPTotal { get; set; }
        public string Remark { get; set; }
        public long Addedby { get; set; }

        public int MaterialConsumptionId { get; set; }

    }



    public class InsertMaterialConsDetail
    {
        public long MaterialConsumptionId { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }

        public DateTime BatchExpDate { get; set; }
        public long Qty { get; set; }
        public long PerUnitLandedRate { get; set; }
        public long ParUnitPurchaseRate { get; set; }
        public long PerUnitMRPRate { get; set; }
        public long LandedRateTotalAmount { get; set; }
        public long PurchaseRateTotalAmount { get; set; }
        public long MRPTotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remark { get; set; }
       
    }


    public class UpdateCurrentStock
    {
        public int ItemId { get; set; }
        public long IssueQty { get; set; }
        public int StoreID { get; set; }
        public int StkId { get; set; }
    }
}
