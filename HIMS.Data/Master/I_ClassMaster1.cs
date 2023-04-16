using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
  public  interface I_ClassMaster1
    {
        bool Update(ClassMasterParams ClassMasterParams);
        bool Save(ClassMasterParams ClassMasterParams);
    }
}
