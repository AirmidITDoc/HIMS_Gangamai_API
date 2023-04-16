using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public interface I_AreaMaster
    {
        bool Update(AreaMasterParams AreaMasterParams);
        bool Save(AreaMasterParams AreaMasterParams);
    }
}
