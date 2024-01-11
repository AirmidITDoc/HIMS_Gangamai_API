using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Master;
using HIMS.Model.Users;

namespace HIMS.Data.Users
{
    public interface I_Role
    {
        public string Insert(RoleModel indentparams);
        List<RoleModel> GetRoles(string RoleName);
        List<MenuMaster> GetPermisison();
    }
}
