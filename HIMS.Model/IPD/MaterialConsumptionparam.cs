using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class MaterialConsumptionparam
    {
        public MaterialconsumptionInsert MaterialconsumptionInsert { get; set; }
    }

    public class MaterialconsumptionInsert
    {
        public int MaterialConsumptionId { get; set; }
        public DateTime ConsumptionDate { get; set; }
        public DateTime ConsumptionTime { get; set; }
        public int FromStoreId { get; set; }
        public int LandedTotalAmount { get; set; }
        public int PurchaseTotal { get; set; }
        public int MRPTotal { get; set; }
        public String Remark { get; set; }
        public int Addedby { get; set; }

    }
}
