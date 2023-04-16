using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_UnitMaster
    {
        public bool Insert(UnitMasterParams unitMasterParams);
        public bool Update(UnitMasterParams unitMasterParams);
    }
}
