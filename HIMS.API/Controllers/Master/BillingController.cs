using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Master.Billing;
using HIMS.Model.Master.Billing;

namespace HIMS.API.Controllers.Master
{

    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : Controller
    {
       /* public IActionResult Index()
        {
            return View();
        }*/

        public readonly I_CashCounterMaster _CashCounterMaster;
        public readonly I_ClassMaster _ClassMaster;
        public readonly I_TariffMaster _TariffMaster;
        public readonly I_GroupMaster _GroupMaster;
        public readonly I_SubGroupMaster _SubGroupMaster;
        public readonly I_ServiceMaster _ServiceMaster;
        public readonly I_CompanyMaster _CompanyMaster;
        public readonly I_CompanyTypeMaster _CompanyTypeMaster;
        public readonly I_SubTpaCompanyMaster _SubTpaCompanyMaster;
        public readonly I_ConsessionReasonMaster _ConsessionReasonMaster;
        public readonly I_BankMaster _BankMaster;

        public BillingController(I_BankMaster bankMaster, I_CashCounterMaster cashCounterMaster, I_ClassMaster classMaster,
            I_CompanyMaster companyMaster, I_CompanyTypeMaster companyTypeMaster,I_ConsessionReasonMaster consessionReasonMaster,
            I_GroupMaster groupMaster, I_ServiceMaster serviceMaster, I_SubGroupMaster subGroupMaster,
            I_SubTpaCompanyMaster subTpaCompanyMaster,
            I_TariffMaster tariffMaster

            )

        {

            this._CashCounterMaster = cashCounterMaster;
            this._ClassMaster = classMaster;
            this._TariffMaster = tariffMaster;

            this._GroupMaster = groupMaster;
            this._SubGroupMaster = subGroupMaster;
            this._ServiceMaster = serviceMaster;
            this._CompanyMaster = companyMaster;
            this._CompanyTypeMaster = companyTypeMaster;
            this._SubTpaCompanyMaster = subTpaCompanyMaster;
            this._ConsessionReasonMaster = consessionReasonMaster;
            this._BankMaster = bankMaster;


        }

        //CashCountertMaster Save and Update
        [HttpPost("CashCounterSave")]
        public IActionResult CashCounterSave(CashCounterMasterParams CashCounterMasterParams)
        {
            var ServiceSave = _CashCounterMaster.Save(CashCounterMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CashCounterUpdate")]
        public IActionResult CashCounterUpdate(CashCounterMasterParams CashCounterMasterParams)
        {
            var ServiceSave = _CashCounterMaster.Update(CashCounterMasterParams);
            return Ok(ServiceSave);
        }
        //ClassMaster Save and Update
        [HttpPost("ClassSave")]
        public IActionResult ClassSave(ClassMasterParams ClassMasterParams)
        {
            var ServiceSave = _ClassMaster.Save(ClassMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("ClassUpdate")]
        public IActionResult ClassUpdate(ClassMasterParams ClassMasterParams)
        {
            var ServiceSave = _ClassMaster.Update(ClassMasterParams);
            return Ok(ServiceSave);
        }

        //TeriffMaster Save and Update
        [HttpPost("TeriffSave")]
        public IActionResult TeriffSave(TariffMasterParams TariffMasterParams)
        {
            var ServiceSave = _TariffMaster.Save(TariffMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("TariffUpdate")]
        public IActionResult WardUpdate(TariffMasterParams TariffMasterParams)
        {
            var ServiceSave = _TariffMaster.Update(TariffMasterParams);
            return Ok(ServiceSave);
        }
        //GroupMaster Save and Update
        [HttpPost("GroupSave")]
        public IActionResult GroupSave(GroupMasterParams GroupMasterParams)
        {
            var ServiceSave = _GroupMaster.Save(GroupMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("GroupUpdate")]
        public IActionResult BedUpdate(GroupMasterParams GroupMasterParams)
        {
            var ServiceSave = _GroupMaster.Update(GroupMasterParams);
            return Ok(ServiceSave);
        }

        //SubGroupMaster Save and Update
        [HttpPost("SubGroupSave")]
        public IActionResult SubGroupSave(SubGroupMasterParams SubGroupMasterParams)
        {
            var ServiceSave = _SubGroupMaster.Save(SubGroupMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("SubGroupUpdate")]
        public IActionResult SubGroupUpdate(SubGroupMasterParams SubGroupMasterParams)
        {
            var ServiceSave = _SubGroupMaster.Update(SubGroupMasterParams);
            return Ok(ServiceSave);
        }
       
        //CashCountertMaster Save and Update
        [HttpPost("ServiceSave")]
        public IActionResult ServiceSave(ServiceMasterParams ServiceMasterParams)
        {
            var ServiceSave = _ServiceMaster.Save(ServiceMasterParams);
            return Ok(ServiceSave);
        }
        
        [HttpPost("ServiceUpdate")]
        public IActionResult ServiceUpdate(ServiceMasterParams ServiceMasterParams)
        {
            var ServiceSave = _ServiceMaster.Update(ServiceMasterParams);
            return Ok(ServiceSave);
        }
        //CompanyTypeMaster Save and Update
        
        [HttpPost("CompanyTypeMasterSave")]
        public IActionResult CompanyTypeMasterSave(CompanyTypeMasterParams CompanyTypeMasterParams)
        {
            var ServiceSave = _CompanyTypeMaster.Save(CompanyTypeMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CompanyTypeMasterUpdate")]
        public IActionResult CompanyTypeMasterUpdate(CompanyTypeMasterParams CompanyTypeMasterParams)
        {
            var ServiceSave = _CompanyTypeMaster.Update(CompanyTypeMasterParams);
            return Ok(ServiceSave);
        }
        
        //CompanyMaster Save and Update
        [HttpPost("CompanySave")]
        public IActionResult CompanySave(CompanyMasterParams CompanyMasterParams)
        {
            var ServiceSave = _CompanyMaster.Save(CompanyMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("CompanyUpdate")]
        public IActionResult CompanyUpdate(CompanyMasterParams CompanyMasterParams)
        {
            var ServiceSave = _CompanyMaster.Update(CompanyMasterParams);
            return Ok(ServiceSave);
        }
        //SubTpaCompanyMaster Save and Update
        [HttpPost("SubTpaCompanySave")]
        public IActionResult SubTpaCompanySave(SubTpaCompanyMasterParams SubTpaCompanyMasterParams)
        {
            var ServiceSave = _SubTpaCompanyMaster.Save(SubTpaCompanyMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("SubTpaCompanyUpdate")]
        public IActionResult SubTpaCompanyUpdate(SubTpaCompanyMasterParams SubTpaCompanyMasterParams)
        {
            var ServiceSave = _SubTpaCompanyMaster.Update(SubTpaCompanyMasterParams);
            return Ok(ServiceSave);
        }

        //ConsessionReasonMaster Save and Update
        [HttpPost("ConsessionReasonSave")]
        public IActionResult ConsessionReasonSave(ConsessionReasonMasterParams ConsessionReasonMasterParams)
        {
            var ServiceSave = _ConsessionReasonMaster.Save(ConsessionReasonMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("ConsessionReasonUpdate")]
        public IActionResult ConsessionReasonUpdate(ConsessionReasonMasterParams ConsessionReasonMasterParams)
        {
            var ServiceSave = _ConsessionReasonMaster.Update(ConsessionReasonMasterParams);
            return Ok(ServiceSave);
        }
        ////Bank Master Save and Update
        [HttpPost("BankMasterSave")]
        public IActionResult BankMasterSave(BankMasterParams BankMasterParams)
        {
            var ServiceSave = _BankMaster.Save(BankMasterParams);
            return Ok(ServiceSave);
        }

        [HttpPost("BankUpdate")]
        public IActionResult BankUpdate(BankMasterParams BankMasterParams)
        {
            var ServiceSave = _BankMaster.Update(BankMasterParams);
            return Ok(ServiceSave);
        }




    }

 }

