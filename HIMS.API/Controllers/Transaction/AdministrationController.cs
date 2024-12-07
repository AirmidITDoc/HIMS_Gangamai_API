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
    public class AdministrationController : Controller
    {
        public readonly I_UserChangePassword _UserChangePassword;
        public readonly I_SMS_Config _SMS_Config;
        public readonly I_Administration _Administration;
        public readonly I_NewTemplateDescription _NewTemplateDescription;
        /* public IActionResult Index()
         {
             return View();
         }*/
        public AdministrationController(
            I_UserChangePassword UserChangePassword,
            I_SMS_Config sMS_Config, I_Administration Administration, I_NewTemplateDescription newTemplateDescription)
        {
            this._UserChangePassword = UserChangePassword;
            this._SMS_Config = sMS_Config;
            this._Administration = Administration;
            this._NewTemplateDescription = newTemplateDescription;
        }
        [HttpPost("SaveFeedBack")]
        public IActionResult Save(Parameter PatientFeedbackParameter)
        {
            var appoSave = _Administration.SavePatientFeedBack(PatientFeedbackParameter);
            return Ok(appoSave);
        }
        //[HttpPost("SaveNursingPainAssessment")]
        //public IActionResult SaveNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        //{
        //    var appoSave = _Administration.SaveNursingPainAssessment(SaveNursingPainAssessmentParam);
        //    return Ok(appoSave);
        //}

        //[HttpPost("UpdateNursingPainAssessment")]
        //public IActionResult UpdateNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        //{
        //    var appoSave = _Administration.UpdateNursingPainAssessment(SaveNursingPainAssessmentParam);
        //    return Ok(appoSave);
        //}

        [HttpPost("InsertPackageDetails")]
        public IActionResult InsertPackageDetails(PackageDetailParam PackageDetailParam)
        {
            var appoSave = _Administration.InsertPackageDetails(PackageDetailParam);
            return Ok(appoSave);
        }

        [HttpPost("InsertCompanyServiceAssignMaster")]
        public IActionResult InsertCompanyServiceAssignMaster(CompanyServiceAssignMaster CompanyServiceAssignMaster)
        {
            var appoSave = _Administration.InsertCompanyServiceAssignMaster(CompanyServiceAssignMaster);
            return Ok(appoSave);
        }
        [HttpPost("SaveTExpenseParam")]
        public IActionResult SaveTExpenseParam(TExpenseParam TExpenseParam)
        {
            var appoSave = _Administration.SaveTExpenseParam(TExpenseParam);
            return Ok(appoSave);
        }

        [HttpPost("CancleTExpenseParam")]
        public IActionResult CancleTExpenseParam(TExpenseParam TExpenseParam)
        {
            var appoSave = _Administration.CancleTExpenseParam(TExpenseParam);
            return Ok(appoSave);
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

        [HttpPost("Billcancellation")]
        public IActionResult Billcancellation(AdministrationParam administrationParams)
        {
            var TODUpdate = _Administration.UpdateBillcancellation(administrationParams);
            return Ok(TODUpdate);

        }


        [HttpPost("InsertDoctorShareMaster")]
        public IActionResult InsertDoctorShareMaster(DoctorShareParam doctorShareParam)
        {
            var TODUpdate = _Administration.InsertDoctorShareMaster(doctorShareParam);
            return Ok(TODUpdate);

        }

        [HttpPost("UpdateDoctorShareMaster")]
        public IActionResult UpdateDoctorShareMaster(DoctorShareParam doctorShareParam)
        {
            var TODUpdate = _Administration.UpdateDoctorShareMaster(doctorShareParam);
            return Ok(TODUpdate);

        }


        [HttpPost("DoctorShareProcess")]
        public IActionResult DoctorShareProcess(DoctorShareProcessParam doctorShareProcessParam)
        {
            var TODUpdate = _Administration.DoctorShareProcess(doctorShareProcessParam);
            return Ok(TODUpdate);

        }
        [HttpPost("IPDischargeCancel")]
        public IActionResult IPDischargeCancel(IPDischargeCancelParam iPDischargeCancelParam)
        {
            var TODUpdate = _Administration.IPDischargeCancel(iPDischargeCancelParam);
            return Ok(TODUpdate);

        }

        [HttpPost("NewTemplatedesc")]
        public IActionResult NewTemplatedesc(NewTemplateDescriptionParam NewTemplateDescriptionParam)
        {
            var TODUpdate = _NewTemplateDescription.Insert(NewTemplateDescriptionParam);
            return Ok(TODUpdate);

        }

        [HttpPost("UpdateTemplatedesc")]
        public IActionResult UpdateTemplatedesc(NewTemplateDescriptionParam NewTemplateDescriptionParam)
        {
            var TODUpdate = _NewTemplateDescription.Update(NewTemplateDescriptionParam);
            return Ok(TODUpdate);

        }
    }

}

