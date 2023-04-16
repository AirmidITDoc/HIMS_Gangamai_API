using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_InsertIPDraft :GenericRepository, I_InsertIPDraft
    {
        public R_InsertIPDraft(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

      
        public String Insert(InsertIPDraftParams InsertIPDraftParams)
        {
            //throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DRBNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc = InsertIPDraftParams.IPIntremdraftbillInsert.ToDictionary();
            disc.Remove("DRBNo");
            var DRNo = ExecNonQueryProcWithOutSaveChanges("insert_DRBill_1", disc, outputId);


            foreach (var a in InsertIPDraftParams.InterimBillDetailsInsert)
            {
                var disc1 = a.ToDictionary();
                disc1["DRNo"] = DRNo;
                ExecNonQueryProcWithOutSaveChanges("insert_T_DRBillDet_1", disc1);
            }


            _unitofWork.SaveChanges();
            return DRNo;
        }

      
    }
}
