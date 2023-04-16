using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPDischarge
    {
        public String Insert(IPDischargeParams IPDischargeParams);
        public bool Update(IPDischargeParams IPDischargeParams);
    }
}
