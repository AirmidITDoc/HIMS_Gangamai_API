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
using HIMS.Data.OT;

namespace HIMS.API.Controllers.Transaction
{ //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OTController : Controller
    {
        public readonly I_OT _OT;
        public readonly I_CustomerInformation _CustomerInformation;
        public OTController (I_CustomerInformation customerInformation, I_OT _OT)
        {
            this._OT = _OT;
            this._CustomerInformation = customerInformation;
        }
        [HttpPost("SaveOTBookingRequest")]
        public IActionResult SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _OT.SaveOTBookingRequest(OTBookingRequestParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateOTBookingRequest")]
        public IActionResult UpdateOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _OT.UpdateOTBookingRequest(OTBookingRequestParam);
            return Ok(Id);
        }
        [HttpPost("CancelOTBookingRequest")]
        public IActionResult CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _OT.CancelOTBookingRequest(OTBookingRequestParam);
            return Ok(Id);
        }
        [HttpPost("SaveOTBooking")]
        public IActionResult SaveOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _OT.SaveOTBooking(OTBookingParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateOTBooking")]
        public IActionResult UpdateOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _OT.UpdateOTBooking(OTBookingParam);
            return Ok(Id);
        }
        [HttpPost("CancelOTBooking")]
        public IActionResult CancelOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _OT.CancelOTBooking(OTBookingParam);
            return Ok(Id);
        }
        [HttpPost("SaveConsentMaster")]
        public IActionResult SaveConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _OT.SaveConsentMaster(ConsentMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateConsentMaster")]
        public IActionResult UpdateConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _OT.UpdateConsentMaster(ConsentMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelConsentMaster")]
        public IActionResult CancelConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _OT.CancelConsentMaster(ConsentMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveOTTableMaster")]
        public IActionResult SaveOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            var Id = _OT.SaveOTTableMaster(MOTTableMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateOTTableMaster")]
        public IActionResult UpdateOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            var Id = _OT.UpdateOTTableMaster(MOTTableMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelOTTableMaster")]
        public IActionResult CancelOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            var Id = _OT.CancelOTTableMaster(MOTTableMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveMOTSurgeryCategoryMaster")]
        public IActionResult SaveMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            var Id = _OT.SaveMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMOTSurgeryCategoryMaster")]
        public IActionResult UpdateMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            var Id = _OT.UpdateMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelMOTSurgeryCategoryMaster")]
        public IActionResult CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            var Id = _OT.CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveMOTTypeMaster")]
        public IActionResult SaveMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            var Id = _OT.SaveMOTTypeMaster(MOTTypeMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMOTTypeMaster")]
        public IActionResult UpdateMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            var Id = _OT.UpdateMOTTypeMaster(MOTTypeMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelMOTTypeMaster")]
        public IActionResult CancelMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            var Id = _OT.CancelMOTTypeMaster(MOTTypeMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveMOTSiteDescriptionMaster")]
        public IActionResult SaveMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            var Id = _OT.SaveMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMOTSiteDescriptionMaster")]
        public IActionResult UpdateMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            var Id = _OT.UpdateMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelMOTSiteDescriptionMaster")]
        public IActionResult CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            var Id = _OT.CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveMOTSurgeryMaster")]
        public IActionResult SaveMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            var Id = _OT.SaveMOTSurgeryMaster(MOTSurgeryMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMOTSurgeryMaster")]
        public IActionResult UpdateMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            var Id = _OT.UpdateMOTSurgeryMaster(MOTSurgeryMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelMOTSurgeryMaster")]
        public IActionResult CancelMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            var Id = _OT.CancelMOTSurgeryMaster(MOTSurgeryMasterParam);
            return Ok(Id);
        }
    }
}
