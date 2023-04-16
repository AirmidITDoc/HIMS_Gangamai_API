using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_OTNotesTemplate
    {
        public String Insert(OTNotesTemplateparam OTNotesTemplateparam);
        public bool Update(OTNotesTemplateparam OTNotesTemplateparam);
    }
}
