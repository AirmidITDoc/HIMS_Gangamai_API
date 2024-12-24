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
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInformationController : Controller
    {
        public readonly I_CustomerInformation _CustomerInformation;
        public readonly I_CustomerPayments _CustomerPayments;
        public readonly I_CustomerInvoiceRaise _CustomerInvoiceRaise;
        public readonly I_CustomerAMCInfo _CustomerAMCInfo;

        public CustomerInformationController(I_CustomerInformation customerInformation, I_CustomerPayments customerPayments,
            I_CustomerInvoiceRaise customerInvoiceRaise, I_CustomerAMCInfo customerAMCInfo)
        {
            this._CustomerInformation = customerInformation;
            _CustomerPayments = customerPayments;
            _CustomerInvoiceRaise = customerInvoiceRaise;
            _CustomerAMCInfo = customerAMCInfo;
        }
        [HttpPost("SaveOTBookingRequest")]
        public IActionResult SaveOTBookingRequest(OTBookingRequestParam OTBookingRequestParam)
        {
            var Id = _CustomerInformation.SaveOTBookingRequest(OTBookingRequestParam);
            return Ok(Id);
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
            return Ok(Id);
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
        [HttpPost("SaveCertificateMaster")]
        public IActionResult SaveCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            var Id = _CustomerInformation.SaveCertificateMaster(CertificateMasterParam);
            return Ok(Id);
        }
        [HttpPost("UpdateCertificateMaster")]
        public IActionResult UpdateCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            var Id = _CustomerInformation.UpdateCertificateMaster(CertificateMasterParam);
            return Ok(Id);
        }
        [HttpPost("CancelCertificateMaster")]
        public IActionResult CancelCertificateMaster(CertificateMasterParam CertificateMasterParam)
        {
            var Id = _CustomerInformation.CancelCertificateMaster(CertificateMasterParam);
            return Ok(Id);
        }
        [HttpPost("SaveConsentMaster")]
        public IActionResult SaveConsentMaster(ConsentMasterParam ConsentMasterParam)
        {
            var Id = _CustomerInformation.SaveConsentMaster(ConsentMasterParam);
            return Ok(Id);
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
        [HttpPost("SaveVendorInformation")]
        public IActionResult SaveVendorInformation(VendorInformationParam VendorInformationParam)
        {
            var Id = _CustomerInformation.SaveVendorInformation(VendorInformationParam);
            return Ok(Id);
        }

        [HttpPost("UpdateVendorInformation")]
        public IActionResult UpdateVendorInformation(VendorInformationParam VendorInformationParam)
        {
            var Id = _CustomerInformation.UpdateVendorInformation(VendorInformationParam);
            return Ok(true);
        }
        [HttpPost("CustomerInformationSave")]
        public IActionResult CustomerInformationSave(CustomerInformationParams customerInformationParams)
        {
            var Id = _CustomerInformation.CustomerInformationInsert(customerInformationParams);
            return Ok(Id);
        }

        [HttpPost("CustomerInformationUpdate")]
        public IActionResult CustomerInformationUpdate(CustomerInformationParams customerInformationParams)
        {
            var Id = _CustomerInformation.CustomerInformationUpdate(customerInformationParams);
            return Ok(true);
        }

        [HttpPost("CustomerPaymentSave")]
        public IActionResult CustomerPaymentSave(CustomerPaymentParams customerPaymentParams)
        {
            var Id = _CustomerPayments.CustomerPaymentInsert(customerPaymentParams);
            return Ok(Id);
        }
        [HttpPost("CustomerPaymentUpdate")]
        public IActionResult CustomerPaymentUpdate(CustomerPaymentParams customerPaymentParams)
        {
            var Id = _CustomerPayments.CustomerPaymentUpdate(customerPaymentParams);
            return Ok(true);
        }
        [HttpPost("CustomerPaymentCancel")]
        public IActionResult CustomerPaymentCancel(CustomerPaymentParams customerPaymentParams)
        {
            var Id = _CustomerPayments.CustomerPaymentCancel(customerPaymentParams);
            return Ok(true);
        }

        [HttpPost("CustomerInvoiceRaiseSave")]
        public IActionResult CustomerInvoiceRaiseSave(CustomerInvoiceRaiseParam CustomerInvoiceRaiseParam)
        {
            var Id = _CustomerInvoiceRaise.CustomerInvoiceRaiseInsert(CustomerInvoiceRaiseParam);
            return Ok(Id);
        }
        [HttpPost("CustomerInvoiceRaiseUpdate")]
        public IActionResult CustomerInvoiceRaiseUpdate(CustomerInvoiceRaiseParam customerInvoiceRaiseParam)
        {
            var Id = _CustomerInvoiceRaise.CustomerInvoiceRaiseUpdate(customerInvoiceRaiseParam);
            return Ok(true);
        }
        [HttpPost("CustomerAMCSave")]
        public IActionResult CustomerAMCSave(CustomerAmcParams customerAmcParams)
        {
            var Id = _CustomerAMCInfo.CustomerAMCInsert(customerAmcParams);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully",Id);
            return Ok(Response);
        }

        [HttpPost("CustomerAMCUpdate")]
        public IActionResult CustomerAMCUpdate(CustomerAmcParams customerAmcParams)
        {
            _CustomerAMCInfo.CustomerAMCUpdate(customerAmcParams);
            return Ok(true);
        }

        [HttpPost("CustomerAMCCancel")]
        public IActionResult CustomerAMCCancel(CustomerAmcParams customerAmcParams)
        {
            _CustomerAMCInfo.CustomerAMCUpdate(customerAmcParams);
            return Ok(true);

        }

        [HttpPost("AMCCancel")]
        [ValidateModel]
        public IActionResult AMCCancel(CustomerAmcParams customerAmcParams)
        {
            try
            {
                // Step 1: Validate the input using model state
                if (!ModelState.IsValid)
                {
                    // If validation fails, return a BadRequest with the validation errors
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(ApiResponseHelper.GenerateResponse<string>(
                        ApiStatusCode.Status400BadRequest,
                        "Validation failed.",
                        null,
                        errors
                    ));
                }

                // Step 1: Validate the input
                if (customerAmcParams == null)
                {
                    return BadRequest(ApiResponseHelper.GenerateResponse<string>(
                        ApiStatusCode.Status400BadRequest,
                        "Invalid request. Customer AMC parameters cannot be null."
                    ));
                }

                // Step 2: Call the business logic layer
                var result = _CustomerAMCInfo.CustomerAMCCancel(customerAmcParams);

                // Step 3: Handle result from the business layer
                if (!result)
                {
                    return Conflict(ApiResponseHelper.GenerateResponse<string>(
                        ApiStatusCode.Status409Conflict,
                        "Unable to cancel the record due to a conflict or invalid data."
                    ));
                }

                // Step 4: Return success response
                var response = ApiResponseHelper.GenerateResponse<string>(
                    ApiStatusCode.Status200OK,
                    "Record cancelled successfully"
                );
                return Ok(response);

            }
            catch (Exception ex)
            {
                // Step 5: Catch unexpected errors
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponseHelper.GenerateResponse<string>(
                        ApiStatusCode.Status500InternalServerError,
                        "An unexpected error occurred while processing your request.",
                        null,
                        new List<string> { ex.Message }
                    )
                );
            }
        }

    }
}

