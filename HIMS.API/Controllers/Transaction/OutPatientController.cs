﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using HIMS.Data.Opd.OP;
using HIMS.Model.Opd.OP;
using HIMS.Model.IPD;
using HIMS.Data.IPD;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class OutPatientController : Controller
    {
        /* public IActionResult Index()
         {
             return View();
         }*/

       
      //  public readonly I_SaveAppointmentNewPatient _SaveAppointmentNewPatient;
        public readonly I_PhoneAppointment _PhoneAppointment;
        public readonly I_Payment _Payment;
        public readonly I_OpdAppointment _OpdAppointment;
        public readonly I_OPDPrescription _OPDPrescription;

        public readonly I_OpdCasePaper _OpdCasePaperRep;
        public readonly I_OpdAppointmentList _OpdAppointmentList;
        public readonly I_OpdBrowseList _OpdBrowseList;
        public readonly I_OPDRegistration _OPDRegistration;
        public readonly I_OPRefundBill _OPRefundBill;
        public readonly I_SS_RoleTemplateMaster i_SS_RoleTemplate;
        public readonly I_OPbilling _OPbilling;
        public readonly I_CasePaperPrescription _CasePaperPrescription;
      //  public readonly I_OPAddCharges _OPAddCharges;
        public readonly I_OPAddCharges _OPDAddCharges;
        //public readonly I_OPAdvance _OPAdvance;
        public readonly I_Emailconfiguration _Emailconfiguration;
        public readonly I_DynamicExecuteSchedule _DynamicExecuteSchedule;
        public readonly I_Configsetting _Configsetting;
        public readonly I_EmailNotification _EmailNotification;
        public readonly I_IP_SMSOutgoing _IP_SMSOutgoing;
        public readonly I_OPBillingCredit _OPBillingCredit;
        public readonly I_OPSettlemtCredit _OPSettlemtCredit;


        public OutPatientController(
            //  I_SaveAppointmentNewPatient saveAppointmentNewPatient,
            I_PhoneAppointment phoneAppointment,
            I_Payment payment,
            I_OpdAppointment opdAppointment
            , I_OPDPrescription oPDPrescription,
            I_OpdCasePaper opdCasePaper,
            I_OpdAppointmentList opdAppointmentList,
            I_OpdBrowseList opdBrowseList,
            I_OPDRegistration oPDRegistration,
            I_OPRefundBill oprefundbill,
            //I_Dashboard Dashboard,
            I_SS_RoleTemplateMaster roleTemplateMaster,
            I_OPbilling oPbilling,
            I_CasePaperPrescription casePaperPrescription,
            I_OPAddCharges oPAddCharges,
            ////I_OPAdvance oPAdvance
            I_Emailconfiguration emailconfiguration,
           I_DynamicExecuteSchedule dynamicExecuteSchedule,
           I_Configsetting configsetting,
            I_OPAddCharges oPAddCharges1,
            I_EmailNotification emailNotification,
            I_OPBillingCredit oPBillingCredit,I_OPSettlemtCredit oPSettlemtCredit, I_IP_SMSOutgoing iP_SMSOutgoing


            )
       {
             
          //  this._SaveAppointmentNewPatient=saveAppointmentNewPatient;
            this._PhoneAppointment = phoneAppointment;
            this._Payment = payment;
            this._OpdAppointment = opdAppointment;
            this._OPDPrescription = oPDPrescription;
            this._OpdCasePaperRep = opdCasePaper;
            this._OpdAppointmentList = opdAppointmentList;
            this._OpdBrowseList = opdBrowseList;
            this._OPDRegistration = oPDRegistration;
            this._OPRefundBill = oprefundbill;
            this.i_SS_RoleTemplate = roleTemplateMaster;
            this._OPbilling = oPbilling;
            this._CasePaperPrescription = casePaperPrescription;
            this._OPDAddCharges = oPAddCharges1;
            //this._OPAdvance = oPAdvance;
            this._Emailconfiguration = emailconfiguration;
            this._DynamicExecuteSchedule = dynamicExecuteSchedule;
            this._Configsetting = configsetting;
            this._EmailNotification = emailNotification;
            // this._OPDAddCharges = _OPAddCharges;
            this._OPBillingCredit = oPBillingCredit;
            this._OPSettlemtCredit = oPSettlemtCredit;
            this._IP_SMSOutgoing = iP_SMSOutgoing;
        }

        //OPDAppointment Insert 
        [HttpPost("OPDAppointmentInsert")]
        public IActionResult OPDAppointmentInsert(OpdAppointmentParams OpdAppointmentParams)
        {
            var appoSave = _OpdAppointment.Save(OpdAppointmentParams);
            return Ok(appoSave);
            //this._OPRefundBill = oprefundbill;
        }

        //OPDAppointment Update 
        [HttpPost("OPDAppointmentUpdate")]
        public IActionResult OPDAppointmentUpdate(OpdAppointmentParams OpdAppointmentParams)
        {
            var appoSave = _OpdAppointment.Update(OpdAppointmentParams);
            return Ok(appoSave);
            //this._OPRefundBill = oprefundbill;
        }

      /*  //saveAppointmentNewPatient Insert 
        [HttpPost("AppointmentNewPatient")]
        public IActionResult saveAppointmentNewPatient(SaveAppointmentNewPatientParams SaveAppointmentNewPatientParams)
        {
            var appoSave = _SaveAppointmentNewPatient.RegistrationInsert(SaveAppointmentNewPatientParams);
         
            return Ok(appoSave);
            
        }
        */
        //PhoneAppointment Insert 
        [HttpPost("PhoneAppointmentInsert")]
        public IActionResult PhoneAppointmentInsert(PhoneAppointmentParams PhoneAppointmentParams)
        {
            var appoSave = _PhoneAppointment.Save(PhoneAppointmentParams);
            return Ok(appoSave);
        }
        //PhoneAppointment Update 
        [HttpPost("PhoneAppointmentCancle")]
        public IActionResult PhoneAppointmentCancle(PhoneAppointmentParams PhoneAppointmentParams)
        {
            var appoSave = _PhoneAppointment.Cancle(PhoneAppointmentParams);
            return Ok(appoSave);
        }


        //OPDRegistration
        [HttpPost("OPDRegistrationSave")]
        public IActionResult OPDRegistrationSave(OPDRegistrationParams OPDRegistrationParams)
        {
            var CasePaperSave = _OPDRegistration.Insert(OPDRegistrationParams);
            return Ok(CasePaperSave);

        }

        //-------------------------------------------------
        [HttpPost("OPDRegistrationUpdate")]
        public IActionResult OPDRegistrationUpdate(OPDRegistrationParams OPDRegistrationParams)
        {
            var CasePaperSave = _OPDRegistration.Update(OPDRegistrationParams);
            return Ok(CasePaperSave);

        }

        //OPD Appointment
        [HttpPost("OPDAppointmentSave")]
        public IActionResult OPDAppointment(OpdAppointmentParams OpdAppointmentParams)
        {
            var CasePaperSave = _OpdAppointment.Save(OpdAppointmentParams);
            return Ok(CasePaperSave);

        }

        //-------------------------------------------------
        [HttpPost("CasePaperSave")]
        public IActionResult CasePaperSave(OpdCasePaperParams opdCasePaperParams)
        {
            var CasePaperSave = _OpdCasePaperRep.Insert(opdCasePaperParams);
            return Ok(CasePaperSave);

        }

        /* [HttpPost("CasePaperUpdate")]
         public IActionResult CasePaperUpdate(OpdCasePaperParams opdCasePaperParams)
         {
             var CasePaperUpdate = _OpdCasePaperRep.Update(opdCasePaperParams);
             return Ok(CasePaperUpdate);
         }*/

        [HttpPost("CasePaperPrescriptionSave")]
        public IActionResult CasePaperPrescriptionSave(CasePaperPrescriptionParams CasePaperPrescriptionParams)
        {
            var CasePaperSave = _CasePaperPrescription.Insert(CasePaperPrescriptionParams);
            return Ok(CasePaperSave);

        }

        //[HttpPost("OPAdvance")]
        //public IActionResult OPAdvance(OPAdvanceParams OPAdvanceParams)
        //{
        //    var OPA = _OPAdvance.Insert(OPAdvanceParams);
        //    return Ok(OPA);

        //}

        //Payment Insert 
        [HttpPost("PaymentInsert")]
        public IActionResult PaymentInsert(PaymentParams PaymentParams)
        {
            var appoSave = _Payment.Save(PaymentParams);
            return Ok(appoSave);
        }
        //Payment Update 
        [HttpPost("PaymentUpdate")]
        public IActionResult PaymentUpdate(PaymentParams PaymentParams)
        {
            var appoSave = _Payment.Update(PaymentParams);
            return Ok(appoSave);
        }

        //Prescription Update 
        [HttpPost("PrescriptionInsert")]
        public IActionResult PrescriptionInsert(OPDPrescriptionParams OPDPrescriptionParams)
        {
            var appoSave = _OPDPrescription.Insert(OPDPrescriptionParams);
            return Ok(appoSave);
        }



        /*[HttpPost("OPDAppointmentList")]
        public IActionResult OPDAppointmentList(AppointmentList AppointmentList)
        {
            var CasePaperSave = _OpdAppointmentList.(AppointmentList);
            return Ok(CasePaperSave);

        }

        //-------------------------------------------------
        [HttpPost("OPDPhoneAppointment")]
        public IActionResult OPDPhoneAppointment(OpdPhoneAppointmentParams OpdPhoneAppointmentParams)
        {
            var phoneappSave = _OpdPhoneAppointment.Insert(OpdPhoneAppointmentParams);
            return Ok(phoneappSave);

        }

        [HttpPost("OPDPhoneAppointmentCancle")]
        public IActionResult OPDPhoneAppointmentCancle(OpdPhoneAppointmentParams OpdPhoneAppointmentParams)
        {
            var phoneappCancle = _OpdPhoneAppointment.Cancel(OpdPhoneAppointmentParams);
            return Ok(phoneappCancle);
        }
        */

        [HttpPost("OpdBrowseList")]
        public IActionResult OpdBrowseList(BrowseOPDBillParams BrowseOPDBillParams)
        {
            var OpdBrowseList = _OpdBrowseList.GetBrowseOPDBill(BrowseOPDBillParams);
            return Ok(OpdBrowseList);

        }

      /*  [HttpPost("OPDPaymentSave")]
        public IActionResult OPDPaymentSave(PaymentParams PaymentParams)
        {
            var OPDPaymentSave = _Payment.Save(PaymentParams);
            return Ok(OPDPaymentSave);
        }

        [HttpPost("OPDPaymentUpdate")]
        public IActionResult OPDPaymentUpdate(PaymentParams PaymentParams)
        {
            var OPDPaymentUpdate = _Payment.Update(PaymentParams);
            return Ok(OPDPaymentUpdate);
        }
      */
        [HttpPost("OPRefundBill")]
        public IActionResult OPRefundBill(OPRefundBillParams OPRBP)
        {
            var OPRBP1 = _OPRefundBill.Insert(OPRBP);
            return Ok(OPRBP1);
        }
        //SS_RoleTemplateMaster
        [HttpPost("OP_SS_RoleTemplateMasterSave")]
        public IActionResult OP_SS_RoleTemplateMasterSave(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams)
        {
            var SSR = i_SS_RoleTemplate.Save(SS_RoleTemplateMasterParams);
            return Ok(SSR);
        }

        [HttpPost("OP_SS_RoleTemplateMasterUpdate")]
        public IActionResult OP_SS_RoleTemplateMasterUpdate(SS_RoleTemplateMasterParams SS_RoleTemplateMasterParams)
        {
            var SSR = i_SS_RoleTemplate.Update(SS_RoleTemplateMasterParams);
            return Ok(SSR);
        }
        //OPBilling
      /*  [HttpPost("OPBilling")]
        public IActionResult OPBilling(OPbillingparams OPbillingparams)
        {
            var SSR = _OPbilling.Insert(OPbillingparams);
            return Ok(SSR);
        }*/


         [HttpPost("OPBilling")]
        public String OPBilling(OPbillingparams OPbillingparams)
        {
            var SSR = _OPbilling.Insert(OPbillingparams);
            return (SSR.ToString());
        }


        [HttpPost("OPBillingCredit")]
        public String OPBillingCredit(OPBillingCreditparam OPBillingCreditparam)
        {
            var SSR = _OPBillingCredit.Insert(OPBillingCreditparam);
            return (SSR.ToString());
        }
        //OPAddcharges
        [HttpPost("OPDAddCharges")]
        public IActionResult OPDAddCharges(OPDAddchargesparams OPDAddchargesparams)
        {
            var SSR = _OPDAddCharges.Save(OPDAddchargesparams);
            return Ok(SSR);
        }

        //EmainConfiguration
        [HttpPost("InsertEmailConfiguration")]
        public IActionResult InsertEmailConfiguration(Emailconfigurationparams Emailconfigurationparams)
        {
            var SSR = _Emailconfiguration.Insert(Emailconfigurationparams);
            return Ok(SSR);
        }

        //EmainConfiguration
        [HttpPost("UpdateEmailConfiguration")]
        public IActionResult UpdateEmailConfiguration(Emailconfigurationparams Emailconfigurationparams)
        {
            var SSR = _Emailconfiguration.Update(Emailconfigurationparams);
            return Ok(SSR);
        }

        //EmainConfiguration
        [HttpPost("InsertDynamicExecuteSchedule")]
        public IActionResult InsertDynamicExecuteSchedule(DynamicExecuteScheduleparam DynamicExecuteScheduleparam)
        {
            var SSR = _DynamicExecuteSchedule.Insert(DynamicExecuteScheduleparam);
            return Ok(SSR);
        }
        //EmainConfiguration
        [HttpPost("UpdateDynamicExecuteSchedule")]
        public IActionResult UpdateDynamicExecuteSchedule(DynamicExecuteScheduleparam DynamicExecuteScheduleparam)
        {
            var SSR = _DynamicExecuteSchedule.Update(DynamicExecuteScheduleparam);
            return Ok(SSR);
        }

        //ConfigurationSetting
        [HttpPut("UpdateConfigSetting")]
        public IActionResult UpdateConfigSetting(ConfigSettingparam ConfigSettingparam)
        {
            var SSR = _Configsetting.Update(ConfigSettingparam);
            return Ok(SSR);
        }

        //EmailNotificationInsert
        [HttpPost("EmailNotificationInsert")]
        public IActionResult EmailNotificationInsert(EmailNotificationParam EmailNotificationParam)
        {
            var SSR = _EmailNotification.Insert(EmailNotificationParam);
            return Ok(SSR);
        }


        //EmailNotificationInsert
        [HttpPost("SMSOutgoingInsert")]
        public IActionResult SMSOutgoingInsert(IPSMSOutgoingparams IPSMSOutgoingparams)
        {
            var SSR = _IP_SMSOutgoing.Insert(IPSMSOutgoingparams);
            return Ok(SSR);
        }
        [HttpPost("OpSettlement")]
        public IActionResult OpSettlement(OPSettlementCreditParam OPSettlementCreditParam)
        {
            var SSR = _OPSettlemtCredit.Insert(OPSettlementCreditParam);
            return Ok(SSR);
        }



    }

}
