using HIMS.API.Utility;
using HIMS.Data.Pharmacy;
using HIMS.Data.WhatsAppEmail;
using HIMS.Model.WhatsAppEmail;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HIMS.Data.Opd;
using HIMS.Data.IPD;
using HIMS.Model.IPD;
using HIMS.Data.Pathology;
using System.Data;
using HIMS.Common.Utility;

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
        public readonly I_Payment _Payment;
        public readonly I_OPRefundBill _OPRefundBill;
        public readonly I_IPRefundofBilll _IPRefundofBilll;
        public readonly I_IPRefundofAdvance _IPRefundofAdvance;
        public readonly I_IP_Settlement_Process _IP_Settlement_Process;
        public readonly I_IPInterimBill _IPInterimBill;
        public readonly I_IPBilling _IPBilling;
        public readonly I_OPDPrescription _OPDPrescription;
        public readonly I_IPPrescription _IPPrescription;
        public readonly I_pathresultentry _Pathresultentry;
        public readonly I_PathologyTemplateResult _PathologyTemplateResult;
        private readonly IFileUtility _FileUtility;
        public WhatsappEmailController(I_Sales sales, I_WhatsappSms whatsappSms, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment ,IPdfUtility pdfUtility,
            I_GRN gRN,I_PurchaseOrder purchaseOrder, I_Payment Payment, I_OPRefundBill oPRefundBill, I_IPRefundofAdvance iPRefundofAdvance, I_IP_Settlement_Process iP_Settlement_Process, I_IPInterimBill ipinterimbill,
            I_IPRefundofBilll iPRefundofBilll, I_IPBilling iPBilling, I_OPDPrescription oPDPrescription, I_IPPrescription iPPrescription, I_pathresultentry pathresultentry, I_PathologyTemplateResult pathologyTemplateResult,
            I_OPbilling oPbilling,IFileUtility fileUtility,
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
            this._OPRefundBill = oPRefundBill;
            this._Payment = Payment;
            this._IPRefundofAdvance = iPRefundofAdvance;
            this._IPRefundofBilll = iPRefundofBilll;
            this._IP_Settlement_Process = iP_Settlement_Process;
            this._IPInterimBill = ipinterimbill;
            this._IPBilling = iPBilling;
            this._IPPrescription = iPPrescription;
            this._OPDPrescription = oPDPrescription;
            this._Pathresultentry = pathresultentry;
            this._PathologyTemplateResult = pathologyTemplateResult;
            _FileUtility = fileUtility;
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

              
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPBillingReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPBilling.ViewIPBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IpBillingReceipt", "IpBillingReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPInterim")
            {

             
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPInterimBill.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPInterimBill.ViewIPInterimBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPInterimBill", "IPInterimBill" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPREFADVANCE")
            {

              
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RefundofAdvanceReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPRefundofAdvance.ViewIPRefundofAdvanceReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefundofAdvanceReceipt", "RefundofAdvanceReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPREFBILL")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPRefundBillReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPRefundofBilll.ViewIPRefundofBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPRefundBillReceipt", "IPRefundBillReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPRECEIPT")
            {

              
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SettlementReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IP_Settlement_Process.ViewSettlementReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SettlementReceipt", "SettlementReceipt" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "IPPrescription")
            {

                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IpPrescription.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _IPPrescription.ViewIPPrescriptionReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IpPrescription", "IpPrescription" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            //Op Part
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "Appointment")
            {
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = "";
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "OPBill")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _OPbilling.ViewOPBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OpBillingReceipt", "OpBilling" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "OPREFBILL")
            {
             
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPRefundofbill.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _OPRefundBill.ViewOPRefundofBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPRefundofbill", "OPRefundofbill" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "OPRECEIPT")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _OPbilling.ViewOPBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OpBillingReceipt", "OpBilling" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "OPPRESCRIPTIONT")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                var html = _OPbilling.ViewOPBillReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OpBillingReceipt", "OpBilling" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "PathlogyTestResult")
            {
                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathologyResultTest.html");
                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                DataTable dt = _Pathresultentry.GetDataForReport(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo);
                var html = _Pathresultentry.ViewPathTestMultipleReport(dt, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                var signature = _FileUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());
                html = html.Replace("{{Signature}}", signature);
                var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PathologyResultTest", "PathologyResultTest" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
            }
            else if (WhatsappSmsparam.InsertWhatsappsmsInfo.SMSType == "PathlogyTemplateResult")
            {
              //  string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTemplate.html");
             //   string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
              //  var html = _PathologyTemplateResult.ViewPathTemplateReceipt(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo);
                //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PathTemplate", "PathTemplate" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                //WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;
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
              
                //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescription.html");
                //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //var html = _OPDPrescription.ViewOPPrescriptionReceipt(WhatsappSmsparam.InsertEamil.TranNo, WhatsappSmsparam.InsertEamil.TranNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
                //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPPrescription", "OPPrescription" + WhatsappSmsparam.InsertEamil.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                //WhatsappSmsparam.InsertEamil.AttachmentLink = tuple.Item2;
            }

            var Id = _WhatsappSms.InsertEmail(WhatsappSmsparam);
            return Ok(Id);
        }

    }


}
