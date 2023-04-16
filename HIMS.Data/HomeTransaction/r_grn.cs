using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Transaction
{
    public class R_grn : GenericRepository, I_grn
    {
        public R_grn(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public bool Save(GrnParams grnParams)
        {
            //Grn Header Insert
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@GRNID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = grnParams.grnHeaderInsert.ToDictionary();
            dic.Remove("GRNId");
            var GRNID = ExecNonQueryProcWithOutSaveChanges("insert_T_GRNHeader_1", dic, outputId);


            //GRN Detail Insert
            foreach (var a in grnParams.GrnDetailInsert)
            {
                var disc = a.ToDictionary();
                disc["GRNId"] = GRNID;
                ExecNonQueryProcWithOutSaveChanges("insert_T_GRNDetail_1", disc);
            }

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
