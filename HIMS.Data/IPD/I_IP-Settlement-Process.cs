using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IP_Settlement_Process
    {
        public String Insert(IP_Settlement_Processparams IP_Settlement_Processparams);
    }
}
