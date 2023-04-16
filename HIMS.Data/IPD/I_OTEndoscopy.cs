using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_OTEndoscopy
    {
        public String Insert(OTEndoscopyParam OTEndoscopyParam);

        public bool Update(OTEndoscopyParam OTEndoscopyParam);
    }
}
