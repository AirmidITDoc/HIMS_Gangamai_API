using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
    public interface I_PathParameterMaster
    {
        bool Save(PathParameterMasterParams PathParameterMasterParams);
        bool Update(PathParameterMasterParams PathParameterMasterParams);
    }
}
