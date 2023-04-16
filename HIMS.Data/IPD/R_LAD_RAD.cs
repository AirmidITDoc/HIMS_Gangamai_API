using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_LAD_RAD:GenericRepository, I_LAD_RAD
    {
        public R_LAD_RAD(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(LAD_RADParams LAD_RADParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RequestId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = LAD_RADParams.InsertH_LabRequest.ToDictionary();
            disc1.Remove("RequestId");
            var RequestId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_T_HLabRequest", disc1, outputId1);

            var disc2 = LAD_RADParams.InsertD_LabRequest.ToDictionary();
            disc2["RequestId"] = RequestId;
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_T_DLabRequest", disc2);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}