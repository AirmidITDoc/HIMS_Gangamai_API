using Microsoft.AspNetCore.Mvc;
using HIMS.Model.Inventory;
using HIMS.Data.Inventory;
using System.IO;
using HIMS.API.Utility;
using System;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTransactionController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly IFileUtility _IFileUtility;
        public readonly I_Indent _indent;
        public readonly I_IssueTrackingInfo _IssueTrackingInfo;
        public readonly I_IssuetoDepartment _IssuetoDepartment;
        public readonly I_InvMaterialConsumption _InvMaterialConsumption;
        public readonly I_ReturnFromDept _ReturnFromDept;
        public InventoryTransactionController(
             I_Indent indent
            ,I_IssueTrackingInfo issueTrackingInfo
            ,Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment
            , IPdfUtility pdfUtility
            , IFileUtility fileUtility
            , I_IssuetoDepartment issuetoDepartment,
             I_InvMaterialConsumption invMaterialConsumption,
             I_ReturnFromDept returnFromDept )
        {
            this._indent = indent;
            this._IssueTrackingInfo = issueTrackingInfo;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _IFileUtility = fileUtility;
            _IssuetoDepartment = issuetoDepartment;
            _InvMaterialConsumption = invMaterialConsumption;
            _ReturnFromDept = returnFromDept;
        }

        [HttpPost("IndentSave")]
        public IActionResult IndentSave(IndentParams indentParams)
        {
            var IndentInsert = _indent.Insert(indentParams);
            return Ok(IndentInsert);
        }

        [HttpPost("IndentUpdate")]
        public IActionResult IndentUpdate(IndentParams indentParams)
        {
            var IndentUpdate = _indent.Update(indentParams);
            return Ok(IndentUpdate);
        }
        
        [HttpPost("IndentVerify")]
        public IActionResult IndentVerify(IndentParams indentParams)
        {
            var IndentUpdate = _indent.IndentVerify(indentParams);
            return Ok(IndentUpdate);
        }

        [HttpPost("IssuetoDepartmentSave")]
        public IActionResult IssuetoDepartmentSave(IssuetoDepartmentParams issuetoDepartmentParams)
        {
            var IndentInsert = _IssuetoDepartment.InsertIssuetoDepartment(issuetoDepartmentParams);
            return Ok(IndentInsert);
        }

        [HttpPost("InsertReturnFromDepartment")]
        public IActionResult ReturnfromDepartmentInsert(ReturnfrdeptParam ReturnfrdeptParam)
        {
            var Id = _ReturnFromDept.InsertReturnFromDepartment(ReturnfrdeptParam);
            return Ok(Id);
        }



        [HttpPost("IssueTrackerSave")]
        public IActionResult IssueTrackerSave(IssueTrackerParams issueTrackerParams)
        {
            var IndentInsert = _IssueTrackingInfo.Insert(issueTrackerParams);
            return Ok(IndentInsert);
        }

        [HttpPost("IssueTrackerUpdate")]
        public IActionResult IssueTrackerUpdate(IssueTrackerParams issueTrackerParams)
        {
            var IndentUpdate = _IssueTrackingInfo.Update(issueTrackerParams);
            return Ok(IndentUpdate);
        }
        [HttpPost("IssueTrackerUpdateStatus")]
        public IActionResult IssueTrackerUpdateStatus(IssueTrackerParams issueTrackerParams)
        {
            var IndentUpdate = _IssueTrackingInfo.UpdateStatus(issueTrackerParams);
            return Ok(IndentUpdate);
        }

        [HttpGet("view-InvItemwiseStock")]
        public IActionResult ViewInvItemwise(DateTime FromDate ,DateTime todate,int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvCurStockItemwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _indent.ViewItemwiseStock(FromDate, todate, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvCurStockItemwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-InvDaywiseStock")]
        public IActionResult ViewInvDaywise(DateTime LedgerDate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvStockDaywise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _indent.ViewDaywisestock(LedgerDate, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvStockDaywise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-InvCurrentStock")]
        public IActionResult ViewCurrentStock(string ItemName, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvCurrentStock.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _indent.ViewCurrentStock(ItemName, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvCurrentStock", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-ItemWisePurchase")]
        public IActionResult ViewItemWisePurchase(DateTime FromDate, DateTime todate, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "ItemWisePurchaseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _indent.ViewItemWisePurchase(FromDate, todate, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemWisePurchaseReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpGet("view-IssuetoDeptIssuewise")]
        public IActionResult ViewIssuetoDeptIssuewise(int IssueId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvIssuetoDept.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewIssuetoDeptIssuewise(IssueId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvIssuetoDept", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-IssuetoDeptSummary")]
        public IActionResult ViewIssuetoDeptSummary(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvIssuetodeptsummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewIssuetodeptsummary(FromDate, ToDate, FromStoreId, ToStoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvIssuetodeptsummary", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-NonMovingItem")]
        public IActionResult ViewNonMovingItem(int NonMovingDay, int StoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NonMovingItemList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewNonMovingItem(NonMovingDay, StoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "NonMovingItemList", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpGet("view-ExpiryItemList")]
        public IActionResult ViewExpiryitemlist(int ExpMonth, int ExpYear,int StoreID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "ExpItemList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewExpItemlist(ExpMonth, ExpYear, StoreID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ExpItemList", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-ReturnfromDept")]
        public IActionResult ViewReturnFromDept(int ReturnId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvReturnfromDept.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewReturnfromDeptReturnIdwise(ReturnId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvReturnfromDept", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-ReturnfromDeptDatewise")]
        public IActionResult ViewReturnFromDeptdatewise(DateTime FromDate,DateTime ToDate, int FromStoreId,int ToStoreId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvReturnfrdeptdatewise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IssuetoDepartment.ViewReturnfrdeptdatewise(FromDate, ToDate, FromStoreId, ToStoreId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "InvReturnfrdeptdatewise", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IndentWise")]
        public IActionResult ViewIndentewise(int IndentId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IndentReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _indent.ViewIndentwise(IndentId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IndentReport", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpPost("MaterialConsumptionSave")]
        public IActionResult MaterialConsumptionSave(MaterialConsumptionParam MaterialConsumptionParam)
        {
            var IndentInsert = _InvMaterialConsumption.Insert(MaterialConsumptionParam);
            return Ok(IndentInsert);
        }
    }
}

