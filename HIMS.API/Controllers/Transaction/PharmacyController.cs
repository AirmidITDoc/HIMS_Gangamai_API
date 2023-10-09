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

        public PharmacyController(I_Sales sales)
        {
            this._Sales = sales;
        }

        [HttpPost("SalesSave")]
        public IActionResult SalesSave(SalesParams salesParams)
        {
            var SalesSave = _Sales.InsertSales(salesParams);
            return Ok(SalesSave.ToString());

        }
    }
}
