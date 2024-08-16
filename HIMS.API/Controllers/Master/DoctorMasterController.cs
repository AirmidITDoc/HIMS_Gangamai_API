using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Master.DoctorMaster;
using HIMS.Model.Master.DoctorMaster;
using HIMS.API.Utility;

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
        private readonly IFileUtility _FileUtility;
        public DoctorMasterController(I_DoctorMaster doctorMaster, I_DoctorTypeMaster doctorTypeMaster, IFileUtility fileUtility)
        {
            this._DoctorMaster = doctorMaster;
            this._DoctorTypeMaster = doctorTypeMaster;
            _FileUtility = fileUtility;
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
            if (!string.IsNullOrWhiteSpace(DoctorMasterParams.InsertDoctorMaster.Signature))
                DoctorMasterParams.InsertDoctorMaster.Signature = _FileUtility.SaveImageFromBase64(DoctorMasterParams.InsertDoctorMaster.Signature, "Doctors\\Signature");
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