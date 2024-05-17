using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.OPReports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class OPReportController : Controller
    {
        public readonly I_OPBillingReport _OPbilling;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public OPReportController(
           I_OPBillingReport oPbilling, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _OPbilling = oPbilling;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;

        }

        [HttpGet("view-OPDailyCollectionReport")]
        public IActionResult ViewOPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPNewDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDailyCollectionReceipt(FromDate, ToDate, AddedById, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPNewDailycollection", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-RegistrationReport ")]
        public IActionResult ViewRegistrationReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RegistrationReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewRegistrationReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RegistrationReport ", "RegistrationReport ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




            [HttpGet("view-AppointmentListReport")]
        public IActionResult ViewOPAppointmentListReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppoitnmentListReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPAppointmentListReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AppointmentListReport", "AppointmentListReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });


        }


        [HttpGet("view-DoctorWiseVisitReport")]
        public IActionResult ViewDoctorWiseVisitReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseVisitReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseVisitReport ", "DoctorWiseVisitReport ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-RefDoctorWiseReport")]
        public IActionResult ViewRefDoctorWiseReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RefDoctorWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewRefDoctorWiseReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefDoctorWiseReport ", "RefDoctorWiseReport ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DepartmentWiseCountSummury")]
        public IActionResult ViewDepartmentWiseCountSummury(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseCountSummury.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _OPbilling.ViewDepartmentWisecountSummury(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseCountSummury", "DepartmentWiseCountSummury", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-CrossConsultationReport")]
        public IActionResult ViewCrossConsultationReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_CrossConsultationReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewCrossConsultationReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CrossConsultationReport", "CrossConsultationReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DoctorWiseVisitCountSummary")]
        public IActionResult ViewDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitSummury.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseVisitCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseVisitCountSummary", "DoctorWiseVisitCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-AppoinmentListWithServiseAvailed")]
        public IActionResult ViewAppoinmentListWithServiseAvailed(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppointmentListWithServiseAvailed.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewAppoinmentListWithServiseAvailed(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AppoinmentListWithServiseAvailed", "AppoinmentListWithServiseAvailed", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DoctorWiseNewAndOldPatientReport")]
        public IActionResult ViewDoctorWiseNewAndOldPatientReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseNewAndOldPatientReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseNewAndOldPatientReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseNewAndOldPatientReport", "DoctorWiseNewAndOldPatientReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentWiseOpdCollectionSummary")]
        public IActionResult ViewDepartmentWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOpdCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOpdCollectionSummary", "DepartmentWiseOpdCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DayWiseOpdCountDetails")]
        public IActionResult ViewDayWiseOpdCountDetails(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDayWiseOpdCountDetails(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DayWiseOpdCountDetails", "ViewDayWiseOpdCountDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DayWiseOpdCountSummry")]
        public IActionResult ViewDayWiseOpdCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDayWiseOpdCountSummry(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DayWiseOpdCountSummary", "ViewDayWiseOpdCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentWiseOpdCountSummary")]
        public IActionResult ViewDepartmentWiseOpdCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOpdCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOpdCountSummary", "DepartmentWiseOpdCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
       
    }
   

}


