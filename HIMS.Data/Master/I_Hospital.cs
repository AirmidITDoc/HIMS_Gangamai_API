using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Master;
using HIMS.Model.Users;

namespace HIMS.Data.Master
{
    public interface I_Hospital
    {
        HospitalMaster GetHospitalById(long Id);
        HospitalMaster GetHospitalStoreById(long Id);
    }
}
