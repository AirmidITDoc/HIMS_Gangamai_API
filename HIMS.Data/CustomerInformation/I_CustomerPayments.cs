using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.CustomerAMCInfo
{
    public interface I_CustomerAMCInfo
    {
        public string CustomerAMCInsert(CustomerAmcParams customerAmcParams);
        public bool CustomerAMCUpdate(CustomerAmcParams customerAmcParams);
        public bool CustomerAMCCancel(CustomerAmcParams customerAmcParams);
    }
}
