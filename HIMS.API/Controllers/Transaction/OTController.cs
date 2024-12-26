using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using HIMS.Data.CustomerAMCInfo;
using HIMS.Data.CustomerInformation;
using HIMS.Data.CustomerPayment;
using HIMS.Model.CustomerInformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HIMS.API.Comman;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace HIMS.API.Controllers.Transaction
{ //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OTController : Controller
    {
        public readonly I_CustomerInformation _CustomerInformation;
        public OTController (I_CustomerInformation customerInformation)
        {
            this._CustomerInformation = customerInformation;
        }
        [HttpPost("SaveOTBookingRequest")]
        public IActionResult SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _CustomerInformation.SaveOTBookingRequest(OTBookingRequestParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateOTBookingRequest")]
        public IActionResult UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _CustomerInformation.UpdateOTBookingRequest(OTBookingRequestParam);
            return Ok(Id);
        }
        [HttpPost("CancelOTBookingRequest")]
        public IActionResult CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _CustomerInformation.CancelOTBookingRequest(OTBookingRequestParam);
            return Ok(Id);
        }
        [HttpPost("SaveOTBooking")]
        public IActionResult SaveOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _CustomerInformation.SaveOTBooking(OTBookingParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateOTBooking")]
        public IActionResult UpdateOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _CustomerInformation.UpdateOTBooking(OTBookingParam);
            return Ok(Id);
        }
        [HttpPost("CancelOTBooking")]
        public IActionResult CancelOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _CustomerInformation.CancelOTBooking(OTBookingParam);
            return Ok(Id);
        }
        [HttpPost("SaveConsentMaster")]
        public IActionResult SaveConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _CustomerInformation.SaveConsentMaster(ConsentMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateConsentMaster")]
        public IActionResult UpdateConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _CustomerInformation.UpdateConsentMaster(ConsentMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelConsentMaster")]
        public IActionResult CancelConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _CustomerInformation.CancelConsentMaster(ConsentMasterParam);
            return Ok(Id);
        }
    }
}
