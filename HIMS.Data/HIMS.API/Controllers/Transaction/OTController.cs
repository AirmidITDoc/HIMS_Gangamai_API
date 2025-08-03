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
using System.IO;
using HIMS.API.Utility;
using HIMS.Common.Utility;

namespace HIMS.API.Controllers.Transaction
{ //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OTController : Controller
    {
        public readonly I_OT _OT;

        public readonly I_CustomerInformation _CustomerInformation;
        public readonly IPdfUtility _pdfUtility;
        private readonly IFileUtility _IFileUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public OTController (I_CustomerInformation customerInformation, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility, I_OT _OT)
        {
            this._OT = _OT;
            _pdfUtility = pdfUtility;
            this._CustomerInformation = customerInformation;
            _hostingEnvironment = hostingEnvironment;
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelOTBookingRequest")]
        public IActionResult CancelOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _OT.CancelOTBookingRequest(OTBookingRequestParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelOTBooking")]
        public IActionResult CancelOTBooking(OTBookingParam OTBookingParam)
        {
            var Id = _OT.CancelOTBooking(OTBookingParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelConsentMaster")]
        public IActionResult CancelConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _OT.CancelConsentMaster(ConsentMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelOTTableMaster")]
        public IActionResult CancelOTTableMaster(MOTTableMasterParam MOTTableMasterParam)
        {
            var Id = _OT.CancelOTTableMaster(MOTTableMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelMOTSurgeryCategoryMaster")]
        public IActionResult CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam MOTSurgeryCategoryMasterParam)
        {
            var Id = _OT.CancelMOTSurgeryCategoryMaster(MOTSurgeryCategoryMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelMOTTypeMaster")]
        public IActionResult CancelMOTTypeMaster(MOTTypeMasterParam MOTTypeMasterParam)
        {
            var Id = _OT.CancelMOTTypeMaster(MOTTypeMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelMOTSiteDescriptionMaster")]
        public IActionResult CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam MOTSiteDescriptionMasterParam)
        {
            var Id = _OT.CancelMOTSiteDescriptionMaster(MOTSiteDescriptionMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
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
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
        [HttpPost("CancelMOTSurgeryMaster")]
        public IActionResult CancelMOTSurgeryMaster(MOTSurgeryMasterParam MOTSurgeryMasterParam)
        {
            var Id = _OT.CancelMOTSurgeryMaster(MOTSurgeryMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Cancel successfully", Id);
            return Ok(Response);
        }
        [HttpGet("view-TConsentInformation")]
        public IActionResult ViewTConsentInformation(int ConsentId,int OP_IP_Type)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "TConsentInformationReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OT.ViewTConsentInformation(ConsentId, OP_IP_Type, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "ViewTConsentInformation", "ViewTConsentInformation", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
    }
}
