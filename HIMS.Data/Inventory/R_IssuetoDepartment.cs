using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Inventory;
using System.IO;

namespace HIMS.Data.Inventory
{
    public class R_IssuetoDepartment : GenericRepository, I_IssuetoDepartment
    {
        public R_IssuetoDepartment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String InsertIssuetoDepartment(IssuetoDepartmentParams issuetoDepartmentParams)
        {
            var vIssueId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = issuetoDepartmentParams.InsertIssuetoDepartmentHeader.ToDictionary();
            disc3.Remove("IssueId");
            var IssueId = ExecNonQueryProcWithOutSaveChanges("Insert_IssueToDepartmentHeader_1_New", disc3, vIssueId);

            foreach (var a in issuetoDepartmentParams.InsertIssuetoDepartmentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IssueId"] = IssueId;
                ExecNonQueryProcWithOutSaveChanges("insert_IssueToDepartmentDetails_1", disc5);

            }
            foreach (var a in issuetoDepartmentParams.updateissuetoDepartmentStock)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            }

            _unitofWork.SaveChanges();
            return IssueId;
        }

    }
}
