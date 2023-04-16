using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
    public interface I_OpdCasePaper
    {
        public bool Insert(OpdCasePaperParams opdCasePaperParams);
        public bool Update(OpdCasePaperParams opdCasePaperParams);
  
    }
}
  