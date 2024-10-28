using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public interface I_NewMenuMaster
    {
        bool Save(NewMenumasterParam NewMenumasterParam);

        bool Update(NewMenumasterParam NewMenumasterParam);
        //List<MenuModel> GetMenus(int RoleId, bool isActiveMenuOnly);
        //List<MenuModel> GetPermisison(int RoleId);
       // void SavePermission(List<PermissionModel> lst);
    }
}
