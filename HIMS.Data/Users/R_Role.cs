using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Users;
using HIMS.Model.Opd.OP;

namespace HIMS.Data.Users
{
    public class R_Role : GenericRepository, I_Role
    {
        public R_Role(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(RoleModel indentParams)
        {
            var dic = indentParams.ToDictionary();
            if (indentParams.RoleId > 0)
            {
                ExecScalar("UPDATE RoleTemplateMaster SET ROleName=@RoleName WHERE RoleId=@RoleId", dic);
            }
            else
            {
                ExecScalar("INSERT INTO RoleTemplateMaster(ROleName,IsActive) VALUES(@RoleName,1)", dic);
            }
            return "Ok";
        }
        public List<RoleModel> GetRoles(string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName))
                RoleName = "";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleName",RoleName);
            return GetList<RoleModel>("SELECT RoleId,RoleName FROM RoleTemplateMaster WHERE IsActive=1 AND RoleName LIKE '%'+@RoleName+'%'", para);
        }

    }
}
