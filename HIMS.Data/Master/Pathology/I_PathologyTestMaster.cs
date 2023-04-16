using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_PathologyTestMaster
    {
        public bool Insert(PathologyTestMasterParams pathTestMasterParams);
        public bool Update(PathologyTestMasterParams pathTestMasterParams);
    }
}
