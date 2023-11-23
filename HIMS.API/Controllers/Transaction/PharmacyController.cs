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

         public PharmacyController(I_Sales sales, I_PurchaseOrder purchaseOrder, I_SalesReturn salesReturn, I_GRN gRN, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility,I_Workorder workorder)
        {
            this._Sales = sales;
            _PurchaseOrder = purchaseOrder;
            _SalesReturn = salesReturn;
            _GRN = gRN;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _Workorder = workorder;
        }

        [HttpPost("SalesSaveWithPayment")]
        public IActionResult SalesSaveWithPayment(SalesParams salesParams)
        {
            var SalesSave = _Sales.InsertSales(salesParams);
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
            return Ok(SalesSave.ToString());

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
            return Ok(SalesSave.ToString());

        }
        [HttpPost("InsertGRNPurchase")]
        public IActionResult InsertGRNPurchase(GRNParams grnParams)
        {
            var SalesSave = _GRN.InsertGRNPurchase(grnParams);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("VerifyGRN")]
        public IActionResult VerifyGRN(GRNParams grnParams)
        {
            var SalesSave = _GRN.VerifyGRN(grnParams);
            return Ok(SalesSave.ToString());

        }

      
        [HttpPost("InsertWorkorder")]
        public IActionResult InsertWorkorder(Workorder Workorder)
        {
            var SalesSave = _Workorder.InsertWorkOrder(Workorder);
            return Ok(SalesSave.ToString());

        }

        [HttpPost("updateWorkorder")]
        public IActionResult updateWorkorder(Workorder Workorder)
        {
            var SalesSave = _Workorder.UpdateWorkOrder(Workorder);
            return Ok(SalesSave.ToString());

        }


       [HttpGet("view-pharmacy-sale-bill")]
        public IActionResult ViewPharmaBill(int SalesID, int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
            var html = _Sales.ViewBill(SalesID, OP_IP_Type, htmlFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html);

            // write logic for send pdf in whatsapp
            if (System.IO.File.Exists(tuple.Item2))
                System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
      

        
    }
}
 