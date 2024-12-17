using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.GSTReports;
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
        [HttpGet("view-SalesProfitDetailDoctorWiseReport")]
        public IActionResult ViewSalesProfitDetailDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitDetailDoctorWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesProfitDetailDoctorWiseReport(FromDate, ToDate, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ViewSalesProfitDetailDoctorWiseReport", "ViewSalesProfitDetailDoctorWiseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-SalesProfitSummaryDoctorWiseReport")]
        public IActionResult ViewSalesProfitSummaryDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitSummaryDoctorWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesProfitSummaryDoctorWiseReport(FromDate, ToDate, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ViewSalesProfitSummaryDoctorWiseReport", "ViewSalesProfitSummaryDoctorWiseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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



        [HttpGet("view-PurchaseGSTReportSupplierWiseGST")]
        public IActionResult ViewPurchaseGSTReportSupplierWiseGST(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseGSTReportSupplierWiseGST.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseGSTReportSupplierWiseGST(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseGSTReportSupplierWiseGST", "PurchaseGSTReportSupplierWiseGST", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseGSTReportSupplierWiseWithoutGST")]
        public IActionResult ViewPurchaseGSTReportSupplierWiseWithoutGST(DateTime FromDate, DateTime ToDate, int StoreID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseGSTReportSupplierWiseWITHOUTGST.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseGSTReportSupplierWiseWithoutGST(FromDate, ToDate,  StoreID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseGSTReportSupplierWiseWithoutGST", "PurchaseGSTReportSupplierWiseWithoutGST", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ViewPurchaseGSTReportDateWise")]
        public IActionResult ViewPurchaseGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseGSTReportDateWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseGSTReportDateWise(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseGSTReportDateWise", "PurchaseGSTReportDateWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-ViewPurchaseGSTReportSummary")]
        public IActionResult ViewPurchaseGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseGSTReportSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseGSTReportSummary(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseGSTReportSummary", "PurchaseGSTReportSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseRetumGSTReportDateWise")]
        public IActionResult ViewPurchaseRetumGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseReturnGSTReportDateWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseRetumGSTReportDateWise(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseRetumGSTReportDateWise", "PurchaseRetumGSTReportDateWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseReturnGSTReportSummary")]
        public IActionResult ViewPurchaseReturnGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_PurchaseReturnGSTReportSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewPurchaseReturnGSTReportSummary(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseReturnGSTReportSummary", "PurchaseReturnGSTReportSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        //[HttpGet("view-PurchaseGSTSummary")]
        //public IActionResult ViewPurchaseGSTSummary(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitItemWiseSummaryReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _IPGST.ViewPurchaseGSTSummary(FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseGSTSummary", "PurchaseGSTSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}

        [HttpGet("view-SalesGSTReport")]
        public IActionResult ViewSalesGSTReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesGSTReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesGSTReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesGSTReport", "SalesGSTReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesGSTDateWiseReport")]
        public IActionResult ViewSalesGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesGSTDateWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesGSTDateWiseReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesGSTDateWiseReport", "SalesGSTDateWiseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesReturnGSTReport")]
        public IActionResult ViewSalesReturnGSTReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesReturnGSTReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesReturnGSTReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnGSTReport", "SalesReturnGSTReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesReturnGSTDateWiseReport")]
        public IActionResult ViewSalesReturnGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesReturnGSTDateWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewSalesReturnGSTDateWiseReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnGSTDateWiseReport", "SalesReturnGSTDateWiseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        //[HttpGet("view-SalesGSTSummaryConsolidated")]
        //public IActionResult ViewSalesGSTSummaryConsolidated(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitItemWiseSummaryReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _IPGST.ViewSalesGSTSummaryConsolidated(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesGSTSummaryConsolidated", "SalesGSTSummaryConsolidated", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}

        [HttpGet("view-HSNCodeWiseReport")]
        public IActionResult ViewHSNCodeWiseReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_HSNCodeWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewHSNCodeWiseReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "HSNCodeWiseReport", "HSNCodeWiseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        //[HttpGet("view-GSTB2CSReport")]
        //public IActionResult ViewGSTB2CSReport(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitItemWiseSummaryReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _IPGST.ViewGSTB2CSReport(FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GSTB2CSReport", "GSTB2CSReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}
        //[HttpGet("view-GSTB2GSReportConsolidated")]
        //public IActionResult ViewGSTB2GSReportConsolidated(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_SalesProfitItemWiseSummaryReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _IPGST.ViewGSTB2GSReportConsolidated(FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GSTB2GSReportConsolidated", "GSTB2GSReportConsolidated", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}
        [HttpGet("view-GSTRZAPurchaseReport")]
        public IActionResult ViewGSTRZAPurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_GSTRZAPurchaseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewGSTRZAPurchaseReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GSTRZAPurchaseReport", "GSTRZAPurchaseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ViewGSTR2ASupplierWisePurchaseReport")]
        public IActionResult ViewGSTR2ASupplierWisePurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GSTReport_GSTRZASupplierWisePurchaseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPGST.ViewGSTR2ASupplierWisePurchaseReport(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GSTR2ASupplierWisePurchaseReport", "GSTR2ASupplierWisePurchaseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }
}
