using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.Users;
using HIMS.Model.Users;
using System.Collections.Generic;

namespace HIMS.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly I_Role i_Role;
        public RoleController(I_Role i_Role)
        {
            this.i_Role = i_Role;
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
        public IActionResult GetPermission()
        {
            return Ok(i_Role.GetPermisison());
        }
        [HttpPost]
        [Route("save-permission")]
        public IActionResult PostPermission(List<PermissionModel> obj)
        {
            i_Role.SavePermission(obj);
            return Ok();
        }
    }
}
