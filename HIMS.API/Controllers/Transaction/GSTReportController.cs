using HIMS.API.Utility;
using HIMS.Data.GSTReports;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Transaction
{
    public class GSTReportController : Controller
    {
        public readonly I_GSTReport _IPGST;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public GSTReportController(
           I_GSTReport ipgst, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _IPGST = ipgst;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;

        }

        [HttpGet("view-SalesProfitSummaryReport")]
        public IActionResult ViewSalesProfitSummaryReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesProfitSummaryReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesProfitSummaryReport", "SalesProfitSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-SalesProfitBillReport")]
        public IActionResult ViewSalesProfitBillReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitBillReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesProfitBillReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesProfitBillReport", "SalesProfitBillReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-SalesProfitItemWiseSummaryReport")]
        public IActionResult ViewSalesProfitItemWiseSummaryReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitItemWiseSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesProfitItemWiseSummaryReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesProfitItemWiseSummaryReport", "SalesProfitItemWiseSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }
}
