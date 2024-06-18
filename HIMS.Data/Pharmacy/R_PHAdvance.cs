using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_PHAdvance:GenericRepository,I_PHAdvance
    {
        public R_PHAdvance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

   
        public bool Insert(PHAdvanceparam PHAdvanceparam)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceDetailID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = PHAdvanceparam.InsertPHAdvance.ToDictionary();
            disc1.Remove("AdvanceID");
            var AdvID = ExecNonQueryProcWithOutSaveChanges("insert_T_PHAdvanceHeader_1", disc1, outputId1);

            foreach (var a in PHAdvanceparam.InsertPHAdvanceDetail)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("AdvanceDetailID");
                disc5["AdvanceId"] = AdvID;
                var AdvdetailID = ExecNonQueryProcWithOutSaveChanges("insert_TPHAdvanceDetail_1", disc5, outputId2);
            }

            var disc2 = PHAdvanceparam.InsertPHPayment.ToDictionary();
            var ID = ExecNonQueryProcWithOutSaveChanges("insert_I_PHPayment_1", disc2);


            _unitofWork.SaveChanges();
            return true;
        }

    

        public bool Update(PHAdvanceparam PHAdvanceparam)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceDetailID",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = PHAdvanceparam.UpdatePHAdvance.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_T_PHAdvanceHeader", disc3);


            foreach (var a in PHAdvanceparam.InsertPHAdvanceDetail)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("AdvanceDetailID");
                disc5["AdvanceId"] = PHAdvanceparam.UpdatePHAdvance.AdvanceId;
                var AdvdetailID = ExecNonQueryProcWithOutSaveChanges("insert_TPHAdvanceDetail_1", disc5, outputId1);
            }

            var disc2 = PHAdvanceparam.InsertPHPayment.ToDictionary();
            var ID = ExecNonQueryProcWithOutSaveChanges("insert_I_PHPayment_1", disc2);


            _unitofWork.SaveChanges();
            return true;
        }
    }
}
