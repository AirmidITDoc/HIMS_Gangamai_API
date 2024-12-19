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
        public string VendorMobileNo { get; set; }
        public string VendorPinCode { get; set; }
        public string VendorPersonName { get; set; }
        public string VendorPersonMobileNo { get; set; }
        public long CreatedBy { get; set; }
    }
    public class UpdateVendorInformationParam
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorMobileNo { get; set; }
        public string VendorPinCode { get; set; }
        public string VendorPersonName { get; set; }
        public string VendorPersonMobileNo { get; set; }
        public long ModifiedBy { get; set; }
    }
}

    