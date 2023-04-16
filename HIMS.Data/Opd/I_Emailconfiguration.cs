using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_Emailconfiguration
    {
        public String Insert(Emailconfigurationparams Emailconfigurationparams);
        public bool Update(Emailconfigurationparams Emailconfigurationparams);

    }
}
