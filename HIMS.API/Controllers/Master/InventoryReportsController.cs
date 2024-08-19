using HIMS.API.Utility;
using HIMS.Data.InventoryReports;
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
    public class InventoryReportsController : Controller
    {
        public readonly I_InventoryReport _InventoryReport;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private I_InventoryReport inventoryReport;

        public InventoryReportsController(
           I_InventoryReport inventoryReport, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _InventoryReport = inventoryReport;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        
    }
        [HttpGet("view-ItemList")]
        public IActionResult ViewItemList(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ItemList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewItemList(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemList", "ItemList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-SupplierList")]
        public IActionResult ViewSupplierList(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_SupplierList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewSupplierList(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SupplierList", "SupplierList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-IndentReport")]
        public IActionResult ViewIndentReport(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_IndentReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewIndentReport(FromDate, ToDate,  FromStoreId,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IndentReport", "IndentReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-MonthlyPurchaseGRNReport")]
        public IActionResult ViewMonthlyPurchaseGRNReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_MonthlyPurchaseGRNReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewMonthlyPurchaseGRNReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "MonthlyPurchaseGRNReport", "MonthlyPurchaseGRNReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-GRNReport")]
        public IActionResult ViewGRNReport(DateTime FromDate, DateTime ToDate, int StoreId, int SupplierID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_GRNReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewGRNReport(FromDate, ToDate,  StoreId, SupplierID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReport", "GRNReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-GRNReportNΑΒΗ")]
        public IActionResult ViewGRNReportNΑΒΗ(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_GRNReportNBHA.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewGRNReportNΑΒΗ(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReportNΑΒΗ", "GRNReportNΑΒΗ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-GRNReturnReport")]
        public IActionResult ViewGRNReturnReport(DateTime FromDate, DateTime ToDate, int StoreId, int SupplierID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_GRNReturnReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewGRNReturnReport(FromDate, ToDate,  StoreId,  SupplierID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReturnReport", "GRNReturnReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IssueToDepartmentMonthlySummary")]
        public IActionResult ViewIssueToDepartmentMonthlySummary(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_IssueToDepartmentMonthlySummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewIssueToDepartmentMonthlySummary(FromDate, ToDate,  FromStoreId, ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IssueToDepartmentMonthlySummary", "IssueToDepartmentMonthlySummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-GRNWiseProductQtyReport")]
        public IActionResult ViewGRNWiseProductQtyReport(DateTime FromDate, DateTime ToDate, int SupplierId, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_GRNWiseProductQtyReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewGRNWiseProductQtyReport(FromDate, ToDate,  SupplierId, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNWiseProductQtyReport", "GRNWiseProductQtyReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-GRNPurchaseReport")]
        public IActionResult ViewGRNPurchaseReport(DateTime FromDate, DateTime ToDate,int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_GRNPurchaseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewGRNPurchaseReport(FromDate, ToDate, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNPurchaseReport", "GRNPurchaseReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SupplierWiseGRNList")]
        public IActionResult ViewSupplierWiseGRNList(int StoreId, int SupplierID, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_SupplierWiseGRNList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewSupplierWiseGRNList( StoreId,  SupplierID, FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SupplierWiseGRNList", "SupplierWiseGRNList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IssueToDepartment")]
        public IActionResult ViewIssueToDepartment(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, int ItemId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_IssueToDepartment.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewIssueToDepartment(FromDate, ToDate, FromStoreId, ToStoreId,  ItemId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IssueToDepartment", "IssueToDepartment", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IssueToDepartmentItemWise")]
        public IActionResult ViewIssueToDepartmentItemWise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, int ItemId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_IssueToDepartmentItemWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewIssueToDepartmentItemWise(FromDate, ToDate,  FromStoreId, ToStoreId,  ItemId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IssueToDepartmentItemWise", "IssueToDepartmentItemWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ReturnFromDepartment")]
        public IActionResult ViewReturnFromDepartment(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ReturnFromDepartment.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewReturnFromDepartment(FromDate, ToDate,  FromStoreId, ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ReturnFromDepartment", "ReturnFromDepartment", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseOrder")]
        public IActionResult ViewPurchaseOrder(DateTime FromDate, DateTime ToDate, int SupplierID, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_PurchaseOrder.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewPurchaseOrder(FromDate, ToDate,  SupplierID,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseOrder", "PurchaseOrder", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-MaterialConsumptionMonthlySummary")]
        public IActionResult ViewMaterialConsumptionMonthlySummary(DateTime FromDate, DateTime ToDate, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_MaterialConsumptionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewMaterialConsumptionMonthlySummary(FromDate, ToDate,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "MaterialConsumptionMonthlySummary", "MaterialConsumptionMonthlySummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-MaterialConsumption")]
        public IActionResult ViewMaterialConsumption(DateTime FromDate, DateTime ToDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_MaterialConsumption.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewMaterialConsumption(FromDate, ToDate,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "MaterialConsumption", "MaterialConsumption", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ItemExpiryReport")]
        public IActionResult ViewItemExpiryReport(int ExpMonth, int ExpYear, int StoreID, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ItemExpiryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewItemExpiryReport(ExpMonth, ExpYear,  StoreID, FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemExpiryReport", "ItemExpiryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-CurrentStockReport")]
        public IActionResult ViewCurrentStockReport(DateTime FromDate, DateTime ToDate, int StoreId, int IsNarcotic, int ish1Drug, int isScheduleH, int IsHighRisk, int IsScheduleX)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_CurrentStockReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewCurrentStockReport(FromDate, ToDate, StoreId, IsNarcotic, ish1Drug, isScheduleH, IsHighRisk, IsScheduleX,htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CurrentStockReport", "CurrentStockReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ClosingCurrentStockReport")]
        public IActionResult ViewClosingCurrentStockReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ClosingCurrentStockReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewClosingCurrentStockReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ClosingCurrentStockReport", "ClosingCurrentStockReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ItemWiseSupplierList")]
        public IActionResult ViewItemWiseSupplierList(int StoreId, int SupplierID, int ItemId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ItemWiseSupplierList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewItemWiseSupplierList(StoreId,  SupplierID, ItemId, FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemWiseSupplierList", "ItemWiseSupplierList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-CurrentStockDateWise")]
        public IActionResult ViewCurrentStockDateWise(DateTime InsertDate, int StoreId, DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_DateWiseCurrentStock.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewCurrentStockDateWise(InsertDate, StoreId, FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CurrentStockDateWise", "CurrentStockDateWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-NonMovingItemList")]
        public IActionResult ViewNonMovingItemList(DateTime FromDate, DateTime ToDate, int NonMovingDay, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_NonMovingItemList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewNonMovingItemList( FromDate, ToDate, NonMovingDay, StoreId,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "NonMovingItemList", "NonMovingItemList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-NonMovingItemWithoutBatchList")]
        public IActionResult ViewNonMovingItemWithoutBatchList(DateTime FromDate, DateTime ToDate, int NonMovingDay, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_NonMovingItemWithoutBatchList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewNonMovingItemWithoutBatchList(FromDate, ToDate, NonMovingDay, StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "NonMovingItemWithoutBatchList", "NonMovingItemWithoutBatchList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PatientWiseMaterialConsumption")]
        public IActionResult ViewPatientWiseMaterialConsumption(DateTime FromDate, DateTime ToDate, int Id, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_PatientWiseMaterialConsumption.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewPatientWiseMaterialConsumption(FromDate, ToDate,  Id,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PatientWiseMaterialConsumption", "PatientWiseMaterialConsumption", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-LastPurchaseRateWiseConsumtion")]
        public IActionResult ViewLastPurchaseRateWiseConsumtion(DateTime FromDate, DateTime ToDate, int ItemId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_LastPurchaseRateWiseConsumtion.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewLastPurchaseRateWiseConsumtion(FromDate, ToDate,  ItemId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "LastPurchaseRateWiseConsumtion", "LastPurchaseRateWiseConsumtion", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ItemCount")]
        public IActionResult ViewItemCount(DateTime FromDate, DateTime ToDate, int ItemId, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_ItemCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewItemCount(FromDate, ToDate, ItemId,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemCount", "ItemCount", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-SupplierWiseDebitCreditNote")]
        public IActionResult ViewSupplierWiseDebitCreditNote(DateTime FromDate, DateTime ToDate, int SupplierId, int StoreId )
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_SupplierWiseDebitCreditNote.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewSupplierWiseDebitCreditNote(FromDate, ToDate,  SupplierId,  StoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SupplierWiseDebitCreditNote", "SupplierWiseDebitCreditNote", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-StockAdjustmentReport")]
        public IActionResult ViewStockAdjustmentReport(DateTime FromDate, DateTime ToDate,int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_StockAdjustmentReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewStockAdjustmentReport(FromDate, ToDate,  ToStoreId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "StockAdjustmentReport", "StockAdjustmentReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-PurchaseWiseGRNSummary")]
        public IActionResult ViewPurchaseWiseGRNSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InventoryReport_PurchaseWiseGRNSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _InventoryReport.ViewPurchaseWiseGRNSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseWiseGRNSummary", "PurchaseWiseGRNSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }
}


