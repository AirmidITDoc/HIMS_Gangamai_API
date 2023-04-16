using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_PathologyCategoryMaster:GenericRepository,I_PathologyCategoryMaster
    {
        public R_PathologyCategoryMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(PathologyCategoryMasterParams pathCategoryMasterParams)
        {
            var disc = pathCategoryMasterParams.UpdatePathologyCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PathCategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(PathologyCategoryMasterParams pathCategoryMasterParams)
        {
            var disc = pathCategoryMasterParams.InsertPathologyCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathCategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
