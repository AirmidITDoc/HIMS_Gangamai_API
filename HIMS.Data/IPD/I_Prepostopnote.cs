using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
  public interface I_Prepostopnote
    {
        public String Insert(Prepostopnoteparam Prepostopnoteparam);
        public bool Update(Prepostopnoteparam Prepostopnoteparam);
    }
}
