using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.PharmacyReports;
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
    public class PharmacyReportController : Controller
    {
        public readonly I_PharmacyReports _IPPharmacy;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public PharmacyReportController(
           I_PharmacyReports ippharmacy, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _IPPharmacy = ippharmacy;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;

        }

        [HttpGet("view-PharmacyDailyCollectionReport")]
        public IActionResult ViewPharmacyDailycollectionReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReports_PharmacyDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewPharmacyDailycollectionReport(FromDate, ToDate, StoreId, AddedById,htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmacyDailyCollectionReport", "PharmacyDailyCollectionReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

           
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SCHEDULEH1Report")]
        public IActionResult ViewSCHEDULEH1Report(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_SCHEDULEH1Report.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewSCHEDULEH1Report(FromDate, ToDate,  DrugTypeId, StoreId,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SCHEDULEH1Report", "SCHEDULEH1Report", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-SCHEDULEH1SalesSummaryReport")]
        public IActionResult ViewSCHEDULEH1SalesSummaryReport(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_SCHEDULEH1SalesSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewSCHEDULEH1SalesSummaryReport(FromDate, ToDate,  DrugTypeId, StoreId,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SCHEDULEH1SalesSummaryReport", "SCHEDULEH1SalesSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesH1DrugCountReport")]
        public IActionResult ViewSalesH1DrugCountReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_SalesH1DrugCountReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewSalesH1DrugCountReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesH1DrugCountReport", "SalesH1DrugCountReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ItemWiseDailySalesReport")]
        public IActionResult ViewItemWiseDailySalesReport(DateTime FromDate, DateTime ToDate, int ItemId, int RegNo, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_ItemWiseDailySalesReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewItemWiseDailySalesReport(FromDate, ToDate, ItemId,RegNo, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemWiseDailySalesReport", "ItemWiseDailySalesReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-WardWiseHighRiskDrugList")]
        public IActionResult ViewWardWiseHighRiskDrugList(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_WardWiseHighRiskDrugList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewWardWiseHighRiskDrugList(FromDate, ToDate, StoreId,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "WardWiseHighRiskDrugList", "WardWiseHighRiskDrugList", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseReOrderList")]
        public IActionResult ViewPurchaseReOrderList(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_PurchaseReOrderList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewPurchaseReOrderList(StoreId, FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseReOrderList", "PurchaseReOrderList", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PharmacyBillSummaryReport")]
        public IActionResult ViewPharmacyBillSummaryReport(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_PharmacyBillSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewPharmacyBillSummaryReport(StoreId, FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmacyBillSummaryReport", "PharmacyBillSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-DoctorWiseProfitReport")]
        public IActionResult ViewDoctorWiseProfitReport( DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_DoctorWiseProfitReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewDoctorWiseProfitReport( FromDate, ToDate, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseProfitReport", "DoctorWiseProfitReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-DoctorWiseSalesReport")]
        public IActionResult ViewDoctorWiseSalesReport(DateTime FromDate, DateTime ToDate, int StoreId, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_DoctorWiseSalesReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewDoctorWiseSalesReport(FromDate, ToDate, StoreId, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseSalesReport", "DoctorWiseSalesReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-PharmacySalesDoctorWiseProfitDetailsReportOPIP")]
        public IActionResult ViewPharmacySalesDoctorWiseProfitDetailsReportOPIP(DateTime FromDate, DateTime ToDate, int StoreId, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_PharmacySalesDoctorWiseProfitDetailsReportOPIP.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewPharmacySalesDoctorWiseProfitDetailsReportOPIP(FromDate, ToDate, StoreId, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmacySalesDoctorWiseProfitDetailsReportOPIP", "PharmacySalesDoctorWiseProfitDetailsReportOPIP", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-PharmacySalesDoctorWiseProfitReportSummaryOPIP")]
        public IActionResult ViewPharmacySalesDoctorWiseProfitReportSummaryOPIP(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacyReport_PharmacySalesDoctorWiseProfitReportSummaryOPIP.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPPharmacy.ViewPharmacySalesDoctorWiseProfitReportSummaryOPIP(FromDate, ToDate, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmacySalesDoctorWiseProfitReportSummaryOPIP", "PharmacySalesDoctorWiseProfitReportSummaryOPIP", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

    }
}

