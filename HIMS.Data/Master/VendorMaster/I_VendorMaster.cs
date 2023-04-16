using HIMS.Model.Master.VendorMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.VendorMaster
{
   public interface I_VendorMaster
    {
        bool Save(VendorMasterParams vendorMasterParams);
        bool Update(VendorMasterParams vendorMasterParams);
    }
}
