using HIMS.Common.Utility;
using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Inventory
{
   public class R_ReturnFromDept :GenericRepository,I_ReturnFromDept
    {
        public R_ReturnFromDept(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string InsertReturnFromDepartment(ReturnfrdeptParam ReturnfrdeptParam)
        {
            //throw new NotImplementedException();

            var vIssueId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ReturnId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = ReturnfrdeptParam.InsertReturnDepartmentHeader.ToDictionary();
            disc3.Remove("ReturnId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_ReturnFromDepartment_1", disc3, vIssueId);

            foreach (var a in ReturnfrdeptParam.InsertReturnDepartmentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["ReturnId"] = Id;
                ExecNonQueryProcWithOutSaveChanges("insert_ReturnFromDepartmentDetail_1", disc5);

            }
            //foreach (var a in ReturnfrdeptParam.updateissuetoDepartmentStock)
            //{
            //    var disc5 = a.ToDictionary();
            //    ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            //}

            _unitofWork.SaveChanges();
            return Id;
        }
    }
}
