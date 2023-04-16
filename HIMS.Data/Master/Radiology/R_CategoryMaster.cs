using HIMS.Common.Utility;
using HIMS.Model.Master.Radiology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Radiology
{
    public class R_CategoryMaster:GenericRepository,I_CategoryMaster
    {
        public R_CategoryMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(CategoryMasterParams categoryMasterParams)
        {
            var disc = categoryMasterParams.UpdateCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_Radiology_CategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(CategoryMasterParams categoryMasterParams)
        {
            var disc = categoryMasterParams.InsertCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_Radiology_CategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
