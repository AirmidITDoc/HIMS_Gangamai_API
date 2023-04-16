using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPDEmergency : GenericRepository, I_IPDEmergency
    {
        public R_IPDEmergency(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(IPDEmergencyParams IPDEmergencyParams)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RegId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = IPDEmergencyParams.IPDEmergencyRegInsert.ToDictionary();
            disc2.Remove("RegId");

            var OutRegId = ExecNonQueryProcWithOutSaveChanges("insert_Registration_1_1", disc2, outputId1);

           /* var disc1 = IPDEmergencyParams.IPDEmergencyRegInsert.ToDictionary();
            disc1.Remove("RegId");
            var OutRegId = ExecNonQueryProcWithOutSaveChanges("ps_Insert_T_Registration", disc1, outputId);
           */


            _unitofWork.SaveChanges();
            return true;
        }
    }
}