using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public interface I_RadiologyTemplateMaster
    {
        public bool Insert(RadiologyTemplateMasterParams radiologyTemplateMasterParams);
        public bool Update(RadiologyTemplateMasterParams radiologyTemplateMasterParams);
    }
}
