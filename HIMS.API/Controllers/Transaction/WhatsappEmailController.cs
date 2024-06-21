using HIMS.API.Utility;
using HIMS.Data.Pharmacy;
using HIMS.Data.WhatsAppEmail;
using HIMS.Model.WhatsAppEmail;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HIMS.Data.Opd;
using HIMS.Data.IPD;
using HIMS.Model.IPD;

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
        public readonly I_OPbilling _OPbilling;
        public readonly I_IPAdvance _IPAdvance;
        public WhatsappEmailController(I_Sales sales, I_WhatsappSms whatsappSms, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment ,IPdfUtility pdfUtility,
            I_GRN gRN,I_PurchaseOrder purchaseOrder,
            I_OPbilling oPbilling,
            I_IPAdvance ipAdvance)
        {
            this._Sales = sales;
            this._WhatsappSms = whatsappSms;
            _hostingEnvironment = hostingEnvironment;
            this._pdfUtility = pdfUtility;
            this._GRN = gRN;
            this._PurchaseOrder = purchaseOrder;
            this._OPbilling = oPbilling;
            this._IPAdvance = ipAdvance;
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

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
                var html = _Sales.ViewSalesTaxReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath, htmlHeaderFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesTaxReceipt", "PharmaBill_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "SalesReturn")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
                var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "SalesReturnReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Appointment")
            {
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = "";
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "OPBill")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _OPbilling.ViewOPBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath,_pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OpBillingReceipt", "OpBilling" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPAdvance")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdvanceReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPAdvance.ViewAdvanceReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IP_Advance", "IP_Advance_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPBill")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdvanceReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPAdvance.ViewAdvanceReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IP_Advance", "IP_Advance_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPInterim")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdvanceReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPAdvance.ViewAdvanceReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IP_Advance", "IP_Advance_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }

            var Id = _WhatsappSms.Insert(WhatsappSmsparam);
            return Ok(Id);
        }

        [HttpPost("EmailSave")]
        public IActionResult EmailInsert(WhatsappSmsparam WhatsappSmsparam)
        {

            if (WhatsappSmsparam.InsertEamil.EmailType == "GRN")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReport.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
                var html = _GRN.ViewGRNReport(WhatsappSmsparam.InsertEamil.TranNo, htmlFilePath, htmlHeaderFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "GRNReport", "GRNReport_" + WhatsappSmsparam.InsertEamil.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertEamil.EmailType == "Purchase")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseorderNew.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
                var html = _PurchaseOrder.ViewPurchaseorder(WhatsappSmsparam.InsertEamil.TranNo, htmlFilePath, htmlHeaderFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PurchaseorderNew", "PurchaseorderNew_" + WhatsappSmsparam.InsertEamil.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertEamil.EmailType == "Sales")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
                var html = _Sales.ViewBill(WhatsappSmsparam.InsertEamil.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + WhatsappSmsparam.InsertEamil.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertEamil.EmailType == "SalesReturn")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
                var html = _Sales.ViewBill(WhatsappSmsparam.InsertEamil.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SalesReturnReceipt", "SalesReturnReceipt" + WhatsappSmsparam.InsertEamil.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            }

            var Id = _WhatsappSms.InsertEmail(WhatsappSmsparam);
            return Ok(Id);
        }

    }


}
