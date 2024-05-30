using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Model.Inventory;
using HIMS.Common.Utility;
using HIMS.Model.CustomerInformation;

namespace HIMS.Data.Inventory
{
    public class R_OpeningTransaction : GenericRepository, I_OpeningTransaction
    {

        public R_OpeningTransaction(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(OpeningTransactionParam OpeningTransactionParam)
        {

            var OpeningHId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OpeningHId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var OpeningId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@OpeningId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = OpeningTransactionParam.Insert_OpeningTransaction_Header_1.ToDictionary();
            disc3.Remove("OpeningHId");
            var Id = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_header_1", disc3, OpeningHId);

            foreach (var a in OpeningTransactionParam.OpeningTransactionInsert)
            {
                var disc5 = a.ToDictionary();
                disc5.Remove("OpeningId");
               // disc5["OpeningDocNo"] = Id;
               var OId = ExecNonQueryProcWithOutSaveChanges("Insert_OpeningTransaction_1", disc5, OpeningId);

            }
            var disc4 = OpeningTransactionParam.Insert_Update_OpeningTran_ItemStock_1.ToDictionary();
            disc4["OPeningHId"] = Id;
            ExecNonQueryProcWithOutSaveChanges("Insert_Update_OpeningTran_ItemStock_1", disc4);  
  

            _unitofWork.SaveChanges();
            return Id;
        }
    }
}


