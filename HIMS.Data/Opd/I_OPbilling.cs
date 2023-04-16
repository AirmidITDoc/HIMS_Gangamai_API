using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_OPbilling
    {
        public string Insert(OPbillingparams OPbillingparams);
       // public bool Insert(OPbillingparams OPbillingparams);
    }
}
