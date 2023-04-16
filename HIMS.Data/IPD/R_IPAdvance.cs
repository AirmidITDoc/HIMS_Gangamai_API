using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPAdvance: GenericRepository,I_IPAdvance
    {
        public R_IPAdvance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public String Insert(IPAdvanceParams IPAdvanceParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
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

            var disc1 = IPAdvanceParams.AdvanceHeaderInsert.ToDictionary();
            disc1.Remove("AdvanceID");
            var AdvanceID = ExecNonQueryProcWithOutSaveChanges("insert_AdvanceHeader_1", disc1, outputId);

            IPAdvanceParams.AdvanceDetailInsert.AdvanceId = (int)Convert.ToInt64(AdvanceID);
            IPAdvanceParams.AdvanceDetailInsert.RefId = IPAdvanceParams.AdvanceHeaderInsert.RefId;
            IPAdvanceParams.AdvanceDetailInsert.OPD_IPD_Id = IPAdvanceParams.AdvanceHeaderInsert.OPD_IPD_Id;
            IPAdvanceParams.AdvanceDetailInsert.OPD_IPD_Type = IPAdvanceParams.AdvanceHeaderInsert.OPD_IPD_Type;
            IPAdvanceParams.AdvanceDetailInsert.AdvanceAmount = IPAdvanceParams.AdvanceHeaderInsert.AdvanceAmount;
            IPAdvanceParams.AdvanceDetailInsert.UsedAmount = IPAdvanceParams.AdvanceHeaderInsert.AdvanceUsedAmount;
            IPAdvanceParams.AdvanceDetailInsert.BalanceAmount = IPAdvanceParams.AdvanceHeaderInsert.BalanceAmount;


            var disc2 = IPAdvanceParams.AdvanceDetailInsert.ToDictionary();            
            disc2.Remove("AdvanceDetailID");
            var AdvanceDetailID = ExecNonQueryProcWithOutSaveChanges("insert_AdvanceDetail_1", disc2, outputId1);

            IPAdvanceParams.IPPaymentInsert.AdvanceId = (int)Convert.ToInt64(AdvanceDetailID);
            var disc3 = IPAdvanceParams.IPPaymentInsert.ToDictionary();
        
           ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc3);


            _unitofWork.SaveChanges();
            return AdvanceDetailID;
        }

      
    }
}
