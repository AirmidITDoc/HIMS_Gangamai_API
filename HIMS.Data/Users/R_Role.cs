using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Users;
using HIMS.Model.Opd.OP;
using HIMS.Model.Master;

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
                ExecScalar("UPDATE RoleMaster SET ROleName=@RoleName WHERE RoleId=@RoleId", dic);
            }
            else
            {
                ExecScalar("INSERT INTO RoleMaster(ROleName,IsActive) VALUES(@RoleName,1)", dic);
            }
            return "Ok";
        }
        public List<RoleModel> GetRoles(string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName))
                RoleName = "";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleName", RoleName);
            return GetList<RoleModel>("SELECT RoleId,RoleName FROM RoleMaster WHERE IsActive=1 AND RoleName LIKE '%'+@RoleName+'%'", para);
        }
        public List<MenuMaster> GetPermisison()
        {
            // return GetListBySp<MenuMaster>("SELECT M.* FROM MenuMaster M LEFT JOIN MenuMaster MM on M.Id=MM.UpId WHERE ISNULL(MM.Id,0)=0", new SqlParameter[0]);

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@RoleId", 1);
            return GetListBySp<MenuMaster>("GET_PERMISSION", sqlParameters);
        }
        public void SavePermission(List<PermissionModel> lst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RoleID", typeof(int));
            dt.Columns.Add("MenuId", typeof(int));
            dt.Columns.Add("IsView", typeof(int));
            dt.Columns.Add("IsAdd", typeof(int));
            dt.Columns.Add("IsEdit", typeof(int));
            dt.Columns.Add("IsDelete", typeof(int));
            foreach (var item in lst)
                dt.Rows.Add(item.RoleId, item.MenuId, item.IsView, item.IsAdd, item.IsEdit, item.IsDelete);
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@tbl", SqlDbType.Structured)
            {
                Value = dt,
                TypeName = "dbo.Permission"
            };
            object dd = ExecuteObjectBySP("Insert_Permission", para);
        }

    }
}
