using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.Opd.OP;
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
    public class OPReportController : Controller
    {
        public readonly I_OPBillingReport _OPbilling;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public OPReportController(
           I_OPBillingReport oPbilling, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _OPbilling = oPbilling;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        [HttpGet("view-OPDailyCollectionReport")]
        public IActionResult ViewOPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPDailycollectionuserwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDailyCollectionReceipt(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPNewDailycollection", "OPNewDailycollection", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

       

    }
}
