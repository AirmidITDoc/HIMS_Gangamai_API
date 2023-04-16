using HIMS.Model.IPD;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Radiology
{
    public interface I_RadiologyTemplateResult
    {
        public bool Update(RadiologyTemplateResultParams RadiologyTemplateResultParams);
    }
}
