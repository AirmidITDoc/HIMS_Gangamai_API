using HIMS.API.Utility;
using HIMS.Data.Pharmacy;
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
    public class SalesController : Controller
    {

        public readonly I_SalesReport _Sales;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public SalesController(I_SalesReport salesReport, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _Sales = salesReport;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }
        [HttpGet("view-Sales_Report")]
        public IActionResult viewSalesReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, AddedBy, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

          
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesSummary_Report")]
        public IActionResult viewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesSummaryReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

           
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-Sales_Report_PatientWiseNew")]
        public IActionResult viewSalesReportPatientWiseNew(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReportPatientwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesReportPatientWise(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReportPatientwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpGet("view-SalesReturn_Patientwise_Report")]
        public IActionResult viewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnPatientwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesReturnPatientwiseReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnPatientwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

           
        }


        [HttpGet("view-SalesReturnSummary_Report")]
        public IActionResult viewSalesReturnsummaryReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnFinalSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesReturnSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnFinalSummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

         
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesReturn_Report")]
        public IActionResult viewSalesReturnReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesReturnReceipt(SalesID, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

         
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesCredit_Report")]
        public IActionResult viewSalesCreditReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesCredit.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewSalesCreditReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, CreditReasonId, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesCredit", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

         
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesTax_Report")]
        public IActionResult viewSalesTaxReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesTaxReceipt(SalesID, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesTaxReceipt", "PharmaBill_" + SalesID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

          
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesTaxReturn_Report")]
        public IActionResult viewSalesReturnTaxReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesTaxReceipt(SalesID, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

           
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-pharmacy-daily-collection_Summary")]
        public IActionResult ViewPharmaDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewDailyCollectionSummary(FromDate, ToDate, StoreId, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

          
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        //[HttpGet("view-pharmacy-daily-collection")]
        //public IActionResult ViewPharmaDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailyCollection.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailyCollection", "PharmaDailyCollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        [HttpGet("view-pharmacy-daily-collection")]
        public IActionResult ViewGRNReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailyCollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath, _pdfUtility.GetStoreHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailyCollection", "PharmaDailyCollection", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }






        [HttpGet("view-PharSalesCashBookReport")]
        public IActionResult ViewPharSalesCashBookReport(DateTime FromDate, DateTime ToDate, string PaymentMode, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesCashBook.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharSalesCashBookReport(FromDate, ToDate, PaymentMode, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesCashBook", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-PharCollectionSummaryDayanduserwise_Report")]
        public IActionResult viewPharCollsummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummaryDayAndUserWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharmsDailyCollectionsummaryDayandUserwise(FromDate, ToDate, StoreId, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummaryDayAndUserWise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-OPEXTDailycount_Report")]
        public IActionResult viewOPExtdailycountReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharOPExtdailycount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharOPExtcountdailyReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharOPExtdailycount", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-CompanyCredit_Report")]
        public IActionResult viewCompanycreditReport(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharCompanycreditlist.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharCompanycreditlistReport(StoreId, FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharCompanycreditlist", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PatientwisecompyCredit_Report")]
        public IActionResult viewCompanywisepatientcreditReport(int StoreID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharIPComwisepatientcredit.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharcomwisepatientcreditReceipt(StoreID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharIPComwisepatientcredit", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

           
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PatientStatement")]
        public IActionResult viewPatientstatementReport(int OP_IP_ID, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacySalesStatement.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Sales.ViewPharSalesstatement(OP_IP_ID, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmacySalesStatement", "PharmacySalesStatement"+ OP_IP_ID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }
}
