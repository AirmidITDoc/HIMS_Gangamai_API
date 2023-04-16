using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public interface I_WardMaster
    {
        bool Update(WardMasterParams WardMasterParams);
        bool Save(WardMasterParams WardMasterParams);
    }
}
