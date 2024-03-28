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
        private readonly IFileUtility _IFileUtility;
        public HomeDeliveryController(I_HomeDeliveryLogin homeDeliveryLogin, I_HomeDeliveryOrder homeDeliveryOrder, IFileUtility fileUtility)
        {
            this._HomeDeliveryLogin = homeDeliveryLogin;
            _HomeDeliveryOrder = homeDeliveryOrder;
            _IFileUtility = fileUtility;

        }

        [HttpPost("MobileAppLoginCreate")]
        public IActionResult MobileAppLoginCreate(HomeDeliveryLoginParams homeDeliveryLoginParams)
        {
            var Id = _HomeDeliveryLogin.HomeDeliveryLoginInsert(homeDeliveryLoginParams);
            return Ok(Id);
        }

        [HttpPost("MobileAppProfileUpdate")]
        public IActionResult MobileAppProfileUpdate(HomeDeliveryLoginParams homeDeliveryLoginParams)
        {
            var Id = _HomeDeliveryLogin.HomeDeliveryProfileUpdate(homeDeliveryLoginParams);
            return Ok(Id);
        }

        [HttpPost("HomeDeliveryOrderInsert")]
        public async Task<IActionResult> HomeDeliveryOrderInsertAsync([FromForm] HomeDeliveryOrderParams homeDeliveryOrderParams)
        {
            string NewFileName = homeDeliveryOrderParams.HomeDeliveryOrderInsert.CustomerID.ToString();//Guid.NewGuid().ToString();
            string FileName = await _IFileUtility.UploadDocument(homeDeliveryOrderParams.HomeDeliveryOrderInsert.ImgFile, "HomeDelivery", NewFileName);
            homeDeliveryOrderParams.HomeDeliveryOrderInsert.UploadDocument = FileName;

            var Id = _HomeDeliveryOrder.HomeDeliveryOrderInsert(homeDeliveryOrderParams);
            return Ok(Id);
        }

    }


}
