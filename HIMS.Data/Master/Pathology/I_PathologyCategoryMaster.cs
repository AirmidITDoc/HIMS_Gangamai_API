using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public interface I_PathologyCategoryMaster
    {
        public bool Insert(PathologyCategoryMasterParams pathCategoryMasterParams);
        public bool Update(PathologyCategoryMasterParams pathCategoryMasterParams);
    }
}
