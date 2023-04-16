using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
    public class GrnParams
    {
        public GrnHeaderInsert grnHeaderInsert { get; set; }
        public List<GrnDetailInsert> GrnDetailInsert { get; set; }
    }

    public class GrnHeaderInsert
    {
        public long GRNId { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public long VendorID { get; set; }
        public int Cash_Credit_Type { get; set; }
        public DateTime InvoiceDate { get; set; }
        public String InvoiceNo { get; set; }
        public float TotalAmount { get; set; }
        public float GSTAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public float RoundingAmount { get; set; }
        public string Comments { get; set; }
        public string ReceviedBy { get; set; }
        public float PaidAmount { get; set; }
        public float BalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class GrnDetailInsert
    {
        public long GRNId { get; set; }
        public long ItemID { get; set; }
        public float Qty { get; set; }
        public float MRP { get; set; }
        public float Rate { get; set; }
        public float TotalAmount { get; set; }
        public long ConvFactor { get; set; }
        public float GSTAmount { get; set; }
        public float DiscAmount { get; set; }
        public float NetAmount { get; set; }
        public string Comments { get; set; }
        public float CgstPer { get; set; }
        public float CgstAmt { get; set; }
        public float SgstPer { get; set; }
        public float SgstAmt { get; set; }
        public long StkId { get; set; }
        public float ReturnQty { get; set; }

    }
}
