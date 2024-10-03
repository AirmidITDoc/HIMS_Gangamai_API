using Microsoft.AspNetCore.Mvc;
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
using HIMS.Data.HomeTransaction;
using HIMS.Model.HomeTransaction;
using System.IO;
using HIMS.API.Utility;
using Microsoft.Extensions.Configuration;
using System.Data;
using HIMS.Common.Utility;

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
        public readonly I_PatientDocumentupload _PatientDocumentupload;
        public readonly I_PatientFeedback _PatientFeedback;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        private readonly IFileUtility _IFileUtility;
        private readonly I_CrossConsultation _CrossConsultation;
        public readonly IFileUtility _FileUtility;
        public readonly IConfiguration _configuration;
        public OutPatientController(
            I_PhoneAppointment phoneAppointment,
            I_Payment payment,
            I_OpdAppointment opdAppointment
            , I_OPDPrescription oPDPrescription,
            I_OpdCasePaper opdCasePaper,
            I_OpdAppointmentList opdAppointmentList,
            I_OpdBrowseList opdBrowseList,
            I_OPDRegistration oPDRegistration,
            I_OPRefundBill oprefundbill,
            I_SS_RoleTemplateMaster roleTemplateMaster,
            I_OPbilling oPbilling,
            I_CasePaperPrescription casePaperPrescription,
            I_OPAddCharges oPAddCharges,
            I_Emailconfiguration emailconfiguration,
            I_DynamicExecuteSchedule dynamicExecuteSchedule,
            I_Configsetting configsetting,
            I_OPAddCharges oPAddCharges1,
            I_EmailNotification emailNotification,
            I_OPBillingCredit oPBillingCredit, I_OPSettlemtCredit oPSettlemtCredit, I_IP_SMSOutgoing iP_SMSOutgoing, I_PatientDocumentupload patientDocumentupload
            , I_PatientFeedback patientFeedback, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility
            , IFileUtility fileUtility, I_CrossConsultation crossConsultation,IFileUtility FileUtility,
            IConfiguration configuration

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
            this._PatientDocumentupload = patientDocumentupload;
            this._PatientFeedback = patientFeedback;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _IFileUtility = fileUtility;
            _CrossConsultation = crossConsultation;
            _IFileUtility = fileUtility;
            _configuration = configuration;
        }


        //OPDAppointment Insert 
        [HttpPost("PatientFeedback")]
        public IActionResult PatientFeedback(PatientFeedbackParams patientFeedbackParams)
        {
            var PatientInsert = _PatientFeedback.Insert(patientFeedbackParams);
            return Ok(PatientInsert);
        }

        //OPDAppointment Insert 
        [HttpPost("OPDAppointmentWithPhotoInsert")]
        public async Task<IActionResult> OPDAppointmentInsertwithPhotoAsync([FromForm] OpdAppointmentParams OpdAppointmentParams)
        {
            if (OpdAppointmentParams.RegistrationSavewithPhoto.ImgFile != null)
            {
                string NewFileName = Guid.NewGuid().ToString();
                string FileName = await _IFileUtility.UploadDocument(OpdAppointmentParams.RegistrationSavewithPhoto.ImgFile, "PatientPhoto", NewFileName);
                // OpdAppointmentParams.RegistrationSave.Photo = FileName;
            }
            else
            {
                // OpdAppointmentParams.RegistrationSave.Photo = null;
            }
            var appoSave = _OpdAppointment.Save(OpdAppointmentParams);
            return Ok(appoSave);
        }


        //OPDAppointment Insert 
        [HttpPost("AppointmentInsert")]
        public IActionResult AppointmentInsert(OpdAppointmentParams OpdAppointmentParams)
        {
            var appoSave = _OpdAppointment.Save(OpdAppointmentParams);
            return Ok(appoSave);
        }

        //OPDAppointment Update 
        [HttpPost("AppointmentVisitUpdate")]
        public IActionResult AppointmentVisitUpdate(OpdAppointmentParams OpdAppointmentParams)
        {
            var appoSave = _OpdAppointment.Update(OpdAppointmentParams);
            return Ok(appoSave);
        }


        //OPDAppointment Cancle 
        [HttpPost("AppointmentCancle")]
        public IActionResult AppointmentCancle(OpdAppointmentParams OpdAppointmentParams)
        {
            var appoSave = _OpdAppointment.AppointmentCancle(OpdAppointmentParams);
            return Ok(appoSave);
        }

        [HttpGet("view-PatientAppointment")]
        public IActionResult ViewPatientAppointment(int VisitId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPCasePaperNew.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OpdAppointment.ViewOppatientAppointmentdetailsReceipt(VisitId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPAppointmentDetails", "AppointmentofOPPatient"+ VisitId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp
            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-AppointmentTemplate")]
        public IActionResult viewAppoinmentTemplate(int VisitId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CommanTemplate.html");
            
            // Hospital Header 
            string Hospitalheader = _pdfUtility.GetHeader(6, 1);// hospital header
            Hospitalheader = Hospitalheader.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));
            
            //Report content
            string header1 = _pdfUtility.GetTemplateHeader(1);// Appointment header
            header1 = header1.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));
          
            DataTable dt = _OpdAppointment.GetDataForReport(VisitId);
            var html = _OpdAppointment.ViewAppointmentTemplate(dt, htmlFilePath, header1);
            html = html.Replace("{{NewHeader}}", Hospitalheader);

            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AppointmentPrint", "Appointment_"+ VisitId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        //OPDCrossConsultation Insert 
        [HttpPost("OPDCrossConsultationInsert")]
        public IActionResult OPDCrossconsultationInsert(CrossConsultation CrossConsultation)
        {

            var Id = _CrossConsultation.Save(CrossConsultation);
            return Ok(Id);
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


        // Document Upload
        /*  [HttpPost("DocumentuploadSave")]
          public IActionResult DocumentuploadSave(PatientDocumentuploadParam PatientDocumentuploadParam)
          {
              var CasePaperSave = _PatientDocumentupload.Save(PatientDocumentuploadParam);
              return Ok(CasePaperSave);

          }


          //OPD upload
          [HttpPost("DocumentuploadUpdate")]
          public IActionResult DocumentuploadUpdate(PatientDocumentuploadParam PatientDocumentuploadParam)
          {
              var CasePaperSave = _PatientDocumentupload.Update(PatientDocumentuploadParam);
              return Ok(CasePaperSave);

          }*/

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


        [HttpGet("view-OP-PaymentReceipt")]
        public IActionResult ViewOPPaymentReceipt(int PaymentId)
        {
            //OPPaymentReceipt
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Payment.ViewOPPaymentReceipt(PaymentId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPPaymentReceipt", "OPPaymentReceipt"+ PaymentId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        //New Prescription Insert 
        [HttpPost("PrescriptionInsert")]
        public IActionResult PrescriptionInsert(OPDPrescriptionParams OPDPrescriptionParams)
        {
            var appoSave = _OPDPrescription.Insert(OPDPrescriptionParams);
            return Ok(appoSave);
        }



        [HttpGet("view-OP_Prescription")]
        public IActionResult ViewOPPrescription(int VisitId)
        {
            //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescription.html");
            //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            //var html = _OPDPrescription.ViewOPPrescriptionReceipt(VisitId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPPrescription", "OPPrescription" + VisitId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });



            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescription.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");

            DataTable dt = _OPDPrescription.GetDataForReport(VisitId);
            var html = _OPDPrescription.ViewOPPrescriptionReceipt(dt, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            //var signature = _FileUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());

            //html = html.Replace("{{Signature}}", signature);

            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPPrescription", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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

        [HttpGet("view-OPRefundofBill")]
        public IActionResult ViewOPRefundofbill(int RefundId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPRefundofbill.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPRefundBill.ViewOPRefundofBillReceipt(RefundId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPRefundofbill", "OPRefundofbill"+ RefundId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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


        [HttpGet("view-Op-BillReceipt")]
        public IActionResult ViewOpBillReceipt(int BillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPBillReceipt(BillNo, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OpBillingReceipt", "OpBillingReceipt" + BillNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        //[HttpGet("view-OPD-daily-collection")]
        //public IActionResult ViewOPDDailyCollection(DateTime FromDate, DateTime ToDate, int AddedById)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPDDailyCollection.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
        //    var html = _OPbilling.ViewOPBillDailyReportReceipt(FromDate, ToDate, AddedById, htmlFilePath, htmlHeaderFilePath);
        //    // var html = _Sales.ViewDailyCollection(FromDate, ToDate, StoreId, AddedById, htmlFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDDailyCollection", "OPDDailyCollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

        //    // write logic for send pdf in whatsapp


        //    //if (System.IO.File.Exists(tuple.Item2))
        //    //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        //}
        [HttpPost("OPBillWithPayment")]
        public String OPBilling(OPbillingparams OPbillingparams)
        {
            var SSR = _OPbilling.Insert(OPbillingparams);
            return (SSR.ToString());
        }
        [HttpPost("OPBillingWithCredit")]
        public String OPBillingCredit(OPBillingCreditparam OPBillingCreditparam)
        {
            var SSR = _OPBillingCredit.Insert(OPBillingCreditparam);
            return (SSR.ToString());
        }

        [HttpPost("OPBillWithPaymentCashCounter")]
        public String OPBillWithPaymentCashCounter(OPbillingparams OPbillingparams)
        {
            var SSR = _OPbilling.InsertCashCounter(OPbillingparams);
            return (SSR.ToString());
        }

        [HttpPost("OPBillingWithCreditCashCounter")]
        public String OPBillingWithCreditCashCounter(OPBillingCreditparam OPBillingCreditparam)
        {
            var SSR = _OPBillingCredit.InsertCreditCashCounter(OPBillingCreditparam);
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
