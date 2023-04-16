using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.Master;
using HIMS.Data.Master.Billing;
using HIMS.Data.Master.DoctorMaster;
using HIMS.Data.Master.PersonalDetails;
using HIMS.Data.Master.VendorMaster;
using HIMS.Model;
using HIMS.Model.Master;
using HIMS.Model.Master.VendorMaster;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {

        public readonly I_ServiceMaster _ServiceMasterResp;
        public readonly I_DoctorMaster _DoctorMasterResp;
        public readonly I_MenuMaster _MenuMaster;
        public readonly I_PayTranModeMaster _PayTranModeMaster;
        //public readonly I_ProductTypeMasterHome _ProductTypeMaster;
        public readonly I_MenuMasterDetails _MenuMasterDetails;
        public readonly I_MenuMasterDetails_Details _MenuMasterDetails_Details;
        public readonly I_VendorMaster _VendorMaster;
        // public readonly I_GenderMaster _GenderMaster;

        public MasterController(I_ServiceMaster ServiceMasterResp , I_DoctorMaster DoctorMasterResp,
            I_MenuMaster menuMaster, I_PayTranModeMaster payTranModeMaster,
            //I_ProductTypeMasterHome productTypeMaster,
            I_MenuMasterDetails menuMasterDetails,I_MenuMasterDetails_Details menuMasterDetails_Details,
            I_VendorMaster vendorMaster)
        {
            this._ServiceMasterResp = ServiceMasterResp;
            this._DoctorMasterResp = DoctorMasterResp;
            this._MenuMaster = menuMaster;
            this._PayTranModeMaster = payTranModeMaster;
           // this._ProductTypeMaster = productTypeMaster;
            this._MenuMasterDetails = menuMasterDetails;
            this._MenuMasterDetails_Details = menuMasterDetails_Details;
            this._VendorMaster = vendorMaster;
        }

        /* [HttpPost("ServiceSave")]
          public IActionResult ServiceSave(ServiceMasterParams  serviceMasterParams)
          {
              var ServiceSave = _ServiceMasterResp.Save(serviceMasterParams);
              return Ok(ServiceSave);
          }

          [HttpPost("ServiceUpdate")]
          public IActionResult ServiceUpdate(ServiceMasterParams serviceMasterParams)
          {
              var ServiceSave = _ServiceMasterResp.Update(serviceMasterParams);
              return Ok(ServiceSave);
          }

          [HttpPost("DoctorSave")]
          public IActionResult DoctorSave(DoctorMasterParams1 DoctorMasterParams1)
          {
              var ServiceSave = _DoctorMasterResp.Save(DoctorMasterParams1);
              return Ok(ServiceSave);
          }

          [HttpPost("DoctorUpdate")]
          public IActionResult DoctorUpdate(DoctorMasterParams1 DoctorMasterParams1)
          {
              var ServiceSave = _DoctorMasterResp.Update(DoctorMasterParams1);
              return Ok(ServiceSave);
          }

        // MenuMaster Master Insert & Update
        [HttpPost("MenuMasterSave")]
        public IActionResult MenuMasterSave(MenuMasterParamsHome menuMasterParams)
        {
            var menuMaster = _MenuMaster.Save(menuMasterParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(menuMaster);
        }

        [HttpPost("MenuMasterUpdate")]
        public IActionResult MenuMasterUpdate(MenuMasterParamsHome menuMasterParams)
        {
            var menuMaster = _MenuMaster.Update(menuMasterParams);
            return Ok(menuMaster);
        }*/
        // PayTranModeMaster Insert & Update
        [HttpPost("PayTranModeMasterSave")]
        public IActionResult PayTranModeMasterSave(PayTranModeMasterParams PayTranModeMasterParams)
        {
            var payMode = _PayTranModeMaster.Save(PayTranModeMasterParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(payMode);
        }

        [HttpPost("PayTranModeMasterUpdate")]
        public IActionResult PayTranModeMasterUpdate(PayTranModeMasterParams PayTranModeMasterParams)
        {
            var payMode = _PayTranModeMaster.Update(PayTranModeMasterParams);
            return Ok(payMode);
        }
        // ProductTypeMaster Master Insert & Update
      /*  [HttpPost("ProductTypeMasterSave")]
        public IActionResult ProductTypeMasterSave(ProductTypeMasterParams ProductTypeMasterParamsHome)
        {
            var producttypeM = _ProductTypeMaster.Insert(ProductTypeMasterParamsHome);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(producttypeM);
        }
        [HttpPost("ProductTypeMasterUpdate")]
        public IActionResult ProductTypeMasterUpdate(ProductTypeMasterParams productTypeMasterParams)
        {
            var ServiceSave = _ProductTypeMaster.update(productTypeMasterParams);
            return Ok(ServiceSave);
        }
        
        // MenuMasterDetails Insert & Update
        [HttpPost("MenuMasterDetailsSave")]
        public IActionResult MenuMasterDetailsSave(MenuMasterDetailsParams menuMasterDetailsParams)
        {
            var menuMasterDetails = _MenuMasterDetails.Save(menuMasterDetailsParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(menuMasterDetails);
        }

        [HttpPost("MenuMasterDetailsUpdate")]
        public IActionResult MenuMasterDetailsUpdate(MenuMasterDetailsParams menuMasterDetailsParams)
        {
            var menuMasterDetails = _MenuMasterDetails.Update(menuMasterDetailsParams);
            return Ok(menuMasterDetails);
        }

        // MenuMasterDetails_Details Insert & Update
        [HttpPost("MenuMasterDetails_DetailsSave")]
        public IActionResult MenuMasterDetails_DetailsSave(MenuMasterDetails_DetailsParams menuMasterDetails_DetailsParams)
        {
            var menuMasterDetails_Details = _MenuMasterDetails_Details.Save(menuMasterDetails_DetailsParams);
            //  var ServiceSave = _BankMasterResp.Save(bankMasterParams);
            return Ok(menuMasterDetails_Details);
        }

        [HttpPost("MenuMasterDetails_DetailsUpdate")]
        public IActionResult MenuMasterDetails_DetailsUpdate(MenuMasterDetails_DetailsParams menuMasterDetails_DetailsParams)
        {
            var menuMasterDetails_Details = _MenuMasterDetails_Details.Update(menuMasterDetails_DetailsParams);
            return Ok(menuMasterDetails_Details);
        }*/
        // Vendor Master Insert & Update
        [HttpPost("VendorSave")]
        public IActionResult VendorSave(VendorMasterParams vendorMasterParams)
        {
            var ServiceSave = _VendorMaster.Save(vendorMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("VendorUpdate")]
        public IActionResult VendorUpdate(VendorMasterParams vendorMasterParams)
        {
            var ServiceSave = _VendorMaster.Update(vendorMasterParams);
            return Ok(ServiceSave);
        }

    }
}
