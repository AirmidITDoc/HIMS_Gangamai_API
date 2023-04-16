using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPAdvanceUpdate: GenericRepository, I_IPAdvanceUpdate
    {
        public R_IPAdvanceUpdate(IUnitofWork unitofWork) : base(unitofWork)
        {
        
        }

        public String Insert(IPAdvanceUpdateParams IPAdvanceUpdateParams)
        {
            
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceDetailID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

          

           

            var disc2 = IPAdvanceUpdateParams.AdvanceDetailInsert1.ToDictionary();
            disc2.Remove("AdvanceDetailID");
            var AdvanceDetailID = ExecNonQueryProcWithOutSaveChanges("insert_AdvanceDetail_1", disc2, outputId1);

            var disc1 = IPAdvanceUpdateParams.AdvanceHeaderUpdate.ToDictionary();
            int AdvanceId = (int)Convert.ToInt64(IPAdvanceUpdateParams.AdvanceHeaderUpdate.AdvanceId);
            ExecNonQueryProcWithOutSaveChanges("Update_AdvHeader_1", disc1);


            var disc3 = IPAdvanceUpdateParams.IPPaymentInsert1.ToDictionary();
         //   disc3.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc3);

            _unitofWork.SaveChanges();
            return AdvanceDetailID;
        }
    }
}
