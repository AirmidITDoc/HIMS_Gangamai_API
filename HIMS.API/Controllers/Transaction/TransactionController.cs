using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Transaction;
using HIMS.Model.Transaction;


namespace HIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        public readonly I_VendorAdvancePayment _VendorAdvancePayment;
        public readonly I_CustomerAdvancePayment _CustomerAdvancePayment;
        public readonly I_CustomerEnquiry _CustomerEnquiry;
        public readonly I_Customer _Customer;
        public readonly I_CustomerInformation_Sw _CustomerInformation_Sw;
        public readonly I_CustomerInfo_PayAdv_Sw _CustomerInfo_PayAdv_Sw;
        public readonly I_IssueTracking_Sw _IssueTracking_Sw;
        public readonly I_ProjectInformation _ProjectInformation;
        public readonly I_Sw_Bill_info _Sw_Bill_Info;

        public TransactionController(
                         I_VendorAdvancePayment vendorAdvancePayment,
                         I_CustomerAdvancePayment customerAdvancePayment,
                         I_CustomerEnquiry customerEnquiry,
                         I_Customer customer,
                         I_CustomerInformation_Sw customerInformation_Sw,
                         I_CustomerInfo_PayAdv_Sw CustomerInfo_PayAdv_Sw,
                         I_IssueTracking_Sw issueTracking_Sw,
                         I_ProjectInformation projectInformation,
                         I_Sw_Bill_info sw_Bill_Info
                         )
        {
            this._VendorAdvancePayment = vendorAdvancePayment;
            this._CustomerAdvancePayment = customerAdvancePayment;
            this._CustomerEnquiry = customerEnquiry;
            this._Customer = customer;
            this._CustomerInformation_Sw = customerInformation_Sw;
            this._CustomerInfo_PayAdv_Sw = CustomerInfo_PayAdv_Sw;
            this._IssueTracking_Sw = issueTracking_Sw;
            this._ProjectInformation = projectInformation;
            this._Sw_Bill_Info = sw_Bill_Info;
        }


        [HttpPost("VendorAdvancePaymentSave")]
        public IActionResult VendorAdvancePaymentSave(VendorAdvancePaymentParams vendorAdvancePaymentParams)
        {
            var producttypeM = _VendorAdvancePayment.Save(vendorAdvancePaymentParams);
            return Ok(producttypeM);
        }

        [HttpPost("VendorAdvancePaymentUpdate")]
        public IActionResult VendorAdvancePaymentUpdate(VendorAdvancePaymentParams vendorAdvancePaymentParams)
        {
            var producttypeM = _VendorAdvancePayment.Update(vendorAdvancePaymentParams);
            return Ok(producttypeM);

            // ProductTypeMaster Master Insert & Update
        }
        [HttpPost("CustomerSave")]

        public IActionResult CustomerSave(CustomerParams CustomerParams)
        {
                var producttypeM = _Customer.Save(CustomerParams);
                //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
                return Ok(producttypeM);
        }
        [HttpPost("CustomerUpdate")]
        public IActionResult CustomerUpdate(CustomerParams CustomerParams)
        {
            var ServiceSave = _Customer.Update(CustomerParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CustomerEnquirySave")]
        public IActionResult CustomerEnquirySave(CustomerEnquiryParams customerEnquiryParams)
        {
            var CustomerE = _CustomerEnquiry.Save(customerEnquiryParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(CustomerE);
        }
        [HttpPost("CustomerEnquiryUpdate")]
        public IActionResult CustomerEnquiryUpdate(CustomerEnquiryParams CustomerEnquiryParams)
        {
            var CustomerE = _CustomerEnquiry.Update(CustomerEnquiryParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(CustomerE);

        }

        [HttpPost("CustomerAdvancePaymentSave")]
        public IActionResult CustomerAdvancePaymentSave(CustomerAdvancePaymentParams customerAdvancePaymentParams)
        {
            var producttypeM = _CustomerAdvancePayment.Save(customerAdvancePaymentParams);
            return Ok(producttypeM);
        }


        [HttpPost("CustomerAdvancePaymentUpdate")]
        public IActionResult CustomerAdvancePaymentUpdate(CustomerAdvancePaymentParams customerAdvancePaymentParams)
        {
            var ServiceSave = _CustomerAdvancePayment.Update(customerAdvancePaymentParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CustomerInformation_SwSave")]
        public IActionResult CustomerInformation_SwSave(CustomerInformation_SwParams CustomerInformation_SwParams)
        {
            var custinfo = _CustomerInformation_Sw.Save(CustomerInformation_SwParams);
            return Ok(custinfo);
        }
        [HttpPost("CustomerInformation_SwUpdate")]
            
        public IActionResult CustomerInformation_SwUpdate(CustomerInformation_SwParams CustomerInformation_SwParams)
        {
            var custinfo = _CustomerInformation_Sw.Update(CustomerInformation_SwParams);
            return Ok(custinfo);
        }

        [HttpPost("CustomerInfo_PayAdv_SwSave")]
            
        public IActionResult CustomerInfo_PayAdv_SwSave(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams)
        {
            var custinfo = _CustomerInfo_PayAdv_Sw.Save(CustomerInfo_PayAdv_SwParams);
            return Ok(custinfo);
        }
        [HttpPost("CustomerInfo_PayAdv_SwUpdate")]
            
        public IActionResult CustomerInfo_PayAdv_SwUpdate(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams)
        {
            var custinfo = _CustomerInfo_PayAdv_Sw.Update(CustomerInfo_PayAdv_SwParams);
            return Ok(custinfo);
        }

        [HttpPost("IssueTracking_SwSave")]
            
        public IActionResult IssueTracking_SwSave(IssueTracking_SwParams IssueTracking_SwParams)
        {
            var issu = _IssueTracking_Sw.Save(IssueTracking_SwParams);

            return Ok(issu);
        }
        [HttpPost("IssueTracking_SwUpdate")]

        public IActionResult IssueTracking_SwUpdate(IssueTracking_SwParams IssueTracking_SwParams)
        {
            var issu = _IssueTracking_Sw.Update(IssueTracking_SwParams);

            return Ok(issu);
        }

        //ProjectInformation Save And Update
        [HttpPost("ProjectInformationSave")]

        public IActionResult ProjectInformationSave(ProjectInformationParams ProjectInformationParams)
        {
            var pinfo = _ProjectInformation.Save(ProjectInformationParams);

            return Ok(pinfo);
        }
        [HttpPost("ProjectInformationUpdate")]

        public IActionResult ProjectInformationUpdate(ProjectInformationParams ProjectInformationParams)
        {
            var pinfo = _ProjectInformation.Update(ProjectInformationParams);

            return Ok(pinfo);
        }


        //I_Sw_Bill_info Save And Update
        [HttpPost("I_Sw_Bill_infoSave")]

        public IActionResult I_Sw_Bill_infoSave(Sw_Bill_infoParams Sw_Bill_infoParams)
        {
            var pinfo = _Sw_Bill_Info.Save(Sw_Bill_infoParams);

            return Ok(pinfo);
        }
        [HttpPost("I_Sw_Bill_infoUpdate")]
        public IActionResult I_Sw_Bill_infoUpdate(Sw_Bill_infoParams Sw_Bill_infoParams)
        {
            var pinfo = _Sw_Bill_Info.Update(Sw_Bill_infoParams);

            return Ok(pinfo);
        }
    }
    } 