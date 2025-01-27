using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
   public class MaterialConsumptionParam
    {
        public InsertMaterialConsumption InsertMaterialConsumption { get; set; }
        public List<InsertMaterialConsDetail> InsertMaterialConsDetail { get; set; }
        public List<UpdateCurrentStock> UpdateCurrentStock { get; set; }
    }
   
   public class InsertMaterialConsumption{
        public int MaterialConsumptionId { get; set; }
        public int ConsumptionNo { get; set; }
        public DateTime ConsumptionDate { get; set; }
        public DateTime ConsumptionTime { get; set; }
        public int FromStoreId { get; set; }
        public float LandedTotalAmount { get; set; }
        public float PurchaseTotal { get; set; }

        public float MRPTotalAmount { get; set; }
        public string Remark { get; set; }
        public long OP_IP_Type { get; set; }
        public long AdmId { get; set; }
        public long CreatedBy { get; set; }

       


    }



    public class InsertMaterialConsDetail
    {
        //public long MaterialConDetId { get; set; }
        public long MaterialConsumptionId { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }

        public DateTime BatchExpDate { get; set; }
        public long Qty { get; set; }
        public float PerUnitLandedRate { get; set; }
        public float ParUnitPurchaseRate { get; set; }
        public float PerUnitMRPRate { get; set; }
        public float LandedTotalAmount { get; set; }
        public float PurchaseTotalAmount { get; set; }
        public float MRPTotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remark { get; set; }
        public long AdmId { get; set; }



    }


    public class UpdateCurrentStock
    {
        public int ItemId { get; set; }
        public long IssueQty { get; set; }
        public int StoreID { get; set; }
        public int StkId { get; set; }
    }
}
