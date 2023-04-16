using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
  public interface I_ReligionMaster
    {
        bool Update(ReligionMasterParams ReligionMasterParams);
        bool Save(ReligionMasterParams ReligionMasterParams);
    }
}
