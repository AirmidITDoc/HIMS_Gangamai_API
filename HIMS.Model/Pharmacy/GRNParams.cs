using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class GRNParams
    {
        public GRNSave GRNSave { get; set; }
        public List<GRNDetailSave> GRNDetailSave { get; set; }
        public List<UpdateItemMasterGSTPer> UpdateItemMasterGSTPer { get; set; }
        public List<Update_PO_STATUS_AganistGRN> Update_PO_STATUS_AganistGRN { get; set; }
        public List<Update_POHeader_Status_AganistGRN> Update_POHeader_Status_AganistGRN { get; set; }
        public UpdateGRNVerifyStatus UpdateGRNVerifyStatus { get; set; }
        public UpdateGRNHeader updateGRNHeader { get; set; }
        public Delete_GRNDetails Delete_GRNDetails { get; set; }
    }
    public class GRNSave
    {
        public DateTime GRNDate { get; set; }
        public DateTime GRNTime { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public string InvoiceNo { get; set; }
        public string DeliveryNo { get; set; }
        public string GateEntryNo { get; set; }
        public bool Cash_CreditType { get; set; }
        public bool GRNType { get; set; }
        public float TotalAmount { get; set; }
        public float TotalDiscAmount { get; set; }
        public float TotalVATAmount { get; set; }
        public float NetAmount { get; set; }
        public string Remark { get; set; }
        public string ReceivedBy { get; set; }
        public bool IsVerified { get; set; }
        public bool IsClosed { get; set; }
        public long AddedBy { get; set; }
        public DateTime InvDate { get; set; }
        public float DebitNote { get; set; }
        public float CreditNote { get; set; }
        public float OtherCharge { get; set; }
        public float RoundingAmt { get; set; }
        public float TotCGSTAmt { get; set; }
        public float TotSGSTAmt { get; set; }
        public float TotIGSTAmt { get; set; }
        public long TranProcessId { get; set; }
        public string TranProcessMode { get; set; }
        public string EwayBillNo { get; set; }
        public DateTime EwayBillDate { get; set; }
        public float BillDiscAmt  { get; set; }
        public long GRNID { get; set; }
    }

    public class GRNDetailSave
    {

        public long GRNDetID { get; set; }
        public long GRNId { get; set; }
        public long ItemId { get; set; }
        public long UOMId { get; set; }
        public float ReceiveQty { get; set; }
        public float FreeQty { get; set; }
        public float MRP { get; set; }
        public float Rate { get; set; }
        public float TotalAmount { get; set; }
        public long ConversionFactor { get; set; }
        public float VatPercentage { get; set; }
        public float VatAmount { get; set; }
        public float DiscPercentage { get; set; }
        public float DiscAmount { get; set; }
        public float OtherTax { get; set; }
        public float LandedRate { get; set; }
        public float NetAmount { get; set; }
        public float GrossAmount { get; set; }
        public float TotalQty { get; set; }
        public long PONo { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public float PurUnitRate { get; set; }
        public float PurUnitRateWF { get; set; }
        public float CGSTPer { get; set; }
        public float CGSTAmt { get; set; }
        public float SGSTPer { get; set; }
        public float SGSTAmt { get; set; }
        public float IGSTPer { get; set; }
        public float IGSTAmt { get; set; }
        public float MRP_Strip { get; set; }
        public bool IsVerified { get; set; }
        public DateTime IsVerifiedDatetime { get; set; }
        public int IsVerifiedUserId { get; set; }
        public int stkId { get; set; }
        public float DiscPerc2 { get; set; }

        public float DiscAmt2 { get; set; }
    }
   
    public class UpdateItemMasterGSTPer
    {
        public long ItemId { get; set; }
        public float CGST { get; set; }
        public float SGST { get; set; }
        public float IGST { get; set; }
        public string HSNcode { get; set; }
       // public float ConversionFactor { get; set; }
    }

    public class Update_PO_STATUS_AganistGRN
    {
        public long POId { get; set; }

        public long PurDetID { get; set; }

        public long POBalQty { get; set; }
        public bool IsClosed { get; set; }

    }
    public class Update_POHeader_Status_AganistGRN
    {
        public long POId { get; set; }
       // public long PurDetID { get; set; }
        public bool IsClosed { get; set; }
       // public float POBalQty { get; set; }

    }

    public class UpdateGRNVerifyStatus {
        public long GRNID { get; set; }
        public bool ISVerified { get; set; }
    }
    public class UpdateGRNHeader
    {
        public long GRNID { get; set; }
        public DateTime GRNDate { get; set; }
        public DateTime GRNTime { get; set; }
        public long StoreId { get; set; }
        public long SupplierID { get; set; }
        public string InvoiceNo { get; set; }
        public string DeliveryNo { get; set; }
        public string GateEntryNo { get; set; }
        public bool Cash_CreditType { get; set; }
        public bool GRNType { get; set; }
        public float TotalAmount { get; set; }
        public float TotalDiscAmount { get; set; }
        public float TotalVATAmount { get; set; }
        public float NetAmount { get; set; }
        public string Remark { get; set; }
        public string ReceivedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime InvDate { get; set; }
        public float DebitNote { get; set; }
        public float CreditNote { get; set; }
        public float OtherCharge { get; set; }
        public float RoundingAmt { get; set; }
        public float TotCGSTAmt { get; set; }
        public float TotSGSTAmt { get; set; }
        public float TotIGSTAmt { get; set; }
        public long TranProcessId { get; set; }
        public string TranProcessMode { get; set; }

        public float BillDiscAmt { get; set; }
    }
    public class Delete_GRNDetails
    {
        public long GRNId { get; set; }
    }

}
