using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using HIMS.Model.Radiology;
using HIMS.Data.Radiology;
using System.IO;
using HIMS.API.Utility;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class RadiologyController : Controller
    {
        public readonly I_RadiologyTemplateResult _RadiologyReportHeaderUpdate;

        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public RadiologyController(I_RadiologyTemplateResult i_Radiology, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            this._RadiologyReportHeaderUpdate = i_Radiology;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        [HttpPost("RadiologyTemplateResult")]
        public IActionResult RadiologyTemplateResult(RadiologyTemplateResultParams RRHUP)
        {
            var RRHUPI = _RadiologyReportHeaderUpdate.Update(RRHUP);
            return Ok(RRHUPI);
        }

        [HttpGet("view-RadiologyTemplateReport")]
        public IActionResult ViewRadiologyTemplateReport(int RadReportId, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RadiologyTemplateReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _RadiologyReportHeaderUpdate.ViewRadiologyTemplateReceipt(RadReportId, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RadiologyTemplateReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }

}

