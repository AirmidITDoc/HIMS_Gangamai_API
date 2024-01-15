using HIMS.Common.Utility;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master
{
    public class R_MenuMaster : GenericRepository, I_MenuMaster
    {
        public R_MenuMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public List<MenuMaster> GetMenus(int RoleId)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@RoleId", RoleId);
            return GetList<MenuMaster>("SELECT M.*,P.IsView,P.IsAdd,P.IsEdit,P.IsDelete FROM MenuMaster M LEFT JOIN PermissionMaster P ON M.Id=P.MenuId AND P.RoleId=@RoleId\r\nWHERE IsActive=1 AND IsDisplay=1", para);
        }

        public bool Update(MenuMasterParams menuMasterParams)
        {
            //Update VendorMaster
            var disc = menuMasterParams.MenuMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_Menu_Master_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(MenuMasterParams menuMasterParams)
        {

            var disc = menuMasterParams.MenuMasterInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_Menu_Master_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
