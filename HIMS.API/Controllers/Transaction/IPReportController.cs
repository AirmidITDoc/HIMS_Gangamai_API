using HIMS.API.Utility;
using HIMS.Data.IPD;
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
    public class IPReportController : Controller
    {
        public readonly I_IPReports _IPReports;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public IPReportController(
           I_IPReports iPReports, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _IPReports = iPReports;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

     


        [HttpGet("view-CommanDailyCollectionReport")]
        public IActionResult ViewCommanDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CommanDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCommanDailyCollectionReceipt(FromDate, ToDate, AddedById, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CommanDailycollection", "CommanDailycollection", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPAdmitPatientwardwisechargesReport")]
        public IActionResult ViewIPAdmitpatientwardwisechargesReport(int DoctorId, int WardId, int CompanyId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReports_AdmittedPatientWardwisecharges.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDAdmissionWardWiseCharges(DoctorId, WardId, CompanyId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReports_AdmittedPatientWardwisecharges", "IPReports_AdmittedPatientWardwisecharges"+ WardId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

         
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPCurrentrefadmittedReport")]
        public IActionResult ViewIPCurrentrefadmittedReport(int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReports_CurrentRefDoctorAdmitted.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCurrentrefadmittedlist(DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReports_CurrentRefDoctorAdmitted", "IPReports_CurrentRefDoctorAdmitted" + DoctorId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPDischargeTypeReport")]
        public IActionResult ViewIPDischargeTypeReport(int DoctorId, DateTime FromDate, DateTime ToDate, int DischargeTypeId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReportDischargeType.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargetypewise(DoctorId, FromDate, ToDate, DischargeTypeId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReportDischargeType", "IPReportDischargeType" + DoctorId, Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpGet("view-IPDailyCollectionReport")]
        public IActionResult ViewIPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDailyCollectionReceipt(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDailycollection", "IPDDailycollection", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-OPIPBILLSummaryReport")]
        public IActionResult ViewOpIPBillsummaryReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPIPBillsummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewOPIPBillSummaryReceipt(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPIPBillsummary", "OPIPBillsummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-IPCreditReport")]
        public IActionResult ViewIPCreditReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCreditReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPCreditReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPCreditReport", "IPCreditReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        //[HttpGet("view-IPDAdmissionListCompanyWiseSummary")]
        //public IActionResult ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, BigInteger DoctorId, BigInteger WardId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDAdmissionListCompanyWiseSummary.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _IPReports.ViewIPDAdmissionListCompanyWiseSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDAdmissionListCompanyWiseSummary", "IPDAdmissionListCompanyWiseSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}
        [HttpGet("view-IPDDischargeReportWithMarkStatus")]
        public IActionResult ViewIPDDischargeReportWithMarkStatus(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDischargeReportWithMarkStatus.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeReportWithMarkStatus(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeReportWithMarkStatus", "IPDDischargeReportWithMarkStatus", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-IPDDischargeReportWithBillSummary")]
        public IActionResult ViewIPDDischargeReportWithBillSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDischargeReportWithBillSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeReportWithBillSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeReportWithBillSummary", "IPDDischargeReportWithBillSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentWiseCountSummary")]
        public IActionResult ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDepartmentWiseCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDepartmentWiseCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseCountSummary", "DepartmentWiseCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DoctorWiseCountSummary")]
        public IActionResult ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDoctorWiseCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDoctorWiseCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseCountSummary", "DoctorWiseCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-OPToIPConvertedListWithServiceAvailed")]
        public IActionResult ViewOPToIPConvertedListWithServiceAvailed(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDOPToIPConvertedListWithServiceAvailed.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewOPToIPConvertedListWithServiceAvailed(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPToIPConvertedListWithServiceAvailed", "OPToIPConvertedListWithServiceAvailed", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
    }
}