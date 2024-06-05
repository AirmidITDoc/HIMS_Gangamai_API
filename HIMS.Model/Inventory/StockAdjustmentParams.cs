using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class StockAdjustmentParams
    {
        public StockAdjustment StockAdjustment { get; set; }
        public BatchAdjustment BatchAdjustment { get; set; }
        public GSTAdjustment GSTAdjustment { get; set; }
    }

    public class StockAdjustment
    {
        public long StoreID { get; set; }
        public long StkId { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public long Ad_DD_Type { get; set; }
        public long Ad_DD_Qty { get; set; }
        public long PreBalQty { get; set; }
        public long AfterBalQty { get; set; }
        public long AddedBy { get; set; }
        public long StockAdgId { get; set; }
    }
    public class BatchAdjustment
    {
        public long StoreId { get; set; }
        public long ItemId { get; set; }
        public string OldBatchNo { get; set; }
        public DateTime OldExpDate { get; set; }
        public string NewBatchNo { get; set; }
        public DateTime NewExpDate { get; set; }
        public long AddedBy { get; set; }
        public long StkId { get; set; }
    }

    public class GSTAdjustment
    {
        public long StoreId { get; set; }
        public long ItemId { get; set; }
        public long StkId { get; set; }
        public string BatchNo { get; set; }
        public float OldCGSTPer { get; set; }
        public float OldSGSTPer { get; set; }
        public float OldIGSTPer { get; set; }
        public float CGSTPer { get; set; }
        public float SGSTPer { get; set; }
        public float IGSTPer { get; set; }
        public long AddedBy { get; set; }
       
    }
}
