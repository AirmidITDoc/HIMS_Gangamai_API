using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public interface I_CompanyMaster1
    {
        bool Update(CompanyMasterParams CompanyMasterParams);
        bool Save(CompanyMasterParams CompanyMasterParams);
    }
}
