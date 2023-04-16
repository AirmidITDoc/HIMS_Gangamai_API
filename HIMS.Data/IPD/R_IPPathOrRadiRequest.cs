using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPPathOrRadiRequest :GenericRepository,I_IPPathOrRadiRequest
    {
        public R_IPPathOrRadiRequest(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String Insert(IPPathOrRadiRequestParams IPPathOrRadiRequestParams)
        {
            /// throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RequestId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPPathOrRadiRequestParams.IPPathOrRadiRequestInsert.ToDictionary();
            disc1.Remove("RequestId");
            var RequestId = ExecNonQueryProcWithOutSaveChanges("insert_T_HLabRequest_1", disc1, outputId1);

            foreach (var a in IPPathOrRadiRequestParams.IPPathOrRadiRequestLabRequestInsert)
            {
                var disc = a.ToDictionary();
                disc["RequestId"] = RequestId;
                ExecNonQueryProcWithOutSaveChanges("insert_T_DLabRequest_1", disc);
            }

          

            _unitofWork.SaveChanges();
            return RequestId;

        }


    }
}
