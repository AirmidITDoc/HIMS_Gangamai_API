//using HIMS.API.Utility;
//using HIMS.Data.Opd;
//using HIMS.Data.InventoryReports;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HIMS.API.Controllers.Transaction
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class InventoryReportController : Controller
//    {
//        public readonly I_InventoryReport _IPInventory;
//        public readonly IPdfUtility _pdfUtility;
//        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
//        public InventoryReportController(
//           I_InventoryReport ipminventory, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
//        {
//            _IPInventory = ipinventory;
//            _hostingEnvironment = hostingEnvironment;
//            _pdfUtility = pdfUtility;

//        }

//        [HttpGet("view-ItemList")]
//        public IActionResult ViewItemList(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId)
//        {
//            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MISReport_CityWiseIPPatientCountReport.html");
//            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
//            var html = _IPInventory.ViewItemList(FromDate, ToDate, AddedById, DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
//            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ItemList", "ItemList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

//            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
//        }
//    }
//}


