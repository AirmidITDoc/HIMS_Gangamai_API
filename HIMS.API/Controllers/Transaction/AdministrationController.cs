using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Users;
using HIMS.Model.Users;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdministrationController : Controller
    {
        public readonly I_UserChangePassword _UserChangePassword;
        /* public IActionResult Index()
         {
             return View();
         }*/
        public AdministrationController(
            I_UserChangePassword UserChangePassword
            )
        {
            this._UserChangePassword = UserChangePassword;
        }

        [HttpPost("UserChangePassword")]
        public IActionResult UserChangePassword(UserChangePasswordParams userChangePasswordParams)
        {
            var UserName = _UserChangePassword.Update(userChangePasswordParams);
            return Ok(UserName);
        }

    }

}

