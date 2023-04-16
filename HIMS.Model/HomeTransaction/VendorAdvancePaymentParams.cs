using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
    public class VendorAdvancePaymentParams
    {
        public VendorAdvancePaymentInsert vendorAdvancePaymentInsert { get; set; }
        public VendorAdvancePaymentUpdate vendorAdvancePaymentUpdate { get; set; }
        //public getVendor getVendor { get; set; }
    }
    public class VendorAdvancePaymentInsert
    {
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }

        public int VendorID { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }

        public string TransctionType { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public long AddedBy { get; set; }
        public long ProjectId { get; set; }
        public long TranTypeId { get; set; }
    }

    public class VendorAdvancePaymentUpdate
    {
        public int TranID { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }

        public int VendorID { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }

        public string TransctionType { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public long UpdatedBy { get; set; }
        public long ProjectId { get; set; }
        public long TranTypeId { get; set; }

    }

    public class getVendor{

        public string HospitalName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }

}
