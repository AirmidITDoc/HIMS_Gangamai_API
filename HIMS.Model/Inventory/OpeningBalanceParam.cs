using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class OpeningBalanceParam
    {
        public OpeningBalanceParamInsert OpeningBalanceParamInsert {  get; set; }   
    }
    public class OpeningBalanceParamInsert
    {
        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime OpeningTime { get; set; }
        public long OpeningDocNo { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public int PerUnitPurRate { get; set; }
        public int PerUnitMrp { get; set; }
        public float VatPer { get; set; }
        public long BalQty { get; set; }
        public long Addedby { get; set; }
        public long OpeningId { get; set; }
    }
}

