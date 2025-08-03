using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Users;
using HIMS.Model.Users;
using HIMS.Model.Inventory;
using HIMS.Data.Inventory;
using HIMS.Model.Administration;
using HIMS.Data.Administration;
using HIMS.Model.Opd;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]

    public class PhysiotherapyController : Controller
    {
        public readonly I_UserChangePassword _UserChangePassword;
        public readonly I_SMS_Config _SMS_Config;
        public readonly I_Administration _Administration;
        public readonly I_NewTemplateDescription _NewTemplateDescription;
        /* public IActionResult Index()
         {
             return View();
         }*/
        public PhysiotherapyController(
            I_UserChangePassword UserChangePassword,
            I_SMS_Config sMS_Config, I_Administration Administration, I_NewTemplateDescription newTemplateDescription)
        {
            this._UserChangePassword = UserChangePassword;
            this._SMS_Config = sMS_Config;
            this._Administration = Administration;
            this._NewTemplateDescription = newTemplateDescription;
        }
        [HttpPost("InsertPhysiotherapy")]
        public IActionResult InsertPhysiotherapy(PhysiotherapyParam PhysiotherapyParam)
        {
            var appoSave = _Administration.InsertPhysiotherapy(PhysiotherapyParam);
            return Ok(appoSave);
        }
        [HttpPost("UpdatePhysiotherapy")]
        public IActionResult UpdatePhysiotherapy(PhysiotherapyParam PhysiotherapyParam)
        {
            var appoSave = _Administration.UpdatePhysiotherapy(PhysiotherapyParam);
            return Ok(appoSave);
        }
    }

}

