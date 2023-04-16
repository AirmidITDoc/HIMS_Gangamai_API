using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_ParameterMaster
    {
        public bool Insert(ParameterMasterParams ParameterMasterParams);
        public bool Update(ParameterMasterParams ParameterMasterParams);
       
    }
}
