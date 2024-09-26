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
        public IActionResult ViewDoctorShareReport(int Doctor_Id, int GroupId, DateTime From_Dt, DateTime To_Dt, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_DoctorShareReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewDoctorShareReport(Doctor_Id, GroupId, From_Dt, To_Dt, OP_IP_Type,htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorShareReport", "DoctorShareReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("viewDoctorWiseSummaryReport")]
        public IActionResult ViewDoctorWiseSummaryReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_DoctorWiseSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewDoctorWiseSummaryReport(FromDate, ToDate,   htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseSummaryReport", "DoctorWiseSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("ViewConDoctorShareDetails")]
        public IActionResult ViewConDoctorShareDetails(int Doctor_Id, int GroupId, DateTime From_Dt, DateTime To_Dt, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_ConDoctorShareDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewConDoctorShareDetails(Doctor_Id, GroupId, From_Dt, To_Dt, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ConDoctorShareDetails", "ConDoctorShareDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("ViewDoctorShareListWithCharges")]
        public IActionResult ViewDoctorShareListWithCharges(DateTime FromDate, DateTime Todate, int Doctor_Id,int GroupId, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorShare_DoctorShareListWithCharges.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _doctorShareReport.ViewDoctorShareListWithCharges(FromDate, Todate, Doctor_Id, GroupId, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorShareListWithCharges", "DoctorShareListWithCharges", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


    }
}


