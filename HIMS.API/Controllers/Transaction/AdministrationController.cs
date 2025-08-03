using HIMS.API.Utility;
using HIMS.Data.Administration;
using HIMS.Data.Inventory;
using HIMS.Data.Users;
using HIMS.Model.Administration;
using HIMS.Model.Inventory;
using HIMS.Model.Opd;
using HIMS.Model.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public AdministrationController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility,
            I_UserChangePassword UserChangePassword,
            I_SMS_Config sMS_Config, I_Administration Administration, I_NewTemplateDescription newTemplateDescription, I_ReportConfig ReportConfig)
        {
            this._UserChangePassword = UserChangePassword;
            this._SMS_Config = sMS_Config;
            this._Administration = Administration;
            this._NewTemplateDescription = newTemplateDescription;
        }
        [HttpGet("view-ExpensesReport")]
        public IActionResult ViewExpensesReport(DateTime FromDate, DateTime ToDate, int ExpHeadId, int ExpType)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DailyExpensesReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Administration.ViewExpensesReport(FromDate, ToDate, ExpHeadId, ExpType, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ViewExpensesReport", "ViewExpensesReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-VoucharPrint")]
        public IActionResult ViewVoucharPrint( int ExpId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "VoucherPrint.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Administration.ViewVoucharPrint(ExpId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ViewVoucharPrint", "ViewVoucharPrint", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpPost("MClassMasterInsert")]
        public IActionResult MClassMasterInsert(ClassMasterPara ClassMasterPara)
        {
            var appoSave = _Administration.MClassMasterInsert(ClassMasterPara);
            return Ok(appoSave);
        }
        [HttpPost("MClassMasterUpdate")]
        public IActionResult MClassMasterUpdate(ClassMasterPara ClassMasterPara)
        {
            var appoSave = _Administration.MClassMasterUpdate(ClassMasterPara);
            return Ok(appoSave);
        }
        [HttpPost("InsertGSTReCalculProcess")]
        public IActionResult InsertGSTReCalculProcess(GSTReCalculProcessParam GSTReCalculProcessParam)
        {
            var appoSave = _Administration.InsertGSTReCalculProcess(GSTReCalculProcessParam);
            return Ok(appoSave);
        }
        [HttpPost("SaveMExpensesHeadMaster")]
        public IActionResult SaveMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam)
        {
            var appoSave = _Administration.SaveMExpensesHeadMaster(MExpensesHeadMasterParam);
            return Ok(appoSave);
        }

        [HttpPost("UpdateMExpensesHeadMaster")]
        public IActionResult UpdateMExpensesHeadMaster(MExpensesHeadMasterParam MExpensesHeadMasterParam)
        {
            var appoSave = _Administration.UpdateMExpensesHeadMaster(MExpensesHeadMasterParam);
            return Ok(appoSave);
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



        [HttpPost("ReportConfigsave")]
        public IActionResult InsertReportConfig(ReportConfigparam ReportConfigparam)
        {
            var TODUpdate = _I_ReportConfig.InsertReportConfig(ReportConfigparam);
            return Ok(TODUpdate);

        }


        [HttpPost("ReportConfigUpdate")]
        public IActionResult UpdateReportConfig(ReportConfigparam ReportConfigparam)
        {
            var TODUpdate = _I_ReportConfig.UpdateReportConfig(ReportConfigparam);
            return Ok(TODUpdate);

        }

        //[HttpPut("ReportConfig/{id:int}")]

        //public async Task<ApiResponse> Edit(ReportConfigModel obj)
        //{
        //    MReportConfig model = obj.MapTo<MReportConfig>();
        //    if (obj.ReportId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.UpdatedOn = DateTime.Now;
        //        model.UpdateBy = CurrentUserId;
        //        model.IsActive = true;
        //        await _ReportConfigService.UpdateAsyncm(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}


    }

}

