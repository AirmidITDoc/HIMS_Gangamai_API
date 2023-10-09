using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Inventory;

namespace HIMS.Data.Inventory
{
    public class R_Indent : GenericRepository, I_Indent
    {
        public R_Indent(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String Insert(IndentParams indentParams)
        {

            var vIndentId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IndentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = indentParams.InsertIndent.ToDictionary();
            disc3.Remove("IndentId");
            var IndentId = ExecNonQueryProcWithOutSaveChanges("insert_IndentHeader_1", disc3, vIndentId);

            foreach (var a in indentParams.InsertIndentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IndentId"] = IndentId;
                ExecNonQueryProcWithOutSaveChanges("insert_IndentDetails", disc5);
             
            }

            _unitofWork.SaveChanges();
            return IndentId;
        }

        public bool Update(IndentParams indentParams)
        {

            var vupdateIndent = indentParams.UpdateIndent.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_IndentHeader", vupdateIndent);

            var vdeleteParams = indentParams.DeleteIndent.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_IndentDetails", vdeleteParams);

            foreach (var a in indentParams.InsertIndentDetail)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_IndentDetails", disc5);

            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
