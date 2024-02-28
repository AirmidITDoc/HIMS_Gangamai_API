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
    
        public InventoryTransactionController(
             I_Indent indent
            ,I_IssueTrackingInfo issueTrackingInfo
            ,Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment
            , IPdfUtility pdfUtility
            , IFileUtility fileUtility            , I_IssuetoDepartment issuetoDepartment )
        {
            this._indent = indent;
            this._IssueTrackingInfo = issueTrackingInfo;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _IFileUtility = fileUtility;            _IssuetoDepartment = issuetoDepartment;

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

        [HttpPost("IssuetoDepartmentSave")]
        public IActionResult IssuetoDepartmentSave(IssuetoDepartmentParams issuetoDepartmentParams)
        {
            var IndentInsert = _IssuetoDepartment.InsertIssuetoDepartment(issuetoDepartmentParams);
            return Ok(IndentInsert);
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
    }
}

