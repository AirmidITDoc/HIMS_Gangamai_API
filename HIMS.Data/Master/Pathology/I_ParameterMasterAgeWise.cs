using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_ParameterMasterAgeWise
    {
       public bool Insert(ParameterMasterAgeWiseParams paraMasterAgeParams);
       public bool Update(ParameterMasterAgeWiseParams paraMasterAgeParams);
    }
}
