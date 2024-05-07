using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Master;
using HIMS.Model.Users;
using Newtonsoft.Json.Linq;

namespace HIMS.Data.Users
{
    public interface I_Favourite
    {
        public string Insert(int UserId,int MenuId);
        List<FavouriteModel> GetFavMenus(int RoleId, int UserId);
    }
}
