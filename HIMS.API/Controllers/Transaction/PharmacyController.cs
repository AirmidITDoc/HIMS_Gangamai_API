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
        private readonly I_Stokadjustment _Stokadjustment;
        private readonly I_Mrpadjustment _Mrpadjustment;
        private readonly I_Openingbalance _Openingbalance;
        private readonly I_MaterialAcceptance _MaterialAcceptance;
        private readonly I_PharmPaymentMode _PharmPaymentMode;
        private readonly I_GRNReturn _GRNReturn;

        public PharmacyController(I_Sales sales, I_PurchaseOrder purchaseOrder, I_SalesReturn salesReturn, I_GRN gRN,
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility, I_Workorder workorder, I_Stokadjustment stokadjustment,
            I_Openingbalance openingbalance,
            I_Mrpadjustment mrpadjustment,
            I_MaterialAcceptance materialAcceptance, I_PharmPaymentMode pharmPaymentMode, I_GRNReturn gRNReturn)
        {
            this._Sales = sales;
            _PurchaseOrder = purchaseOrder;
            _SalesReturn = salesReturn;
            _GRN = gRN;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _Workorder = workorder;
            _Stokadjustment = stokadjustment;
            _Mrpadjustment = mrpadjustment;
            _Openingbalance = openingbalance;
            _MaterialAcceptance = materialAcceptance;
            _PharmPaymentMode = pharmPaymentMode;
            _GRNReturn = gRNReturn;
        }

        [HttpPost("SalesSaveWithPaymentwithStockCheck")]
        public IActionResult SalesSaveWithPaymentwithStockCheck(SalesParams salesParams)
        {
            bool IsInStock = true;
            foreach (UpdateCurStkSales objItem in salesParams.UpdateCurStkSales)
            {
                int CurrentStock = _Sales.GetCurrentStock(objItem.ItemId, objItem.StoreID, objItem.StkID);
                if (CurrentStock < objItem.IssueQty)
                    IsInStock = false;
            }
            if (IsInStock)
            {
                var SalesSave = _Sales.InsertSales(salesParams);
                return Ok(SalesSave.ToString());
            }
            else
            {
                return Ok("-1");
            }
        }

        [HttpPost("SalesSaveWithPayment")]
        public IActionResult SalesSaveWithPayment(SalesParams salesParams)
        {
            var SalesSave = _Sales.InsertSales(salesParams);
            return Ok(SalesSave.ToString());
        }

        [HttpPost("SalesSaveDraftBill")]
        public IActionResult SalesSaveDraftBill(SalesParams salesParams)
        {
            var SalesSave = _Sales.InsertSalesDraftBill(salesParams);
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

        [HttpPost("UpdateStockToMainStock")]
        public IActionResult UpdateStockToMainStock(MaterialAcceptParams materialAcceptParams)
        {
            var SalesSave = _MaterialAcceptance.UpdateStockToMainStock(materialAcceptParams);
            return Ok(true);

        }
        [HttpPost("UpdateMaterialAcceptance")]
        public IActionResult UpdateMaterialAcceptance(MaterialAcceptParams materialAcceptParams)
        {
            var SalesSave = _MaterialAcceptance.UpdateMaterialAcceptance(materialAcceptParams);
            return Ok(true);

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

        [HttpGet("view-Purchaseorder")]
        public IActionResult ViewAdmittedPatientList(int PurchaseID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseorderNew.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _PurchaseOrder.ViewPurchaseorder(PurchaseID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseorderNew", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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

        [HttpPost("UpdateGRNPurchase")]
        public IActionResult UpdateGRNPurchase(GRNParams grnParams)
        {
            var SalesSave = _GRN.UpdateGRNPurchase(grnParams);
            return Ok(true);

        }

        [HttpGet("view-GRNReport")]
        public IActionResult ViewGRNReport(int GRNID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _GRN.ViewGRNReport(GRNID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpPost("VerifyGRN")]
        public IActionResult VerifyGRN(GRNParams grnParams)
        {
            var SalesSave = _GRN.VerifyGRN(grnParams);
            return Ok(SalesSave);

        }

        [HttpPost("InsertGRNReturn")]
        public IActionResult InsertGRNReturn(GRNReturnParam GRNReturnParam)
        {
            var Id = _GRNReturn.InsertGRNReturn(GRNReturnParam);
            return Ok(Id);

        }
        [HttpPost("InsertWorkorder")]
        public IActionResult InsertWorkorder(Workorder Workorder)
        {
            var Id = _Workorder.InsertWorkOrder(Workorder);
            return Ok(Id);

        }

        [HttpPost("updateWorkorder")]
        public IActionResult updateWorkorder(Workorder Workorder)
        {
            var SalesSave = _Workorder.UpdateWorkOrder(Workorder);
            return Ok(true);

        }

        [HttpPost("InsertStockadjustment")]
        public IActionResult InsertStockadjustment(Stockadjustmentparam Stockadjustmentparam)
        {
            var Id = _Stokadjustment.Insert(Stockadjustmentparam);
            return Ok(Id);

        }

        [HttpPost("UpdateStockadjustment")]
        public IActionResult updateStockadjustment(Stockadjustmentparam Stockadjustmentparam)
        {
            var SalesSave = _Stokadjustment.Update(Stockadjustmentparam);
            return Ok(true);

        }

        [HttpPost("InsertMRPadjustment")]
        public IActionResult InsertMRPadjustment(MRPAdjustment MRPAdjustment)
        {
            var Id = _Mrpadjustment.Insert(MRPAdjustment);
            return Ok(Id);

        }

        [HttpPost("UpdateMRPadjustment")]
        public IActionResult updateMRPadjustment(MRPAdjustment MRPAdjustment)
        {
            var SalesSave = _Mrpadjustment.update(MRPAdjustment);
            return Ok(true);

        }

        [HttpPost("InsertOpeningBalance")]
        public IActionResult InsertOpeningbalance(Openingbalance Openingbalance)
        {
            var Id = _Openingbalance.Insert(Openingbalance);
            return Ok(Id);

        }

        [HttpPost("UpdateOpeningbalance")]
        public IActionResult updateMRPadjustment(Openingbalance Openingbalance)
        {
            var SalesSave = _Openingbalance.Update(Openingbalance);
            return Ok(true);

        }


        [HttpPost("UpdatePharmPaymentMode")]
        public IActionResult updatePharmPaymentMode(PharmPaymentMode PharmPaymentMode)
        {
            var SalesSave = _PharmPaymentMode.UpdatePaymentMode(PharmPaymentMode);
            return Ok(true);

        }


        //[HttpGet("view-pharmacy-daily-collection")]
        //public IActionResult ViewPharmaDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailyCollection.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
        //    // var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailyCollection", "PharmaDailyCollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-pharmacy-daily-collection_Summary")]
        //public IActionResult ViewPharmaDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummary.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewDailyCollectionSummary(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-Sales_Report")]
        //public IActionResult viewSalesReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, AddedBy, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //   // var tuple1 = _pdfUtility.CreateExel();

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}

        //[HttpGet("view-SalesSummary_Report")]
        //public IActionResult viewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesSummaryReport.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesSummaryReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        [HttpGet("view-OPEXTDailycount_Report")]
        public IActionResult viewOPExtdailycountReport(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharOPExtdailycount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPharOPExtcountdailyReport(FromDate, ToDate, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharOPExtdailycount", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-CompanyCredit_Report")]
        public IActionResult viewCompanycreditReport(int StoreId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharCompanycreditlist.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPharCompanycreditlistReport(StoreId, FromDate, ToDate, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharCompanycreditlist", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PatientwisecompyCredit_Report")]
        public IActionResult viewCompanywisepatientcreditReport(int StoreID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharIPComwisepatientcredit.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPharcomwisepatientcreditReceipt(StoreID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharIPComwisepatientcredit", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        //[HttpGet("view-Sales_Report_PatientWiseNew")]
        //public IActionResult viewSalesReportPatientWiseNew(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReportPatientwise.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReportPatientWise(FromDate, ToDate, SalesFromNumber, SalesToNumber, AddedBy, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReportPatientwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-SalesReturn_Patientwise_Report")]
        //public IActionResult viewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnPatientwise.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReturnPatientwiseReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnPatientwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        //    //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnPatientwiseNew.html");
        //    //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    //var html = _Sales.ViewSalesReturnPatientwiseReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnPatientwiseNew", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);
        //    //return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-SalesReturn_PatientwiseNew_Report")]
        //public IActionResult viewSalesReturnPatientwiseNewReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnPatientwiseNew.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReturnPatientwiseReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnPatientwiseNew", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}




        //[HttpGet("view-SalesReturnSummary_Report")]
        //public IActionResult viewSalesReturnsummaryReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnFinalSummary.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReturnSummaryReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnFinalSummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-SalesReturn_Report")]
        //public IActionResult viewSalesReturnReport(int SalesID, int OP_IP_Type)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesReturnReceipt(SalesID, OP_IP_Type, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}


        //[HttpGet("view-SalesCredit_Report")]
        //public IActionResult viewSalesCreditReporte(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesCredit.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewSalesCreditReport(FromDate, ToDate, SalesFromNumber, SalesToNumber, CreditReasonId, StoreId, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesCredit", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}

        [HttpGet("view-pharmacy-sale-bill")]
        public IActionResult ViewPharmaBill(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
            var html = _Sales.ViewBill(SalesID, OP_IP_Type, htmlFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + SalesID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            if (System.IO.File.Exists(tuple.Item2))
                System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SalesTax_Report")]
        public IActionResult viewSalesTaxReport(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesTaxReceipt(SalesID, OP_IP_Type, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesTaxReceipt", "PharmaBill_" + SalesID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

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
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        //[HttpGet("view-PharCollectionSummaryDayanduserwise_Report")]
        //public IActionResult viewPharCollsummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaDailySummaryDayAndUserWise.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
        //    var html = _Sales.ViewPharmsDailyCollectionsummaryDayandUserwise(FromDate, ToDate, StoreId, AddedById, htmlFilePath, htmlHeaderFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaDailySummaryDayAndUserWise","", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}

        [HttpGet("view-PharSalesCashBookReport")]
        public IActionResult ViewPharSalesCashBookReport(DateTime FromDate, DateTime ToDate, string PaymentMode, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesCashBook.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPharSalesCashBookReport(FromDate, ToDate, PaymentMode, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesCashBook", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-POrchaseorder_Report")]
        public IActionResult viewPurchaseorderReport(int PurchaseID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseOrder.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewPurchaseorderReceipt(PurchaseID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseOrder", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

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
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPBillingSummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }





        [HttpGet("view-MaterialRecivedFrDept_Report")]
        public IActionResult viewMaterialRecfrdeptReporte(int IssueId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PhrmaMaterialReceivedfrDept.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _MaterialAcceptance.ViewMaterialReceivedfrDept(IssueId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PhrmaMaterialReceivedfrDept", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

    }
}
