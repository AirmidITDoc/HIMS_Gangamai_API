using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Data.IPD;
using HIMS.Model.Inventory;
using HIMS.Common.Utility;

namespace HIMS.Data.Inventory
{
    public  class R_InvOpeningBalance:GenericRepository,I_InvOpeningBalance
    {
        public R_InvOpeningBalance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string OpeningBalanceParamInsert(OpeningBalanceParam OpeningBalanceParam)
        {
            var vIssueTrackerId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OpeningId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = OpeningBalanceParam.OpeningBalanceParamInsert.ToDictionary();
            disc3.Remove("OpeningId");
            var OpeningId = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_1", disc3, vIssueTrackerId);


            _unitofWork.SaveChanges();
            return OpeningId;
        }
    }
}
