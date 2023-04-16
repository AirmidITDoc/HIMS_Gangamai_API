using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public interface I_DischargeTypeMaster
    {
        bool Update(DischargeTypeMasterParams DischargeTypeMasterParams);
        bool Save(DischargeTypeMasterParams DischargeTypeMasterParams);
    }
}
