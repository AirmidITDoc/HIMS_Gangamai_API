using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using HIMS.API.Utility;
using HIMS.Data.Radiology;
using HIMS.Model.Radiology;
using System.Data;

namespace HIMS.API.Controllers.Transaction
{
        [ApiController]
        [Route("api/[controller]")]
        public class NursingController : Controller
        {
            public readonly I_RadiologyTemplateResult i_RadiologyTemplate;

            public readonly IPdfUtility _pdfUtility;
            private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
            public readonly IFileUtility _FileUtility;
            public NursingController(I_RadiologyTemplateResult i_Radiology, 
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, 
                IPdfUtility pdfUtility, IFileUtility fileUtility)
            {
                this.i_RadiologyTemplate = i_Radiology;
                _hostingEnvironment = hostingEnvironment;
                _pdfUtility = pdfUtility;
                _FileUtility = fileUtility;
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

