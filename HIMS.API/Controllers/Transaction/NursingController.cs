using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using HIMS.API.Utility;
using HIMS.Data.Radiology;
using HIMS.Model.Radiology;
using System.Data;
using HIMS.Model.Administration;
using HIMS.Model.Administration;
using HIMS.Data.Administration;
using HIMS.API.Comman;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;

namespace HIMS.API.Controllers.Transaction
{
        [ApiController]
        [Route("api/[controller]")]
        public class NursingController : Controller
        {
            public readonly I_CustomerInformation _CustomerInformation;
        public readonly I_RadiologyTemplateResult i_RadiologyTemplate;
        public readonly I_Administration _Administration;
        public readonly IPdfUtility _pdfUtility;
            private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
            public readonly IFileUtility _FileUtility;
            public NursingController(I_RadiologyTemplateResult i_Radiology,
                I_Administration Administration,
                 I_CustomerInformation I_CustomerInformation,
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, 
                IPdfUtility pdfUtility, IFileUtility fileUtility)
            {
                this.i_RadiologyTemplate = i_Radiology;
            this._CustomerInformation = I_CustomerInformation;
            _hostingEnvironment = hostingEnvironment;
            this._Administration = Administration;
            _pdfUtility = pdfUtility;
                _FileUtility = fileUtility;
            }
        [HttpPost("SaveTNursingNotes")]
        public IActionResult SaveTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            var Id = _CustomerInformation.SaveTNursingNotes(NursingNoteParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateTNursingNotes")]
        public IActionResult UpdateTNursingNotes(NursingNoteParam NursingNoteParam)
        {
            var Id = _CustomerInformation.UpdateTNursingNotes(NursingNoteParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Updated successfully", Id);
            return Ok(Response);
        }

        [HttpPost("SaveTNursingPatientHandover")]
        public IActionResult SaveTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            var Id = _CustomerInformation.SaveTNursingPatientHandover(TNursingPatientHandoverParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateTNursingPatientHandover")]
        public IActionResult UpdateTNursingPatientHandover(TNursingPatientHandoverParam TNursingPatientHandoverParam)
        {
            var Id = _CustomerInformation.UpdateTNursingPatientHandover(TNursingPatientHandoverParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Updated successfully", Id);
            return Ok(Response);
        }
        [HttpPost("SaveTNursingMedicationChart")]
        public IActionResult SaveTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            var Id = _CustomerInformation.SaveTNursingMedicationChart(TNursingMedicationChartParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateTNursingMedicationChart")]
        public IActionResult UpdateTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            var Id = _CustomerInformation.UpdateTNursingMedicationChart(TNursingMedicationChartParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Updated successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelTNursingMedicationChart")]
        public IActionResult CancelTNursingMedicationChart(TNursingMedicationChartParam TNursingMedicationChartParam)
        {
            var Id = _CustomerInformation.CancelTNursingMedicationChart(TNursingMedicationChartParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Canceled successfully", Id);
            return Ok(Response);
        }
        [HttpPost("SaveNursingWeight")]
        public IActionResult SaveNursingWeight(NursingWeightParam NursingWeightParam)
        {
            var appoSave = _Administration.SaveNursingWeight(NursingWeightParam);
            return Ok(appoSave);
        }
        [HttpPost("UpdateNursingWeight")]
        public IActionResult UpdateNursingWeight(NursingWeightParam NursingWeightParam)
        {
            var appoSave = _Administration.UpdateNursingWeight(NursingWeightParam);
            return Ok(appoSave);
        }

        [HttpPost("SaveNursingOrygenVentilator")]
        public IActionResult SaveNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam)
        {
            var appoSave = _Administration.SaveNursingOrygenVentilator(NursingOrygenVentilatorParam);
            return Ok(appoSave);
        }

        [HttpPost("UpdateNursingOrygenVentilator")]
        public IActionResult UpdateNursingOrygenVentilator(NursingOrygenVentilatorParam NursingOrygenVentilatorParam)
        {
            var appoSave = _Administration.UpdateNursingOrygenVentilator(NursingOrygenVentilatorParam);
            return Ok(appoSave);
        }

        [HttpPost("SaveNursingVitals")]
        public IActionResult SaveNursingVitals(NursingVitalsParam NursingVitalsParam)
        {
            var appoSave = _Administration.SaveNursingVitals(NursingVitalsParam);
            return Ok(appoSave);
        }

        [HttpPost("UpdateNursingVitals")]
        public IActionResult UpdateNursingVitals(NursingVitalsParam NursingVitalsParam)
        {
            var appoSave = _Administration.UpdateNursingVitals(NursingVitalsParam);
            return Ok(appoSave);
        }


        [HttpPost("SaveNursingSugarLevel")]
        public IActionResult SaveNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam)
        {
            var appoSave = _Administration.SaveNursingSugarLevel(NursingSugarLevelParam);
            return Ok(appoSave);
        }

        [HttpPost("UpdateNursingSugarLevel")]
        public IActionResult UpdateNursingSugarLevel(NursingSugarLevelParam NursingSugarLevelParam)
        {
            var appoSave = _Administration.UpdateNursingSugarLevel(NursingSugarLevelParam);
            return Ok(appoSave);
        }

        [HttpPost("SaveDischargeInitiate")]
        public IActionResult SaveDischargeInitiate(DischargeInitiateParam DischargeInitiateParam)
        {
            var appoSave = _Administration.SaveDischargeInitiate(DischargeInitiateParam);
            return Ok(appoSave);
        }

        //[HttpPost("UpdateDischargeInitiate")]
        //public IActionResult UpdateDischargeInitiate(DischargeInitiateParam DischargeInitiateParam)
        //{
        //    var appoSave = _Administration.UpdateDischargeInitiate(DischargeInitiateParam);
        //    return Ok(appoSave);
        //}
        [HttpPost("UpdateDischargeInitiateApproval")]
        public IActionResult UpdateDischargeInitiateApproval(DischargeInitiateApprovalParam DischargeInitiateApprovalParam)
        {
            var appoSave = _Administration.UpdateDischargeInitiateApproval(DischargeInitiateApprovalParam);
            return Ok(appoSave);
        }

        [HttpPost("SaveUptDocMerge")]
        public IActionResult SaveUptDocMerge(UptDocMergeParam UptDocMergeParam)
        {
            var appoSave = _Administration.SaveUptDocMerge(UptDocMergeParam);
            return Ok(appoSave);
        }
        [HttpPost("SaveNursingPainAssessment")]
        public IActionResult SaveNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        {
            var appoSave = _Administration.SaveNursingPainAssessment(SaveNursingPainAssessmentParam);
            return Ok(appoSave);
        }

        [HttpPost("UpdateNursingPainAssessment")]
        public IActionResult UpdateNursingPainAssessment(SaveNursingPainAssessmentParam SaveNursingPainAssessmentParam)
        {
            var appoSave = _Administration.UpdateNursingPainAssessment(SaveNursingPainAssessmentParam);
            return Ok(appoSave);
        }
        [HttpPost("RadiologyTemplateResult")]
        public IActionResult RadiologyTemplateResult(RadiologyTemplateResultParams RRHUP)
        {
            var RRHUPI = i_RadiologyTemplate.Update(RRHUP);
            return Ok(RRHUPI);
        }

        [HttpGet("view-RadiologyTemplateReport")]
        public IActionResult ViewRadiologyTemplateReport(int RadReportId, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RadiologyTemplateReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            //var html = i_RadiologyTemplate.ViewRadiologyTemplateReceipt(RadReportId, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RadiologyTemplateReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            DataTable dt = i_RadiologyTemplate.GetDataForReport(RadReportId, OP_IP_Type);
            var html = i_RadiologyTemplate.ViewRadiologyTemplateReceipt(dt, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            //var signature = _FileUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());
            //html = html.Replace("{{Signature}}", signature);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RadiologyTemplateReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpPost("SaveTDoctorsNotes")]
        public IActionResult SaveTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam)
        {
            var appoSave = _Administration.SaveTDoctorsNotes(TDoctorsNotesParam);
            return Ok(appoSave);
        }
        [HttpPost("UpdateTDoctorsNotes")]
        public IActionResult UpdateTDoctorsNotes(TDoctorsNotesParam TDoctorsNotesParam)
        {
            var appoSave = _Administration.UpdateTDoctorsNotes(TDoctorsNotesParam);
            return Ok(appoSave);
        }
        [HttpPost("SaveTDoctorPatientHandover")]
        public IActionResult SaveTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam)
        {
            var appoSave = _Administration.SaveTDoctorPatientHandover(TDoctorPatientHandoverParam);
            return Ok(appoSave);
        }
        [HttpPost("UpdateTDoctorPatientHandover")]
        public IActionResult UpdateTDoctorPatientHandover(TDoctorPatientHandoverParam TDoctorPatientHandoverParam)
        {
            var appoSave = _Administration.UpdateTDoctorPatientHandover(TDoctorPatientHandoverParam);
            return Ok(appoSave);
        }



    }
}

