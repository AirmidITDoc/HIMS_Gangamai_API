using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Users;
using HIMS.Model.Users;
using HIMS.Model.Inventory;
using HIMS.Data.Inventory;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class AdministrationController : Controller
    {
        public readonly I_UserChangePassword _UserChangePassword;
        public readonly I_SMS_Config _SMS_Config;
        /* public IActionResult Index()
         {
             return View();
         }*/
        public AdministrationController(
            I_UserChangePassword UserChangePassword,
            I_SMS_Config sMS_Config)
        {
            this._UserChangePassword = UserChangePassword;
            this._SMS_Config = sMS_Config;
        }

        [HttpPost("UserChangePassword")]
        public IActionResult UserChangePassword(UserChangePasswordParams userChangePasswordParams)
        {
            var UserName = _UserChangePassword.Update(userChangePasswordParams);
            return Ok(UserName);
        }

        [HttpPost("InsertLoginUser")]
        public IActionResult UserInsert(UserChangePasswordParams userChangePasswordParams)
        {
            var UserName = _UserChangePassword.Insertlogin(userChangePasswordParams);
            return Ok(UserName);
        }

        [HttpPost("UpdateLoginUser")]
        public IActionResult Userupdate(UserChangePasswordParams userChangePasswordParams)
        {
            var UserName = _UserChangePassword.UpdateLogin(userChangePasswordParams);
            return Ok(UserName);
        }
        [HttpPost("SMS_Configsave")]
        public IActionResult SMS_Configsave(SMS_ConfigParam SMS_ConfigParam)
        {
            var TODSave = _SMS_Config.InsertSMSConfig(SMS_ConfigParam);
            return Ok(TODSave);

        }
        [HttpPost("SMS_ConfigUpdate")]
        public IActionResult SMS_ConfigUpdate(SMS_ConfigParam SMS_ConfigParam)
        {
            var TODUpdate = _SMS_Config.UpdateSMSConfigParam(SMS_ConfigParam);
            return Ok(TODUpdate);

        }
    }

}

