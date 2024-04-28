using HIMS.Model.Master;
using HIMS.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
    public interface I_MenuMaster
    {
        bool Save(MenuMasterParams menuMasterParams);

        bool Update(MenuMasterParams menuMasterParams);
        List<MenuModel> GetMenus(int RoleId, bool isActiveMenuOnly);
        List<MenuModel> GetPermisison(int RoleId);
        void SavePermission(List<PermissionModel> lst);
    }
}
