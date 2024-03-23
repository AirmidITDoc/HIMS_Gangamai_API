using HIMS.API.Utility;
using HIMS.Data.IPD;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Transaction
{


    [ApiController]
    [Route("api/[controller]")]
    public class IPReportController : Controller
    {
        public readonly I_IPBilling _IPBilling;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public IPReportController(
           I_IPBilling iPBilling, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _IPBilling = iPBilling;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        [HttpGet("view-CommanDailyCollectionReport")]
        public IActionResult ViewCommanDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById,int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CommanDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IPBilling.ViewCommanDailyCollectionReceipt(FromDate, ToDate, AddedById, DoctorId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CommanDailycollection", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

         
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDailyCollectionReport")]
        public IActionResult ViewIPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IPBilling.ViewIPDailyCollectionReceipt(FromDate, ToDate, AddedById, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDailycollection", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

    }
}