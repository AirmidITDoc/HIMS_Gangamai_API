using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_Mrpadjustment
    {
        public bool Insert(MRPAdjustment MRPAdjustment);
        public bool update(MRPAdjustment MRPAdjustment);
    }
}
