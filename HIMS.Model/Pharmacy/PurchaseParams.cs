using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class PurchaseParams
    {
        public PurchaseHeaderInsert PurchaseHeaderInsert { get; set; }
        public List<PurchaseDetailInsert> PurchaseDetailInsert { get; set; }
        public UpdatePurchaseOrderHeader UpdatePurchaseOrderHeader { get; set; }
        public Delete_PurchaseDetails Delete_PurchaseDetails { get; set; }
        public Update_POVerify_Status Update_POVerify_Status { get; set; }
    }

    public class PurchaseHeaderInsert
    {
        public DateTime PurchaseDate { get; set; }
        public DateTime PurchaseTime { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public float TotalAmount { get; set; }
        public float DiscAmount { get; set; }
        public float TaxAmount { get; set; }
        public float FreightAmount { get; set; }
        public float OctriAmount  { get; set; }
        public float GrandTotal { get; set; }
        public bool Isclosed { get; set; }
        public bool IsVerified { get; set; }
        public string Remarks { get; set; }
        public long TaxID { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long PaymentTermId { get; set; }
        public long ModeofPayment { get; set; }
        public string Worrenty { get; set; }
        public float RoundVal { get; set; }
        public float TotCGSTAmt { get; set; }
        public float TotSGSTAmt { get; set; }
        public float TotIGSTAmt { get; set; }
        public float TransportChanges { get; set; }
        public float HandlingCharges { get; set; }
        public float FreightCharges { get; set; }
        public long PurchaseId { get; set; }
    }

    public class PurchaseDetailInsert
    {
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public long UOMId { get; set; }
        public float Qty { get; set; }
        public float Rate { get; set; }
        public float TotalAmount { get; set; }
        public float DiscAmount { get; set; }
        public float DiscPer { get; set; }
        public float VatAmount { get; set; }
        public float VatPer { get; set; }
        public float GrandTotalAmount { get; set; }
        public float MRP { get; set; }
        public string Specification { get; set; }
        public float CGSTPer { get; set; }
        public float CGSTAmt { get; set; }
        public float SGSTPer { get; set; }
        public float SGSTAmt { get; set; }
        public float IGSTPer { get; set; }
        public float IGSTAmt { get; set; }
    }

    public class UpdatePurchaseOrderHeader
    {
        public long PurchaseID { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public float TotalAmount { get; set; }
        public float DiscAmount { get; set; }
        public float TaxAmount { get; set; }
        public float FreightAmount { get; set; }
        public float OctriAmount { get; set; }
        public float GrandTotal { get; set; }
        public bool Isclosed { get; set; }
        public bool IsVerified { get; set; }
        public string Remarks { get; set; }
        public long TaxID { get; set; }
        public long UpdatedBy { get; set; }
        public float TotCGSTAmt { get; set; }
        public float TotSGSTAmt { get; set; }
        public float TotIGSTAmt { get; set; }
    }
    public class Delete_PurchaseDetails
    {
        public long PurchaseId { get; set; }
    }

    public class Update_POVerify_Status
    {
        public long PurchaseID { get; set; }
        public bool ISVerified { get; set; }
        public long IsVerifiedId { get; set; }
    }

}
