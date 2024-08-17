using HIMS.API.Utility;
using HIMS.Data.DoctorShareReports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Transaction
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class DoctorShareReportsController : Controller
    {
        public readonly I_DoctorShareReport _doctorShareReport;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private I_DoctorShareReport doctorShareReport;

        public DoctorShareReportsController(
           I_DoctorShareReport doctorShareReport, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _doctorShareReport = doctorShareReport;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        
    }
        [HttpGet("view-DoctorShareReport")]
        public IActionResult ViewDoctorShareReport(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_DoctorShareReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewDoctorShareReport(FromDate, ToDate,  DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorShareReport", "DoctorShareReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("viewDoctorWiseSummaryReport")]
        public IActionResult ViewDoctorWiseSummaryReport(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_DoctorWiseSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewDoctorWiseSummaryReport(FromDate, ToDate,  DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseSummaryReport", "DoctorWiseSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
      

    }
}


