using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_ParameterMasterAgeWise
    {
       public bool Insert(PathParameterMasterParams pathParameterMasterParams);
        public bool Update(PathParameterMasterParams pathParameterMasterParams);
    }
}
