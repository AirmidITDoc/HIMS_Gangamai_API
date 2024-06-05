using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Inventory;

namespace HIMS.Data.Inventory
{
    public interface I_StockAdjustment
    {
        public string StockAdjustment(StockAdjustmentParams stockAdjustmentParams);
        public bool BatchAdjustment(StockAdjustmentParams stockAdjustmentParams);
        public bool GSTAdjustment(StockAdjustmentParams stockAdjustmentParams);
    }
}
