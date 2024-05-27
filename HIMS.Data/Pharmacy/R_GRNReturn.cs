using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_GRNReturn :GenericRepository , I_GRNReturn
    {
           public R_GRNReturn(IUnitofWork unitofWork) : base(unitofWork)
    {

    }

        public string InsertGRNReturn(GRNReturnParam GRNReturnParam)
        {
            //  throw new NotImplementedException();


            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNReturnId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc3 = GRNReturnParam.GRNReturnSave.ToDictionary();
            disc3.Remove("GRNReturnId");
            var GrnReturnId = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNReturnH_GrnReturnNo_1", disc3, outputId2);

            foreach (var a in GRNReturnParam.GRNReturnDetailSave)
            {
                var disc5 = a.ToDictionary();
                //   disc5.Remove("GRNDetID");
                disc5["GrnReturnId"] = GrnReturnId;
                var GrnDetID = ExecNonQueryProcWithOutSaveChanges("m_insert_GRNReturnDetails_1", disc5);
            }
            foreach (var a in GRNReturnParam.GRNReturnUpdateCurrentStock)
            {
                var disc6 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_T_CurrentStock_GRNReturn_1", disc6);
            }


            foreach (var a in GRNReturnParam.GRNReturnUpateReturnQty)
            {
                var disc7 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_GrnReturnQty_GrnTbl_1", disc7);
            }
           
            _unitofWork.SaveChanges();
            return GrnReturnId;
        }

        public bool VerifyGRNReturn(GRNReturnParam GRNReturnParam)
        {
            var vGRNVerify = GRNReturnParam.UpdateGRNReturnVerifyStatus.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_GRNReturn_Verify_Status_1", vGRNVerify);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
