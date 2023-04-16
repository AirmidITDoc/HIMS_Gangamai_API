using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_NeroSurgeryOTNotes
    {
        public String Insert(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam);
        public bool Update(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam);
    }
}
