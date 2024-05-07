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
using Newtonsoft.Json.Linq;

namespace HIMS.Data.Users
{
    public class R_Favourite : GenericRepository, I_Favourite
    {
        public R_Favourite(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(int UserId, int MenuId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                { "UserId", UserId },
                { "MenuId", MenuId }
            };
            ExecScalarProc("IUD_FavouriteMenu", dic);
            return "Ok";
        }
        public List<FavouriteModel> GetFavMenus(int RoleId, int UserId)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@RoleId", RoleId);
            para[1] = new SqlParameter("@UserId", UserId);
            return GetList<FavouriteModel>(@"select @UserId UserId,M.LinkName,M.Icon,M.LinkAction,P.MenuId,CONVERT(BIT,CASE WHEN F.FavouriteId>0 THEN 1 ELSE 0 END) IsFavourite from MenuMaster M
INNER JOIN PermissionMaster P ON M.Id=P.MenuId AND P.IsView=1 AND P.RoleId=@RoleId
LEFT JOIN T_FavouriteUserList F ON M.Id=F.MenuId AND F.UserId=@UserId", para);
        }

    }
}
