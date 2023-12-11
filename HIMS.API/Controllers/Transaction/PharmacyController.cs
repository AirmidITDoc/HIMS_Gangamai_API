using HIMS.Data.Pharmacy;
using HIMS.Model.Pharmacy;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Wkhtmltopdf.NetCore;
using HIMS.API.Utility;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Hosting.IWebHostEnvironment

namespace HIMS.API.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacyController : Controller
    {

        public readonly I_Sales _Sales;
        public readonly I_SalesReturn _SalesReturn;
        public readonly I_PurchaseOrder _PurchaseOrder;
        public readonly I_GRN _GRN;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly I_Workorder _Workorder;

        public PharmacyController(I_Sales sales, I_PurchaseOrder purchaseOrder, I_SalesReturn salesReturn, I_GRN gRN, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility, I_Workorder workorder)
        {
            this._Sales = sales;
            _PurchaseOrder = purchaseOrder;
            _SalesReturn = salesReturn;
            _GRN = gRN;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _Workorder = workorder;
        }

        [HttpPost("SalesSaveWithPayment")]
        public IActionResult SalesSaveWithPayment(SalesParams salesParams)
        {
            var SalesSave = _Sales.InsertSales(salesParams);
            return Ok(SalesSave.ToString());

        }
        [HttpPost("SalesSaveWithCredit")]
        public IActionResult SalesSaveWithCredit(SalesCreditParams salesCreditParams)
        {
            var SalesSave = _Sales.InsertSalesWithCredit(salesCreditParams);
            return Ok(SalesSave.ToString());

        }
        [HttpPost("PaymentSettlement")]
        public IActionResult PaymentSettlement(SalesParams salesParams)
        {
            var PaymentSettlement = _Sales.PaymentSettlement(salesParams);
            return Ok(PaymentSettlement);

        }

        [HttpPost("InsertSalesReturnCredit")]
        public IActionResult InsertSalesReturnCredit(SalesReturnCreditParams salesReturnParams)
        {
            var SalesSave = _SalesReturn.InsertSalesReturnCredit(salesReturnParams);
            return Ok(SalesSave.ToString());

        }
        [HttpPost("InsertSalesReturnPaid")]
        public IActionResult InsertSalesReturnPaid(SalesReturnCreditParams salesReturnParams)
        {
            var SalesSave = _SalesReturn.InsertSalesReturnPaid(salesReturnParams);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("InsertPurchaseOrder")]
        public IActionResult InsertPurchaseOrder(PurchaseParams purchaseParams)
        {
            var SalesSave = _PurchaseOrder.InsertPurchaseOrder(purchaseParams);
            return Ok(SalesSave.ToString());

        }
        [HttpPost("UpdatePurchaseOrder")]
        public IActionResult UpdatePurchaseOrder(PurchaseParams purchaseParams)
        {
            var SalesSave = _PurchaseOrder.UpdatePurchaseOrder(purchaseParams);
            return Ok(true);

        }
        [HttpPost("VerifyPurchaseOrder")]
        public IActionResult VerifyPurchaseOrder(PurchaseParams purchaseParams)
        {
            var SalesSave = _PurchaseOrder.VerifyPurchaseOrder(purchaseParams);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("InsertGRNDirect")]
        public IActionResult InsertGRNDirect(GRNParams grnParams)
        {
            var SalesSave = _GRN.InsertGRNDirect(grnParams);
            return Ok(SalesSave.ToString());

        }
        [HttpPost("updateGRN")]
        public IActionResult updateGRN(GRNParams grnParams)
        {
            var SalesSave = _GRN.UpdateGRN(grnParams);
            return Ok(true);

        }
        [HttpPost("InsertGRNPurchase")]
        public IActionResult InsertGRNPurchase(GRNParams grnParams)
        {
            var SalesSave = _GRN.InsertGRNPurchase(grnParams);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("VerifyGRN")]
        public IActionResult VerifyGRN(GRNParams grnParams)
        {
            var SalesSave = _GRN.VerifyGRN(grnParams);
            return Ok(SalesSave.ToString());

        }


        [HttpPost("InsertWorkorder")]
        public IActionResult InsertWorkorder(Workorder Workorder)
        {
            var SalesSave = _Workorder.InsertWorkOrder(Workorder);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("updateWorkorder")]
        public IActionResult updateWorkorder(Workorder Workorder)
        {
            var SalesSave = _Workorder.UpdateWorkOrder(Workorder);
            return Ok(true);

        }


        [HttpGet("view-pharmacy-sale-bill")]
        public IActionResult ViewPharmaBill(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
            var html = _Sales.ViewBill(SalesID, OP_IP_Type, htmlFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            if (System.IO.File.Exists(tuple.Item2))
                System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-pharmacy-daily-collection")]
        public IActionResult ViewPharmaDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailyCollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
           // var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailyCollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-pharmacy-daily-collection_Summary")]
        public IActionResult ViewPharmaDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewDailyCollectionSummary(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-Sales_Report")]
        public IActionResult viewSalesReport(DateTime FromDate, DateTime ToDate,string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, AddedBy, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesSummary_Report")]
        public IActionResult viewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId,htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-Sales_Report_PatientWise")]
        public IActionResult viewSalesReportPatientWise(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber,int AddedBy, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReportPatientwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReportPatientWise(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId,htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReportPatientwise", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesReturn_Patientwise_Report")]
        public IActionResult viewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnPatientwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReturnPatientwiseReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnPatientwise", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesReturnSummary_Report")]
        public IActionResult viewSalesReturnsummaryReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnFinalSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReturnSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnFinalSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesReturn_Report")]
        public IActionResult viewSalesReturnReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReturnReceipt(SalesID, OP_IP_Type, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesCredit_Report")]
        public IActionResult viewSalesCreditReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesCredit.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesCreditReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, CreditReasonId, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesCredit", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesTax_Report")]
        public IActionResult viewSalesTaxReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesTaxReceipt(SalesID, OP_IP_Type, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesTaxReceipt", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-SalesTaxReturn_Report")]
        public IActionResult viewSalesReturnTaxReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesTaxReceipt(SalesID, OP_IP_Type, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PharCollectionSummaryDayanduserwise_Report")]
        public IActionResult viewPharCollsummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummaryDayAndUserWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPharmsDailyCollectionsummaryDayandUserwise(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummaryDayAndUserWise", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-OPBilling_Patientwise_Report")]
        public IActionResult viewOpBillingpatientwiseReporte(string PBillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPBillingSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewOPBillingpatientwiseReport(PBillNo, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPBillingSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

    }
}
