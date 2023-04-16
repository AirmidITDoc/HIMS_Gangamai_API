using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public interface I_RadiologyTestMaster
    {
        public bool Insert(RadiologyTestMasterParams radiologyTemplateMasterParams);
        public bool Update(RadiologyTestMasterParams radiologyTemplateMasterParams);
    }
}
