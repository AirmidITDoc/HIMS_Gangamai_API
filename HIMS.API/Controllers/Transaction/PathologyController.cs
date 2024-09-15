using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using HIMS.Model.Pathology;
using HIMS.Data.Pathology;
using System.IO;
using HIMS.API.Utility;
using Microsoft.Extensions.Configuration;
using System.Data;
using HIMS.Common.Utility;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class PathologyController : Controller
    {

        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;


        public readonly I_PathologyTemplateResult _PathologyTemplateResult;
        public readonly I_Pathologysamplecollection _Pathologysamplecollection;
        public readonly I_pathresultentry _Pathresultentry;
        public readonly IConfiguration _configuration;
        public readonly IFileUtility _FileUtility;
        public PathologyController(I_PathologyTemplateResult pathologyTemplateResult, IConfiguration configuration, IFileUtility fileUtility,
            I_Pathologysamplecollection pathologysamplecollection, I_pathresultentry pathresultentry, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            this._PathologyTemplateResult = pathologyTemplateResult;
            this._Pathologysamplecollection = pathologysamplecollection;
            this._Pathresultentry = pathresultentry;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _configuration = configuration;
            _FileUtility = fileUtility;
        }

        [HttpPost("PathologyTemplateResult")]
        public IActionResult PathologyTemplateResult(PathologyTemplateResultParams PTRP)
        {
            var PTR1 = _PathologyTemplateResult.Insert(PTRP);
            return Ok(PTR1);
        }


        [HttpGet("view-PathTemplate")]
        public IActionResult ViewPathTemplate(int PathReportId, int OP_IP_Type)
        {
            //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTemplate.html");
            //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            //var html = _PathologyTemplateResult.ViewPathTemplateReceipt(PathReportId, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            //html = html.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));
            //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PathTemplate", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTemplate.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
          
            DataTable dt = _PathologyTemplateResult.GetDataForReport(PathReportId,OP_IP_Type);
        
            var html = _PathologyTemplateResult.ViewPathTemplateReceipt(dt, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var signature = _FileUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());
            html = html.Replace("{{Signature}}", signature);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PathTemplate", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpPost("PathSamplecollection")]
        public IActionResult PathSamplecollection(Pathologysamplecollectionparameter PTRP)
        {
            var PTR1 = _Pathologysamplecollection.Update(PTRP);
            return Ok(PTR1);
        }

        [HttpPost("PathResultentryInsert")]
        public IActionResult PathResultentryInsert(pathresultentryparam pathresultentryparam)
        {
            var PTR1 = _Pathresultentry.Insert(pathresultentryparam);
            return Ok(PTR1);
        }

        [HttpPost("PathPrintResultentryInsert")]
        public IActionResult PrintPathResultentryInsert(pathresultentryparam pathresultentryparam)
        {
            var PTR1 = _Pathresultentry.PrintInsert(pathresultentryparam);
            return Ok(PTR1);
        }

        [HttpPost("PathResultentryrollback")]
        public IActionResult PathResultentryRollback(pathresultentryparam pathresultentryparam)
        {
            var PTR1 = _Pathresultentry.Rollback(pathresultentryparam);
            return Ok(PTR1);
        }



        [HttpGet("view-PathReportMultiple")]
        public IActionResult ViewPathReportMultiple(int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathologyResultTest.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            
            string header = _pdfUtility.GetHeader(10056,2); // store header
            string header1 = _pdfUtility.GetHeader(6, 1);// hospital header

            header1= header1.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));
            DataTable dt = _Pathresultentry.GetDataForReport(OP_IP_Type);
            //var html = _Pathresultentry.ViewPathTestMultipleReport(dt, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));

            var html = _Pathresultentry.ViewPathTestMultipleReport(dt, htmlFilePath, header1);
            var signature = _FileUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());
            
            html = html.Replace("{{Signature}}", signature);
            
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PathTestReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }

}

