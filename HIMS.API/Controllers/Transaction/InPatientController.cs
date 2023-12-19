using Microsoft.AspNetCore.Mvc;
using HIMS.Data.IPD;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using HIMS.Data.Pharmacy;
using Microsoft.Extensions.Configuration;
using HIMS.API.Utility;
using HIMS.Common.Utility;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class InPatientController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IFileUtility _IFileUtility;
        public readonly I_AdmissionReg _AdmissionReg;
        public readonly I_RegisteredPatientAdmission _RPA;
        public readonly I_IPDischarge _IPDischarge;
        public readonly I_IPDDischargeSummary _IPDDischargeSummary;
        public readonly I_IPRefundofAdvance _IPRefundofAdvance;
        public readonly I_IPRefundofBilll _IPRefundofBilll;
        public readonly I_IPBilling _IPBilling;
        public readonly I_IPLabrequestChange _IPLabrequestChange;
        public readonly I_IPInterimBill _IPInterimBill;
        public readonly I_IPDEmergency _IPDEmergency;
        public readonly I_MLCInfo _MLCInfo;
        public readonly I_IPPrescriptionReturn _IPPrescriptionReturn;
        public readonly I_pathologyReportDetail _PathologyReportDetail;
        public readonly I_PathologySampleCollection _PathologySampleCollection;
        public readonly I_IPAdvance _IPAdvance;
        public readonly I_IPAdvanceUpdate _IPAdvanceUpdate;
        public readonly I_Addcharges _Addcharges;
        // public readonly I_Addcharges _Addcharges;
        //public readonly I_IPBilling _IPBilling;
        //public readonly I_IPInterimBill _IPInterimBill;
        public readonly I_BedTransfer _BedTransfer;
        public readonly I_Admission _Admission;
        public readonly I_RegisteredPatientAdmission _RegisteredPatientAdmission1;

        public readonly I_InsertIPDraft _InsertIPDraft;
        public readonly I_IPPathOrRadiRequest _IPPathOrRadiRequest;

        public readonly I_IPBillingwithcredit _IPBillingwithcredit;

        public readonly I_Payment _Payment;
        public readonly I_IPBillEdit _IPBillEdit;

        public readonly I_IPAdvanceEdit _IPAdvanceEdit;
        public readonly I_IP_Settlement_Process _IP_Settlement_Process;
        public readonly I_DocumentAttachment _DocumentAttachment;
        public readonly I_WhatsappSms _WhatsappSms;
        public readonly I_IP_SMSOutgoing _IP_SMSOutgoing;
        public readonly I_OTTableDetail _OTTableDetail;
        public readonly I_OTBookingDetail _OTBookingDetail;
        public readonly I_CathLabBookingDetail _CathLabBookingDetail;
        public readonly I_OTEndoscopy _OTEndoscopy;

        public readonly I_IPPrescription _IPPrescription;
        public readonly I_OTRequest _OTRequest;
        public readonly I_OTNotesTemplate _OTNotesTemplate;
        public readonly I_MaterialConsumption _MaterialConsumption;
        public readonly I_NeroSurgeryOTNotes _NeroSurgeryOTNotes;
        public readonly I_DoctorNote _DoctorNote;
        public readonly I_NursingTemplate _NursingTemplate;
        public readonly I_Mrdmedicalcertificate _Mrdmedicalcertificate;
        public readonly I_SubcompanyTPA _SubcompanyTPA;
        public readonly I_Mrddeathcertificate _Mrddeathcertificate;
        public readonly I_Prepostopnote _Prepostopnote;
        public InPatientController(
            IWebHostEnvironment environment,
            IFileUtility fileUtility,
            I_AdmissionReg admission,
            I_RegisteredPatientAdmission rpa,
            I_IPDischarge iPDischarge,
            I_IPDDischargeSummary iPDDischargeSummary,
            I_IPRefundofAdvance iPRefundofAdvance,
            I_IPRefundofBilll iPRefundofBilll,
            I_IPBilling ipbilling,
            I_IPInterimBill ipinterimbill,
            I_IPDEmergency iPDEmergency,
            I_MLCInfo mLCInfo,
            I_IPPrescriptionReturn iPPrescriptionReturn,
            I_pathologyReportDetail pathologyReportDetail,
            I_IPAdvance ipAdvance,
            I_IPAdvanceUpdate ipAdvanceUpdate,
            I_PathologySampleCollection pathologySampleCollection,
            I_Addcharges addcharges, I_RegisteredPatientAdmission registeredPatientAdmission,
            I_BedTransfer bedTransfer,
            I_Admission admission1,
            I_RegisteredPatientAdmission _RegisteredPatientAdmission,
            I_IPLabrequestChange labrequestChange, I_InsertIPDraft insertIPDraft,
            I_IPPathOrRadiRequest iPPathOrRadiRequest,
            I_IPBillingwithcredit iPBillingwithcredit,
            I_Payment payment, I_IPBillEdit iPBillEdit, I_IPAdvanceEdit iPAdvanceEdit, I_IP_Settlement_Process iP_Settlement_Process,
            I_DocumentAttachment documentAttachment, I_IP_SMSOutgoing iP_SMSOutgoing, I_OTTableDetail oTTableDetail, I_OTBookingDetail oTBookingDetail, I_CathLabBookingDetail cathLabBookingDetail
            , I_IPPrescription iPPrescription, I_OTEndoscopy oTEndoscopy, I_OTRequest oTRequest, I_OTNotesTemplate oTNotesTemplate, I_MaterialConsumption materialConsumption
            , I_NeroSurgeryOTNotes neroSurgeryOTNotes, I_DoctorNote doctorNote, I_NursingTemplate nursingTemplate, I_Mrdmedicalcertificate mrdmedicalcertificate,
            I_Mrddeathcertificate mrddeathcertificate, I_SubcompanyTPA subcompanyTPA, I_Prepostopnote prepostopnote, I_WhatsappSms whatsappSms
            )
        {
            this._environment = environment;
            this._AdmissionReg = admission;
            this._RPA = rpa;
            this._IPDischarge = iPDischarge;
            this._IPDDischargeSummary = iPDDischargeSummary;
            this._IPRefundofAdvance = iPRefundofAdvance;
            this._IPRefundofBilll = iPRefundofBilll;
            this._IPBilling = ipbilling;
            this._IPInterimBill = ipinterimbill;
            this._IPDEmergency = iPDEmergency;
            this._MLCInfo = mLCInfo;
            this._IPPrescriptionReturn = iPPrescriptionReturn;
            this._PathologyReportDetail = pathologyReportDetail;
            this._PathologySampleCollection = pathologySampleCollection;
            this._Addcharges = addcharges;
            this._RPA = registeredPatientAdmission;
            this._IPAdvance = ipAdvance;
            //this._IPAdvance = ipAdvance;
            this._IPAdvanceUpdate = ipAdvanceUpdate;
            this._Admission = admission1;
            this._RegisteredPatientAdmission1 = registeredPatientAdmission;
            this._IPLabrequestChange = labrequestChange;
            this._InsertIPDraft = insertIPDraft;
            this._IPPathOrRadiRequest = iPPathOrRadiRequest;
            this._IPBillingwithcredit = iPBillingwithcredit;
            this._Payment = payment;
            this._IPBillEdit = iPBillEdit;
            this._IPAdvanceEdit = iPAdvanceEdit;
            this._IP_Settlement_Process = iP_Settlement_Process;
            this._DocumentAttachment = documentAttachment;
            this._IP_SMSOutgoing = iP_SMSOutgoing;
            this._OTTableDetail = oTTableDetail;
            this._OTBookingDetail = oTBookingDetail;
            this._CathLabBookingDetail = cathLabBookingDetail;
            this._OTEndoscopy = oTEndoscopy;
            this._OTRequest = oTRequest;
            this._IPPrescription = iPPrescription;
            this._OTNotesTemplate = oTNotesTemplate;
            this._MaterialConsumption = materialConsumption;
            this._NeroSurgeryOTNotes = neroSurgeryOTNotes;
            this._DoctorNote = doctorNote;
            this._NursingTemplate = nursingTemplate;
            this._Mrdmedicalcertificate = mrdmedicalcertificate;
            this._Mrddeathcertificate = mrddeathcertificate;
            this._SubcompanyTPA = subcompanyTPA;
            this._Prepostopnote = prepostopnote;
            this._IFileUtility = fileUtility;
            this._WhatsappSms = whatsappSms;
        }

        //New AdmissionSave

        [HttpPost("NewAdmissionSave")]
        public IActionResult NewAdmissionSave(AdmissionParams admissionParams)
        {
            var AdmissionS = _Admission.Insert(admissionParams);
            return Ok(AdmissionS);
        }

        [HttpPost("NewAdmissionUpdate")]
        public IActionResult NewAdmissionUpdate(AdmissionParams admissionParams)
        {
            var AdmissionS = _Admission.Update(admissionParams);
            return Ok(AdmissionS);
        }

        [HttpPost("RegisteredAdmissionSave")]
        public IActionResult AdmissionSave(RegisteredPatientAdmissionParams RegisteredPatientAdmissionParams)
        {
            var regAdmissionS = _RegisteredPatientAdmission1.Update(RegisteredPatientAdmissionParams);
            return Ok(regAdmissionS);
        }

        [HttpPost("UpdateRegisteredPatientAdmission")]
        public IActionResult RegisteredPatientAdmission(RegisteredPatientAdmissionParams RegisteredPatientAdmissionParams)
        {
            var AdmissionS = _RPA.Update(RegisteredPatientAdmissionParams);
            return Ok(AdmissionS);
        }

        [HttpPost("AddIPCharges")]
        public IActionResult AddIPCharges(AddChargesParams addChargesParams)
        {
            var RPAP = _Addcharges.Save(addChargesParams);
            return Ok(RPAP);
        }

        [HttpPost("InsertIPDischarge")]

        public String InsertIPDischarge(IPDischargeParams IPDischargeParams)
        {
            var IPD = _IPDischarge.Insert(IPDischargeParams);
            return (IPD.ToString());
        }

        [HttpPost("UpdateIPDischarge")]

        public bool UpdateIPDischarge(IPDischargeParams IPDischargeParams)
        {
            var IPD = _IPDischarge.Update(IPDischargeParams);
            return (true);
        }
        [HttpPost("InsertIPDischargeSummary")]

        public String InsertIPDischargeSummary(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
            var IPD = _IPDDischargeSummary.Insert(IPDDischargeSummaryParams);
            return (IPD.ToString());
        }
        [HttpPost("UpdateIPDischargeSummary")]

        public IActionResult UpdateIPDischargeSummary(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
            var IPD = _IPDDischargeSummary.Update(IPDDischargeSummaryParams);
            return Ok(IPD);
        }


        [HttpPost("InsertIPRefundofAdvance")]

        public String InsertIPRefundofAdvance(IPRefundofAdvanceParams IPRefundofAdvanceParams)
        {
            var IPD = _IPRefundofAdvance.Insert(IPRefundofAdvanceParams);
            return (IPD.ToString());

        }

        [HttpPost("InsertIPRefundofBill")]

        public String InsertIPRefundofBill(IPRefundofBilllparams IPRefundofBillParams)
        {
            var IPD = _IPRefundofBilll.Insert(IPRefundofBillParams);
            return (IPD.ToString());

        }

        [HttpPost("InsertIPPrescription")]

        public String InsertIPPrescription(IPPrescriptionParams IPPrescriptionParams)
        {
            var IPD = _IPPrescription.Insert(IPPrescriptionParams);
            return (IPD.ToString());

        }

        [HttpPost("InsertIPPrescriptionReturn")]

        public IActionResult InsertIPPrescriptionReturn(IPPrescriptionReturnParams IPPrescriptionReturnParams)
        {
            var IPD = _IPPrescriptionReturn.Insert(IPPrescriptionReturnParams);
            return Ok(IPD);

        }

        [HttpPost("IPDEmergencyRegInsert")]

        public IActionResult IPDEmergencyRegInsert(IPDEmergencyParams IPDEmergencyParams)
        {
            var IPE = _IPDEmergency.Insert(IPDEmergencyParams);
            return Ok(IPE);

        }

        [HttpPost("IPDPathologyReportDetailsInsert")]

        public IActionResult IPDPathologyReportDetailsInsert(pathologyReportDetailParams pathologyReportDetailParams)
        {
            var IPE = _PathologyReportDetail.Insert(pathologyReportDetailParams);
            return Ok(IPE);

        }

        [HttpPost("IPDPathologySampleCollectionUpdate")]

        public IActionResult IPDPathologySampleCollectionUpdate(PathologySampleCollectionParams PathologySampleCollectionParams)
        {
            var IPS = _PathologySampleCollection.Update(PathologySampleCollectionParams);
            return Ok(IPS);

        }

        [HttpPost("IPBillingInsert")]

        public String IPBillingInsert(IPBillingParams IPBP)
        {
            var IPBill = _IPBilling.Insert(IPBP);
            return (IPBill.ToString());
        }

        [HttpPut("IPBillingEditProcess")]

        public IActionResult IPBillingEditProcess(IPBillEditparam IPBP)
        {
            var IPBill = _IPBillEdit.Update(IPBP);
            return Ok(IPBill);
        }

        [HttpPost("IPBillingCreditInsert")]

        public IActionResult IPBillingCreditInsert(IPBillingwithcreditparams IPBP)
        {
            var IPBill = _IPBillingwithcredit.Insert(IPBP);
            return Ok(IPBill);
        }


        [HttpPost("Credit_Payment")]

        public String Credit_Payment(PaymentParams IPBP)
        {
            var IPBill = _Payment.Save(IPBP);
            return (IPBill.ToString());
        }

        [HttpPost("IPLabRequestChangesInsert")]

        public IActionResult IPLabRequestChangesInsert(IPLabrequestChangeParam IPLabrequestChangeParam)
        {
            var IPBill = _IPLabrequestChange.Insert(IPLabrequestChangeParam);
            return Ok(IPBill);
        }


        [HttpPost("IPInterimBillInsert")]

        public IActionResult IPInterimBillInsert(IPInterimBillParams IPBP)
        {
            var IPIBP = _IPInterimBill.Insert(IPBP);
            return Ok(IPIBP);
        }

        /*   [HttpPost("IPAdvance")]
           public IActionResult IPAdvance(IPAdvanceParams IPA)
           {
               var IPAP = _IPAdvance.Insert(IPA);
               return Ok(IPAP);
           }*/

        [HttpPost("IPAdvance")]
        public String IPAdvance(IPAdvanceParams IPAdvanceParams)
        {
            var AdvId = _IPAdvance.Insert(IPAdvanceParams);
            return (AdvId.ToString());
        }

        [HttpPut("IPAdvanceEditProcess")]

        public IActionResult IPAdvanceEditProcess(IPAdvaceEditparam IPA)
        {
            var IPAP = _IPAdvanceEdit.Update(IPA);
            return Ok(IPAP);
        }


        [HttpPost("IPAdvanceUpdate")]
        public IActionResult IPAdvanceUpdate(IPAdvanceUpdateParams iPAdvanceUpdateParams)
        {
            var AdvUpdate = _IPAdvanceUpdate.Insert(iPAdvanceUpdateParams);
            return Ok(AdvUpdate);
        }

        //[HttpPost("IPPrescriptionInsert")]
        //public IActionResult IPPrescriptionInsert(IPPrescriptionParams IPPR)
        //{
        //    var IPPIR = _IPPrescription.Insert(IPPR);
        //    return Ok(IPPIR);
        //}

        //[HttpPost("IP_Charges_Delete")]
        //public IActionResult IP_Charges_Delete(IP_Charges_DeleteParams IPCDR)
        //{
        //    var IPCR = _IP_Charges_Delete.Update(IPCDR);
        //    return Ok(IPCR);
        //}



        [HttpPost("IPDBedTransfer")]

        public IActionResult IPDBedTransfer(BedTransferParams BedTransferParams)
        {
            var Bed = _BedTransfer.Update(BedTransferParams);
            return Ok(Bed);
        }

        [HttpPost("InsertIPDraftBill")]

        public IActionResult InsertIPDraft(InsertIPDraftParams InsertIPDraftParams)
        {
            var Bed = _InsertIPDraft.Insert(InsertIPDraftParams);
            return Ok(Bed);
        }

        [HttpPost("IPPathOrRadiRequest")]

        public IActionResult IPPathOrRadiRequest(IPPathOrRadiRequestParams IPPathOrRadiRequestParams)
        {
            var RequestId = _IPPathOrRadiRequest.Insert(IPPathOrRadiRequestParams);
            return Ok(RequestId);
        }


        [HttpPost("IPMLCInsert")]

        public IActionResult IPMLCInsert(MLCInfoParams MLCInfoParams)
        {
            var RequestId = _MLCInfo.Insert(MLCInfoParams);
            return Ok(RequestId);
        }

        [HttpPost("IPMLCUpdate")]

        public IActionResult IPMLCUpdate(MLCInfoParams MLCInfoParams)
        {
            var RequestId = _MLCInfo.Update(MLCInfoParams);
            return Ok(RequestId);
        }


        [HttpPost("IPSettlement")]

        public IActionResult IPSettlement(IP_Settlement_Processparams IP_Settlement_Processparams)
        {
            var RequestId = _IP_Settlement_Process.Insert(IP_Settlement_Processparams);
            return Ok(RequestId);
        }

        [HttpPost("WhatsappSMSoutgoingSave")]
        public IActionResult InsertWhatsappsmsoutgoing(WhatsappSmsparam WhatsappSmsparam)
        {
            var Id = _WhatsappSms.Insert(WhatsappSmsparam);
            return Ok(Id);
        }

        [HttpPost("WhatsappSMSoutgoingUpdate")]
        public IActionResult UpdateWhatsappsmsoutgoing(WhatsappSmsparam WhatsappSmsparam)
        {
            var RequestId = _WhatsappSms.Update(WhatsappSmsparam);
            return Ok(RequestId);
        }

        [HttpPost("DocAttachment")]
        public async Task<IActionResult> DocumentAttachmentAsync([FromForm] DocumentAttachment documentAttachments)
        {
            foreach (DocumentAttachmentItem objFile in documentAttachments.Files)
            {
                string NewFileName = objFile.OPD_IPD_ID + "_" + (objFile.CategoryName ?? "") + "_" + objFile.OPD_IPD_Type;
                string FileName = await _IFileUtility.UploadDocument(objFile.DocFile, "PatientDocuments\\" + objFile.OPD_IPD_ID, NewFileName);
                objFile.FilePath = FileName;
                objFile.FilePathLocation = FileName;
                objFile.FileName = objFile.DocFile.FileName;
                objFile.CategoryId = 1;
                objFile.CategoryName = "AppoinmentImgDocument";
            }
            var res = _DocumentAttachment.Save(documentAttachments.Files);
            return Ok(res.Select(x => new { x.Id, x.FileName }));
        }
        [HttpGet("get-files")]
        public IActionResult GetFiles(int OPD_IPD_ID, int OPD_IPD_Type)
        {
            return Ok(_DocumentAttachment.GetFiles(OPD_IPD_ID, OPD_IPD_Type).Select(x => new { x.Id, x.FileName }));
        }
        [HttpGet("download-file")]
        public async Task<IActionResult> DownloadFiles(int Id)
        {
            DocumentAttachmentItem item = _DocumentAttachment.GetFileById(Id);
            if (item == null)
            {
                return NotFound();
            }
            var fileData = await _IFileUtility.DownloadFile(item.FilePathLocation);
            return File(fileData.Item1, fileData.Item2, fileData.Item3);
        }
        [HttpGet("get-file")]
        public async Task<IActionResult> GetFile(int Id)
        {
            DocumentAttachmentItem item = _DocumentAttachment.GetFileById(Id);
            if (item == null)
            {
                return NotFound();
            }
            var fileData = await _IFileUtility.GetBase64(item.FilePathLocation);
            return Ok(new { file = fileData, Mime = _IFileUtility.GetMimeType(item.FilePathLocation) });
        }

        [HttpPost("SingleDocUpload")]
        public async Task<IActionResult> SingleDocUpload(IFormFile formFile, string FileName)
        {
            if (formFile.FileName.Length > 0)
            {
                var filepath = getFilePath(FileName);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string imgpath = filepath + "\\" + formFile.FileName;
                if (System.IO.File.Exists(imgpath))
                {
                    System.IO.File.Delete(imgpath);
                }
                using (FileStream fileStream = System.IO.File.Create(imgpath))
                {
                    await formFile.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
            }

            //var RequestId = _DocumentAttachment.Save(documentAttachment);
            return Ok(true);
        }
        [HttpPost("MultipleDocUpload")]
        public async Task<IActionResult> MultipleDocUpload(IFormFileCollection formFileCollection, string FileName)
        {
            {
                var filepath = getFilePath(FileName);
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                foreach (var file in formFileCollection)
                {
                    string imgpath = filepath + "\\" + file.FileName;

                    if (System.IO.File.Exists(imgpath))
                    {
                        System.IO.File.Delete(imgpath);
                    }
                    using (FileStream fileStream = System.IO.File.Create(imgpath))
                    {
                        await file.CopyToAsync(fileStream);
                        fileStream.Flush();
                    }
                }

                //var RequestId = _DocumentAttachment.Save(documentAttachment);
                return Ok(true);
            }
        }
        [NonAction]
        public string getFilePath(string fileName)
        {
            return this._environment.WebRootPath + "\\upload\\" + fileName;
        }

        [HttpPost("OTDetailInsert")]
        public IActionResult OTDetailInsert(OTTableDetailParams OTTableDetailParams)
        {
            var OtId = _OTTableDetail.Insert(OTTableDetailParams);
            return Ok(OtId);
        }


        [HttpPost("OTDetailUpdate")]
        public IActionResult OTDetailUpdate(OTTableDetailParams OTTableDetailParams)
        {
            var OtId = _OTTableDetail.Update(OTTableDetailParams);
            return Ok(OtId);
        }


        [HttpPost("OTBookingInsert")]
        public IActionResult OTBookingInsert(OTBookingDetailParams OTBookingDetailParams)
        {
            var OtId = _OTBookingDetail.Insert(OTBookingDetailParams);
            return Ok(OtId);
        }


        [HttpPost("OTBookingUpdate")]
        public IActionResult OTBookingUpdate(OTBookingDetailParams OTBookingDetailParams)
        {
            var OtId = _OTBookingDetail.Update(OTBookingDetailParams);
            return Ok(OtId);
        }
        [HttpPost("CathLabBookingInsert")]
        public IActionResult CathLabBookingInsert(CathLabBookingDetailParams CathLabBookingDetailParams)
        {
            var OtId = _CathLabBookingDetail.Insert(CathLabBookingDetailParams);
            return Ok(OtId);
        }

        [HttpPost("CathLabBookingUpdate")]
        public IActionResult CathLabBookingUpdate(CathLabBookingDetailParams CathLabBookingDetailParams)
        {
            var OtId = _CathLabBookingDetail.Update(CathLabBookingDetailParams);
            return Ok(OtId);
        }

        [HttpPost("OTEndoscopyInsert")]
        public IActionResult OTEndoscopyInsert(OTEndoscopyParam OTEndoscopyParam)
        {
            var OtId = _OTEndoscopy.Insert(OTEndoscopyParam);
            return Ok(OtId);
        }

        [HttpPost("OTEndoscopyUpdate")]
        public IActionResult OTEndoscopyUpdate(OTEndoscopyParam OTEndoscopyParam)
        {
            var OtId = _OTEndoscopy.Update(OTEndoscopyParam);
            return Ok(OtId);
        }

        [HttpPost("OTRequestInsert")]
        public IActionResult OTRequestInsert(OTRequestparam OTRequestparam)
        {
            var OtId = _OTRequest.Insert(OTRequestparam);
            return Ok(OtId);
        }

        [HttpPost("OTNoteTemplateInsert")]
        public IActionResult OTNoteTemplateInsert(OTNotesTemplateparam OTNotesTemplateparam)
        {
            var OtId = _OTNotesTemplate.Insert(OTNotesTemplateparam);
            return Ok(OtId);
        }


        [HttpPost("OTNoteTemplateUpdate")]
        public IActionResult OTNoteTemplateUpdate(OTNotesTemplateparam OTNotesTemplateparam)
        {
            var OtId = _OTNotesTemplate.Update(OTNotesTemplateparam);
            return Ok(OtId);
        }

        [HttpPost("NeroSurgeryOTNotesInsert")]
        public IActionResult NeroSurgeryOTNotesInsert(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam)
        {
            var OtId = _NeroSurgeryOTNotes.Insert(NeroSurgeryOTNotesparam);
            return Ok(OtId);
        }

        [HttpPost("NeroSurgeryOTNotesUpdate")]
        public IActionResult NeroSurgeryOTNotesUpdate(NeroSurgeryOTNotesparam NeroSurgeryOTNotesparam)
        {
            var OtId = _NeroSurgeryOTNotes.Update(NeroSurgeryOTNotesparam);
            return Ok(OtId);
        }

        [HttpPost("MaterialConsumption")]
        public IActionResult MaterialConsumption(MaterialConsumptionparam MaterialConsumptionparam)
        {
            var OtId = _MaterialConsumption.Insert(MaterialConsumptionparam);
            return Ok(OtId);
        }

        [HttpPost("DoctorNoteInsert")]
        public IActionResult DoctorNoteInsert(DoctorNoteparam DoctorNoteparam)
        {
            var OtId = _DoctorNote.Insert(DoctorNoteparam);
            return Ok(OtId);
        }


        [HttpPost("NursingTemplateInsert")]
        public IActionResult NursingTemplateInsert(NursingTemplateparam NursingTemplateparam)
        {
            var OtId = _NursingTemplate.Insert(NursingTemplateparam);
            return Ok(OtId);
        }

        [HttpPost("MrdMedicalcasepaperInsert")]
        public IActionResult MrdMedicalcasepaperInsert(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            var OtId = _Mrdmedicalcertificate.Insert(Mrdmedicalcertificateparam);
            return Ok(OtId);
        }


        [HttpPost("MrdMedicalcasepaperUpdate")]
        public IActionResult MrdMedicalcasepaperUpdate(Mrdmedicalcertificateparam Mrdmedicalcertificateparam)
        {
            var OtId = _Mrdmedicalcertificate.Update(Mrdmedicalcertificateparam);
            return Ok(OtId);
        }

        [HttpPost("MrdDeathcertificateInsert")]
        public IActionResult MrdDeathcertificateInsert(Mrddeathcertificateparam Mrddeathcertificateparam)
        {
            var OtId = _Mrddeathcertificate.Insert(Mrddeathcertificateparam);
            return Ok(OtId);
        }


        [HttpPost("SubCompanyTPAInsert")]
        public IActionResult SubCompanyTPAInsert(SubcompanyTPAparam SubcompanyTPAparam)
        {
            var OtId = _SubcompanyTPA.Insert(SubcompanyTPAparam);
            return Ok(OtId);
        }

        [HttpPost("SubCompanyTPAUpdate")]
        public IActionResult SubCompanyTPAUpdate(SubcompanyTPAparam SubcompanyTPAparam)
        {
            var OtId = _SubcompanyTPA.Update(SubcompanyTPAparam);
            return Ok(OtId);
        }

        [HttpPost("PrepostOtNoteInsert")]
        public IActionResult PrepostOtNoteInsert(Prepostopnoteparam Prepostopnoteparam)
        {
            var OtId = _Prepostopnote.Insert(Prepostopnoteparam);
            return Ok(OtId);
        }

        [HttpPost("PrepostOtNoteUpdate")]
        public IActionResult PrepostOtNoteUpdate(Prepostopnoteparam Prepostopnoteparam)
        {
            var OtId = _Prepostopnote.Update(Prepostopnoteparam);
            return Ok(OtId);
        }
    }

}

