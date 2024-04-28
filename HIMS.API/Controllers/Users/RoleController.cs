using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.Users;
using HIMS.Model.Users;
using System.Collections.Generic;
using HIMS.Data.Master;

namespace HIMS.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly I_Role i_Role;
        private readonly I_MenuMaster i_MenuMaster;
        public RoleController(I_Role i_Role, I_MenuMaster i_MenuMaster)
        {
            this.i_Role = i_Role;
            this.i_MenuMaster = i_MenuMaster;
        }
        [HttpGet]
        [Route("get-roles")]
        public IActionResult GetRoles(string RoleName)
        {
            return Ok(i_Role.GetRoles(RoleName));
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(RoleModel obj)
        {
            i_Role.Insert(obj);
            return Ok(obj);
        }
        [HttpGet]
        [Route("get-permissions")]
        public IActionResult GetPermission(int RoleId)
        {
            return Ok(i_MenuMaster.GetPermisison(RoleId));
        }
        [HttpPost]
        [Route("save-permission")]
        public IActionResult PostPermission(List<PermissionModel> obj)
        {
            i_MenuMaster.SavePermission(obj);
            return Ok();
        }
    }
}
