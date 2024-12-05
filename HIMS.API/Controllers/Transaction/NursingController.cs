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

namespace HIMS.API.Controllers.Transaction
{
        [ApiController]
        [Route("api/[controller]")]
        public class NursingController : Controller
        {
            public readonly I_RadiologyTemplateResult i_RadiologyTemplate;
        public readonly I_Administration _Administration;
        public readonly IPdfUtility _pdfUtility;
            private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
            public readonly IFileUtility _FileUtility;
            public NursingController(I_RadiologyTemplateResult i_Radiology,
                I_Administration Administration,
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, 
                IPdfUtility pdfUtility, IFileUtility fileUtility)
            {
                this.i_RadiologyTemplate = i_Radiology;
                _hostingEnvironment = hostingEnvironment;
            this._Administration = Administration;
            _pdfUtility = pdfUtility;
                _FileUtility = fileUtility;
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

        }
 }

