using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class VendorInformationParam
    {
        public SaveVendorInformationParam SaveVendorInformationParam { get; set; }
        public UpdateVendorInformationParam UpdateVendorInformationParam { get; set; }
    }

    public class SaveVendorInformationParam
    {
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public long VendorMobileNo { get; set; }
        public long VendorPinCode { get; set; }
        public string VendorPersonName { get; set; }
        public long VendorPersonMobileNo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
    public class UpdateVendorInformationParam
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public long VendorMobileNo { get; set; }
        public long VendorPinCode { get; set; }
        public string VendorPersonName { get; set; }
        public long VendorPersonMobileNo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}

    