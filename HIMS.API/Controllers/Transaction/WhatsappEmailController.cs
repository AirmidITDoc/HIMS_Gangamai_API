using HIMS.API.Utility;
using HIMS.Data.IPD;
using HIMS.Data.Pharmacy;
using HIMS.Data.WhatsAppEmail;
using HIMS.Model.WhatsAppEmail;
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
        private readonly I_GRN _GRN;
        private readonly I_PurchaseOrder _PurchaseOrder;

        public WhatsappEmailController(I_Sales sales, I_WhatsappSms whatsappSms, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment ,IPdfUtility pdfUtility,
            I_GRN gRN,I_PurchaseOrder purchaseOrder)
        {
            this._Sales = sales;
            this._WhatsappSms = whatsappSms;
            _hostingEnvironment = hostingEnvironment;
            this._pdfUtility = pdfUtility;
            this._GRN = gRN;
            this._PurchaseOrder = purchaseOrder;

        }



        [HttpPost("WhatsappSalesSave")]
        public IActionResult InsertWhatsappSales(WhatsappSmsparam WhatsappSmsparam)
        {

            if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "GRN")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReport.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
                var html = _GRN.ViewGRNReport(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, htmlHeaderFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReport", "GRNReport_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Purchase")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseorderNew.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
                var html = _PurchaseOrder.ViewPurchaseorder(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, htmlHeaderFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseorderNew", "PurchaseorderNew_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Sales")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
                var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "SalesReturn")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
                var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "SalesReturnReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

            }

            var Id = _WhatsappSms.Insert(WhatsappSmsparam);
            return Ok(Id);
        }

        [HttpPost("EmailSave")]
        public IActionResult EmailInsert(WhatsappSmsparam WhatsappSmsparam)
        {

            //if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "GRN")
            //{
            //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReport.html");
            //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            //    var html = _GRN.ViewGRNReport(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, htmlHeaderFilePath);
            //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReport", "GRNReport_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //    WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            //}
            //else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Purchase")
            //{
            //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseorderNew.html");
            //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            //    var html = _PurchaseOrder.ViewPurchaseorder(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, htmlHeaderFilePath);
            //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseorderNew", "PurchaseorderNew_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //    WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            //}
            //else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Sales")
            //{
            //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
            //    var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
            //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //    WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            //}
            //else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "SalesReturn")
            //{
            //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
            //    var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
            //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "SalesReturnReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //    WhatsappSmsparam.InsertEamil.AttachmentLink= tuple.Item2;
            //}

            var Id = _WhatsappSms.InsertEmail(WhatsappSmsparam);
            return Ok(Id);
        }

    }


}
