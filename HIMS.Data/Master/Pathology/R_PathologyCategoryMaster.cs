using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
            ExecNonQueryProcWithOutSaveChanges("m_update_PathCategory_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(PathologyCategoryMasterParams pathCategoryMasterParams)

        {

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@CategoryId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc = pathCategoryMasterParams.InsertPathologyCategoryMaster.ToDictionary();
           // disc.Remove("CategoryId");
            ExecNonQueryProcWithOutSaveChanges("m_insert_PathCategoryMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
