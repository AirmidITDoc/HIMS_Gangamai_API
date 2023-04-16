using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Master.DoctorMaster;
using HIMS.Model.Master.DoctorMaster;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorMasterController : Controller
    {
        /*  public IActionResult Index()
          {
              return View();
          }*/
        public readonly I_DoctorMaster _DoctorMaster;
        public readonly I_DoctorTypeMaster _DoctorTypeMaster;
        public DoctorMasterController(I_DoctorMaster doctorMaster,I_DoctorTypeMaster doctorTypeMaster)
        {
            this._DoctorMaster = doctorMaster;
            this._DoctorTypeMaster = doctorTypeMaster;
        }
        //DoctorType Save and Update
        [HttpPost("DoctorTypeSave")]
        public IActionResult DoctorTypeSave(DoctorTypeMasterParams DoctorTypeMasterParams)
        {
            var ServiceSave = _DoctorTypeMaster.Save(DoctorTypeMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("DoctorTypeUpdate")]
        public IActionResult DoctorTypeUpdate(DoctorTypeMasterParams DoctorTypeMasterParams)
        {
            var ServiceSave = _DoctorTypeMaster.Update(DoctorTypeMasterParams);
            return Ok(ServiceSave);
        }

        //Doctor Save and Update
        [HttpPost("DoctorSave")]
        public IActionResult DoctorSave(DoctorMasterParams DoctorMasterParams)
        {
            var ServiceSave = _DoctorMaster.Save(DoctorMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("DoctorUpdate")]
        public IActionResult DoctorSaveUpdate(DoctorMasterParams DoctorMasterParams)
        {
            var ServiceSave = _DoctorMaster.Update(DoctorMasterParams);
            return Ok(ServiceSave);
        }
    }
}