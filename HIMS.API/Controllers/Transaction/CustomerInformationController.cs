using System.Net.Http;
using System.Threading.Tasks;
using HIMS.Data.CustomerInformation;
using HIMS.Data.CustomerPayment;
using HIMS.Model.CustomerInformation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HIMS.API.Controllers.Transaction
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInformationController : Controller
    {
        public readonly I_CustomerInformation _CustomerInformation;
        public readonly I_CustomerPayments _CustomerPayments;
        public readonly I_CustomerInvoiceRaise _CustomerInvoiceRaise;

        public CustomerInformationController(I_CustomerInformation customerInformation, I_CustomerPayments customerPayments,
            I_CustomerInvoiceRaise customerInvoiceRaise)
        {
            this._CustomerInformation = customerInformation;
            _CustomerPayments = customerPayments;
            _CustomerInvoiceRaise = customerInvoiceRaise;

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




    }
}

