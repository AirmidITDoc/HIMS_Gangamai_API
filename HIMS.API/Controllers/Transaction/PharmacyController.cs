using HIMS.Data.Pharmacy;
using HIMS.Model.Pharmacy;
using Microsoft.AspNetCore.Mvc;


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

        public PharmacyController(I_Sales sales, I_PurchaseOrder purchaseOrder, I_SalesReturn salesReturn, I_GRN gRN)
        {
            this._Sales = sales;
            _PurchaseOrder = purchaseOrder;
            _SalesReturn = salesReturn;
            _GRN = gRN;
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

        [HttpPost("InsertSalesReturn")]
        public IActionResult InsertSalesReturn(SalesReturnParams salesReturnParams)
        {
            var SalesSave = _SalesReturn.InsertSalesReturn(salesReturnParams);
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
    }
}
