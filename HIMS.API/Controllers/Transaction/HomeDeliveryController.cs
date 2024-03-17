using HIMS.API.Utility;
using HIMS.Data.HomeDelivery;
using HIMS.Model.HomeDelivery;
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
    public class HomeDeliveryController : Controller
    {
        public readonly I_HomeDeliveryLogin _HomeDeliveryLogin;
        public readonly I_HomeDeliveryOrder _HomeDeliveryOrder;
     
        public HomeDeliveryController(I_HomeDeliveryLogin homeDeliveryLogin, I_HomeDeliveryOrder homeDeliveryOrder)
        {
            this._HomeDeliveryLogin = homeDeliveryLogin;
            _HomeDeliveryOrder = homeDeliveryOrder;
        }

        [HttpPost("HomeDeliverLoginCreate")]
        public IActionResult HomeDeliverLoginCreate(HomeDeliveryLoginParams homeDeliveryLoginParams)
        {
            var Id = _HomeDeliveryLogin.HomeDeliveryLoginInsert(homeDeliveryLoginParams);
            return Ok(Id);
        }

        [HttpPost("HomeDeliveryOrderInsert")]
        public IActionResult HomeDeliveryOrderInsert(HomeDeliveryOrderParams homeDeliveryOrderParams)
        {
            var Id = _HomeDeliveryOrder.HomeDeliveryOrderInsert(homeDeliveryOrderParams);
            return Ok(Id);
        }

    }


}
