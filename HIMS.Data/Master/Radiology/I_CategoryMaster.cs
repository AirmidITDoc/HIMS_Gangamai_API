using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public interface I_CategoryMaster
    {
        public bool Insert(CategoryMasterParams categoryMasterParams);
        public bool Update(CategoryMasterParams categoryMasterParams);
    }
}
