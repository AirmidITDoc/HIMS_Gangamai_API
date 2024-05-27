using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HIMS.Common.Utility;

namespace HIMS.Data.Inventory
{
    public  class R_IssueToDepartmentIndent : GenericRepository, I_IssueToDepartmentIndent
    {
        public R_IssueToDepartmentIndent(IUnitofWork unitofWork) : base(unitofWork)
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
            var IssueId = ExecNonQueryProcWithOutSaveChanges("m_Insert_IssueToDepartmentHeader_1_New", disc3, vIssueId);

            foreach (var a in issuetoDepartmentParams.InsertIssuetoDepartmentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IssueId"] = IssueId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_IssueToDepartmentDetails_1", disc5);

            }
            foreach (var a in issuetoDepartmentParams.updateissuetoDepartmentStock)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            }

            _unitofWork.SaveChanges();
            return IssueId;
        }

        public string UpdateIndentStatusAganistIss(IssueToDepartmentIndentParam IssueToDepartmentIndentParam)
        {

            var vIssueId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = IssueToDepartmentIndentParam.insertIssuetoDepartmentHeader1.ToDictionary();
            disc3.Remove("IssueId");
            var IssueId = ExecNonQueryProcWithOutSaveChanges("m_Insert_IssueToDepartmentHeader_1_New", disc3, vIssueId);

            foreach (var a in IssueToDepartmentIndentParam.InsertIssuetoDepartmentDetail1)
            {
                var disc5 = a.ToDictionary();
                disc5["IssueId"] = IssueId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_IssueToDepartmentDetails_1", disc5);

            }
            foreach (var a in IssueToDepartmentIndentParam.UpdateissuetoDepartmentStock1)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            }

            var vIndentHeaderStatus = IssueToDepartmentIndentParam.Update_IndentHeader_Status.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_IndentHeader_Status_AganistIssue", vIndentHeaderStatus);
            
            foreach (var a in IssueToDepartmentIndentParam.updateIndentStatusIndentDetails)
            {
                var vIndentDet = a.ToDictionary();
                //vIndentDet["IndentId"] = disc3["IndentId"];
                ExecNonQueryProcWithOutSaveChanges("Update_Indent_Status_AganistIss", vIndentDet);

            }
            
            _unitofWork.SaveChanges();
            return IssueId;
        }

       

        //bool I_IssueToDepartmentIndent.UpdateIndentStatusAganistIss(IssueToDepartmentIndentParam IssueToDepartmentIndentParam)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
