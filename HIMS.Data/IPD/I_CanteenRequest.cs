using System;
using HIMS.Model.IPD;

namespace HIMS.Data.IPD
{
    public interface I_CanteenRequest
    {
        public String Insert(CanteenRequestParams canteenRequestParams);
    }
}
