using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public interface I_LocationMaster
    {
        bool Update(LocationMasterParams LocationMasterParams);
        bool Save(LocationMasterParams LocationMasterParams);
    }
}
