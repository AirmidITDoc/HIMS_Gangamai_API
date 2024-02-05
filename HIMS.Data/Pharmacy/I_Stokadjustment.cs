using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_Stokadjustment
    {
        public String Insert(Stockadjustmentparam Stockadjustmentparam);

        public bool Update(Stockadjustmentparam Stockadjustmentparam);
    }
}
