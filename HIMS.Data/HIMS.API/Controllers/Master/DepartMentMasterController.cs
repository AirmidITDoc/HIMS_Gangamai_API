using HIMS.Data.Master.DepartMentMaster;
using HIMS.Model.Master.DepartmenMaster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartMentMasterController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/
        public readonly I_DepartmentMaster _DepartmentMaster;
        public readonly I_LocationMaster _LocationMaster;
        public readonly I_WardMaster _WardMaster;
        public readonly I_BedMaster _BedMaster;
        public readonly I_DischargeTypeMaster _DischargeTypeMaster;

        public DepartMentMasterController(I_DepartmentMaster departmentMaster, I_LocationMaster locationMaster,
            I_WardMaster wardMaster, I_BedMaster bedMaster, I_DischargeTypeMaster dischargeTypeMaster)
        {
            this._DepartmentMaster = departmentMaster;
            this._LocationMaster = locationMaster;
            this._WardMaster = wardMaster;
            this._BedMaster = bedMaster;
            this._DischargeTypeMaster = dischargeTypeMaster;
        }

        //DepartmentMaster Save and Update
        [HttpPost("DepartmentSave")]
        public IActionResult DepartmentSave(DepartmentMasterParams DepartmentMasterParams)
        {
            var ServiceSave = _DepartmentMaster.Save(DepartmentMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("DepartmentUpdate")]
        public IActionResult DepartmentUpdate(DepartmentMasterParams DepartmentMasterParams)
        {
            var ServiceSave = _DepartmentMaster.Update(DepartmentMasterParams);
            return Ok(ServiceSave);
        }
        //DepartmentMaster Save and Update
        [HttpPost("LocationSave")]
        public IActionResult LocationSave(LocationMasterParams LocationMasterParams)
        {
            var ServiceSave = _LocationMaster.Save(LocationMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("LocationtUpdate")]
        public IActionResult LocationUpdate(LocationMasterParams LocationMasterParams)
        {
            var ServiceSave = _LocationMaster.Update(LocationMasterParams);
            return Ok(ServiceSave);
        }

        //WardMaster Save and Update
        [HttpPost("WardSave")]
        public IActionResult WardSave(WardMasterParams WardMasterParams)
        {
            var ServiceSave = _WardMaster.Save(WardMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("WardUpdate")]
        public IActionResult WardUpdate(WardMasterParams WardMasterParams)
        {
            var ServiceSave = _WardMaster.Update(WardMasterParams);
            return Ok(ServiceSave);
        }
        //BedMaster Save and Update
        [HttpPost("BedSave")]
        public IActionResult BedSave(BedMasterParams BedMasterParams)
        {
            var ServiceSave = _BedMaster.Save(BedMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("BedUpdate")]
        public IActionResult BedUpdate(BedMasterParams BedMasterParams)
        {
            var ServiceSave = _BedMaster.Update(BedMasterParams);
            return Ok(ServiceSave);
        }

        //DischargeTypeMaster Save and Update
        [HttpPost("DischargeTypeMasterSave")]
        public IActionResult DischargeTypeMasterSave(DischargeTypeMasterParams DischargeTypeMasterParams)
        {
            var ServiceSave = _DischargeTypeMaster.Save(DischargeTypeMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("DischargeTypeMasterUpdate")]
        public IActionResult LocationUpdate(DischargeTypeMasterParams DischargeTypeMasterParams)
        {
            var ServiceSave = _DischargeTypeMaster.Update(DischargeTypeMasterParams);
            return Ok(ServiceSave);
        }
    }
}