//using HIMS.API.Utility;
//using HIMS.Data.Opd;
//using HIMS.Data.MISReports;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HIMS.API.Controllers.Transaction
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MISReportController : Controller
//    {
//        public readonly I_MISReport _IPMIS;
//        public readonly IPdfUtility _pdfUtility;
//        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
//        public MISReportController(
//           I_MISReport ipmis, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
//        {
//            _IPMIS = ipmis;
//            _hostingEnvironment = hostingEnvironment;
//            _pdfUtility = pdfUtility;

//        }

//        [HttpGet("view-CityWiseIPPatientCountReport")]
//        public IActionResult ViewCityWiseIPPatientCountReport(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_CityWiseIPPatientCountReport.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewCityWiseIPPatientCountReport(FromDate, ToDate, AddedById, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CityWiseIPPatientCountReport", "CityWiseIPPatientCountReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
//        }
//        [HttpGet("view-DepartmentWiseOPandIPRevenueReport")]
//        public IActionResult ViewDepartmentWiseOPandIPRevenueReport(DateTime FromDate, DateTime ToDate,int DoctorID)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_DepartmentWiseOPandIPRevenueReport.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewDepartmentWiseOPandIPRevenueReport(FromDate, ToDate, DoctorID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOPandIPRevenueReport", "DepartmentWiseOPandIPRevenueReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

//        }

//        [HttpGet("view-DepartmentandDoctorWiseOPBillingReport")]
//        public IActionResult ViewDepartmentandDoctorWiseOPBillingReport(DateTime FromDate, DateTime ToDate, int DoctorID)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_DepartmentAndDoctorWiseOPBillingReport.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewDepartmentandDoctorWiseOPBillingReport(FromDate, ToDate, DoctorID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentandDoctorWiseOPBillingReport", "DepartmentandDoctorWiseOPBillingReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

//        }

//        [HttpGet("view-DepartmentandDoctorWiseIPBillingReport")]
//        public IActionResult ViewDepartmentandDoctorWiseIPBillingReport(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int DoctorID)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_DepartmentAndDoctorWiseIPBillingReport.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewDepartmentandDoctorWiseIPBillingReport(FromDate, ToDate, OP_IP_Type, DoctorID,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentandDoctorWiseIPBillingReport", "DepartmentandDoctorWiseIPBillingReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

//        }

//        [HttpGet("view-DepartmentWiseOPRevenueReport")]
//        public IActionResult ViewDepartmentWiseOPRevenueReport(DateTime FromDate, DateTime ToDate)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_DepartmentWiseOPDRevenue.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewDepartmentWiseOPRevenueReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOPRevenueReport", "DepartmentWiseOPRevenueReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

//        }

//        [HttpGet("view-DepartmentWiseIPRevenueReport")]
//        public IActionResult ViewDepartmentWiseIPRevenueReport(DateTime FromDate, DateTime ToDate)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_DepartmentWiseIPDRevenue.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPMIS.ViewDepartmentWiseIPRevenueReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseIPRevenueReport", "DepartmentWiseIPRevenueReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

//        }

//    }
//}

