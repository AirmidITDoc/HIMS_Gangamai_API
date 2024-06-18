using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_PHAdvance
    {
        bool Insert(PHAdvanceparam PHAdvanceparam);
        bool Update(PHAdvanceparam PHAdvanceparam);
    }
}
