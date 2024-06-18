using System;
using System.Collections.Generic;
using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System.Data;
using System.Data.SqlClient;

namespace HIMS.Data.IPD
{
    public class R_CanteenRequest : GenericRepository, I_CanteenRequest
    {
        public R_CanteenRequest(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String Insert(CanteenRequestParams canteenRequestParams)
        {
            /// throw new NotImplementedException();
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ReqId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = canteenRequestParams.CanteenRequestHeaderInsert.ToDictionary();
            disc1.Remove("ReqId");
            var RequestId = ExecNonQueryProcWithOutSaveChanges("m_insert_T_CanteenRequestHeader_1", disc1, outputId1);

            foreach (var a in canteenRequestParams.CanteenRequestDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["ReqId"] = RequestId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_CanteenRequestDetails_1", disc);
            }

            _unitofWork.SaveChanges();
            return RequestId;

        }
    }
}
