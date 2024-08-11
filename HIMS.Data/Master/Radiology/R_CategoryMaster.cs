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
            ExecNonQueryProcWithOutSaveChanges("update_Radiology_Category_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(CategoryMasterParams categoryMasterParams)
        {
            var disc = categoryMasterParams.InsertCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_Radiology_CategoryMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
