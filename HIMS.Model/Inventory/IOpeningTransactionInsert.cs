using System;

namespace HIMS.Model.Inventory
{
    public interface IOpeningTransactionInsert
    {
        long Addedby { get; set; }
        string BalQty { get; set; }
        DateTime BatchExpDate { get; set; }
        string BatchNo { get; set; }
        long ItemId { get; set; }
        long OpeningDocNo { get; set; }
        long OpeningId { get; set; }
        DateTime OpeningTime { get; set; }
        long PerUnitMrp { get; set; }
        long PerUnitPurRate { get; set; }
        long StoreId { get; set; }
        long VatPer { get; set; }
    }
}