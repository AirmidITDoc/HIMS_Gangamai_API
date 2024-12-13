using HIMS.Model.CustomerInformation;
using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.CustomerInformation
{
    public  interface I_CustomerInformation
    {
        public string CustomerInformationInsert(CustomerInformationParams customerInformationParams);
        public bool CustomerInformationUpdate(CustomerInformationParams customerInformationParams);
        public bool SaveVendorInformation(VendorInformationParam VendorInformationParam);
        public bool UpdateVendorInformation(VendorInformationParam VendorInformationParam);
    }
}
