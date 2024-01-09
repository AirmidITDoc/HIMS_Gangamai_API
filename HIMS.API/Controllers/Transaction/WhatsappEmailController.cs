using HIMS.API.Utility;
using HIMS.Data.IPD;
using HIMS.Data.Pharmacy;
using HIMS.Model.IPD;
using Microsoft.AspNetCore.Hosting;
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
    public class WhatsappEmailController : Controller
    {
        public readonly I_Sales _Sales;
        public readonly I_WhatsappSms _WhatsappSms;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public WhatsappEmailController(I_Sales sales, I_WhatsappSms whatsappSms, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment ,IPdfUtility pdfUtility)
        {
            this._Sales = sales;
            this._WhatsappSms = whatsappSms;
            _hostingEnvironment = hostingEnvironment;
            this._pdfUtility = pdfUtility;
        }



        [HttpPost("WhatsappSalesSave")]
        public IActionResult InsertWhatsappSales(WhatsappSmsparam WhatsappSmsparam)
        {

            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
            var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

            var Id = _WhatsappSms.Insert(WhatsappSmsparam);
            return Ok(Id);
        }

        [HttpPost("WhatsappSalesReturnSave")]
        public IActionResult WhatsappSalesReturnSave(WhatsappSmsparam WhatsappSmsparam)
        {

            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _Sales.ViewSalesReturnReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "SalesReturnReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

            var Id = _WhatsappSms.Insert(WhatsappSmsparam);
            return Ok(Id);
        }
    }
}
