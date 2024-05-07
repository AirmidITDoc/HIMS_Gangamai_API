using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.Users;
using HIMS.Model.Users;
using System.Collections.Generic;
using HIMS.Data.Master;
using Newtonsoft.Json.Linq;

namespace HIMS.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly I_Favourite i_Favourite;
        public FavouriteController(I_Favourite _Favourite)
        {
            this.i_Favourite = _Favourite;
        }
        [HttpGet]
        [Route("get-favmenus")]
        public IActionResult GetRoles(int RoleId, int UserId)
        {
            return Ok(i_Favourite.GetFavMenus(RoleId, UserId));
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(FavouriteDto obj)
        {
            if (obj.UserId <= 0 || obj.MenuId <= 0)
                return BadRequest("Invalid params.");
            i_Favourite.Insert(obj.UserId,obj.MenuId);
            return Ok();
        }
    }
}
