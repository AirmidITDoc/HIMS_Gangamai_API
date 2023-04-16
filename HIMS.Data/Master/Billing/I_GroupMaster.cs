using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Master.DepartmenMaster;
using HIMS.Model.Master.Billing;

namespace HIMS.Data.Master.Billing
{
   public interface I_GroupMaster
    {
        bool Update(GroupMasterParams GroupMasterParams);
        bool Save(GroupMasterParams GroupMasterParams);
    }
}
