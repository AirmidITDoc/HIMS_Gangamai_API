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
using HIMS.Model.WhatsAppEmail;
using HIMS.Data.WhatsAppEmail;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class InPatientController : Controller
    {
        public readonly I_Sales _Sales;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

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

        public readonly I_DoctorShare _DoctorShare;

       
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
            I_Mrddeathcertificate mrddeathcertificate, I_SubcompanyTPA subcompanyTPA, I_Prepostopnote prepostopnote,I_WhatsappSms whatsappSms,
            I_Sales sales,I_DoctorShare doctorShare,
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility
            )
        {
            this._Sales = sales;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
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
            this._DoctorShare = doctorShare;
            
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


        [HttpGet("view-Admitted_PatientList")]
        public IActionResult ViewAdmittedPatientList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdmittedPatientList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _Admission.AdmissionListCurrent(FromDate, ToDate, DoctorId, WardId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AdmittedPatientList", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-Admitted_PatientListHospitalDet")]
        public IActionResult ViewAdmittedPatientListHospdetail(int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "Admittedpatientlisthospdetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _Admission.AdmissionListCurrentHospitaldetail( DoctorId, WardId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "Admittedpatientlisthospdetail", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-Admitted_PatientListPharmacydetail")]
        public IActionResult ViewAdmittedPatientListPharmactdet(int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "Admittedpatientpharmacydetaillist.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _Admission.AdmissionListCurrentPharmacydetail(DoctorId, WardId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "Admittedpatientpharmacydetaillist", "", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-Admitted_PatientCasepaper")]
        public IActionResult ViewPatientCasepaper(int AdmissionId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _Admission.ViewAdmissionPaper(AdmissionId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPAdmission", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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
            return (IPD);
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
            return (IPD);
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


        [HttpGet("view-IP-ReturnOfAdvanceReceipt")]
        public IActionResult ViewRetunOFAdvanceReceipt(int RefundId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RefundofAdvanceReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPRefundofAdvance.ViewIPRefundofAdvanceReceipt(RefundId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefundofAdvanceReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpPost("InsertIPRefundofBill")]

        public String InsertIPRefundofBill(IPRefundofBilllparams IPRefundofBillParams)
        {
            var IPD = _IPRefundofBilll.Insert(IPRefundofBillParams);
            return (IPD.ToString());

        }


        [HttpGet("view-IP-ReturnOfBillReceipt")]
        public IActionResult ViewRetunOFBillReceipt(int RefundId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPRefundBillReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPRefundofBilll.ViewIPRefundofBillReceipt(RefundId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPRefundBillReceipt", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpPost("InsertIPPrescription")]

        public IActionResult InsertIPPrescription(IPPrescriptionParams IPPrescriptionParams)
        {
            var IPD = _IPPrescription.Insert(IPPrescriptionParams);
            return Ok(IPD);

        }


        [HttpGet("view-IP_Prescription")]
        public IActionResult ViewIPPrescription(int OP_IP_ID, int PatientType)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IpPrescription.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPPrescription.ViewIPPrescriptionReceipt(OP_IP_ID, PatientType, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IpPrescription", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-OP_Prescription")]
        public IActionResult ViewOPPrescription(int VisitId, int PatientType)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescription.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPPrescription.ViewOPPrescriptionReceipt(VisitId, PatientType, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPPrescription", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IP_PrescriptionDetails")]
        public IActionResult ViewIPPrescriptionDetails(int AdmissionID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPPrescriptiondetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPPrescription.ViewIPPrescriptionDetailReceipt(AdmissionID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPPrescriptiondetail", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IP_PrescriptionSummarylist")]
        public IActionResult VieIOPPrescriptionSummarylist(int  AdmissionID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "Prescriptionsummarylist.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPPrescription.ViewIPPrescriptionSummReceipt(AdmissionID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "Prescriptionsummarylist", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpPost("InsertIPPrescriptionReturn")]

        public IActionResult InsertIPPrescriptionReturn(IPPrescriptionReturnParams IPPrescriptionReturnParams)
        {
            var IPD = _IPPrescriptionReturn.Insert(IPPrescriptionReturnParams);
            return Ok(IPD);

        }

        [HttpGet("view-IP_PrescriptionReturn")]
        public IActionResult ViewIPPrescriptionReturn(int PresReId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPPrescriptionReturn.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IPPrescriptionReturn.ViewIPPrescriptionReturnReceipt(PresReId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPPrescriptionReturn", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IP_PrescriptionReturnWardwise")]
        public IActionResult ViewIPPrescriptionReturnwardwise(DateTime FromDate, DateTime ToDate, int Reg_No)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PrescriptionReturnwardwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HeaderName.html");
            var html = _IPPrescriptionReturn.ViewIPPrescriptionReturnfromwardReceipt(FromDate, ToDate, Reg_No, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PrescriptionReturnwardwise", "", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            // write logic for send pdf in whatsapp


            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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



        [HttpGet("view-IP-BillReceipt")]
        public IActionResult ViewIpBillReceipt(int BillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPBillingReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPBilling.ViewIPBillReceipt(BillNo, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IpBillingReceipt", "IpBillingReceipt" + BillNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IP-BillDatewiseReceipt")]
        public IActionResult ViewIpBilldatewiseReceipt(int BillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPBillDatewiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPBilling.ViewIPBillDatewiseReceipt(BillNo, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPBillDatewiseReport", "IPBillDatewiseReport" + BillNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IP-BillWardwiseReceipt")]
        public IActionResult ViewIpBillwardwiseReceipt(int BillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPBillWardwiseReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPBilling.ViewIPBillWardwiseReceipt(BillNo, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPBillWardwiseReceipt", "IPBillWardwiseReceipt" + BillNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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

        [HttpGet("view-IP-Labrequest")]
        public IActionResult ViewLabRequestReceipt(int RequestId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "LabRequest.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPLabrequestChange.ViewLabRequest(RequestId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "LabRequest", "LabRequest" + RequestId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

    

        [HttpGet("view-IP-InterimBillReceipt")]
        public IActionResult ViewIpInterimBillReceipt(int BillNo)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPInterimBill.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPInterimBill.ViewIPInterimBillReceipt(BillNo, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPInterimBill", "IPInterimBill" + BillNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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



        [HttpGet("view-IP-AdvanceReceipt")]
        public IActionResult ViewAdvanceReceipt(int AdvanceDetailID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdvanceReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _IPAdvance.ViewAdvanceReceipt(AdvanceDetailID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AdvanceReceipt", "AdvanceReceipt" + AdvanceDetailID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpPost("IPAdvance")]
        public String IPAdvance(IPAdvanceParams IPAdvanceParams)
        {
            var AdvId = _IPAdvance.Insert(IPAdvanceParams);
            return (AdvId);
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



        [HttpGet("view-IP-DraftBillReceipt")]
        public IActionResult ViewIpDraftBillReceipt(int AdmissionID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDraftBill.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _InsertIPDraft.ViewIPDraftBillReceipt(AdmissionID, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDraftBill", "IPInterimBill" + AdmissionID.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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


        [HttpGet("view-IP-SettlementReceipt")]
        public IActionResult ViewSettlementReceipt(int PaymentId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SettlementReceipt.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IP_Settlement_Process.ViewSettlementReceipt(PaymentId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "SettlementReceipt", "SettlementReceipt" + PaymentId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        //[HttpPost("WhatsappSMSoutgoingSave")]
        //public IActionResult InsertWhatsappsmsoutgoing(WhatsappSmsparam WhatsappSmsparam)
        //{

        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaBillReceipt.html");
        //    var html = _Sales.ViewBill(WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo, WhatsappSmsparam.InsertWhatsappsmsInfo.PatientType, htmlFilePath);
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "PharmaBill", "PharmaBill_" + WhatsappSmsparam.InsertWhatsappsmsInfo.TranNo.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
            
        //    WhatsappSmsparam.InsertWhatsappsmsInfo.FilePath = tuple.Item2;

        //    var Id = _WhatsappSms.Insert(WhatsappSmsparam);
        //    return Ok(Id);
        //}
       
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

        [HttpGet("view-MaterialConsumption")]
        public IActionResult ViewMaterialconsumptionReceipt(int MaterialConsumptionId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MaterialConsumptionReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "HospitalHeader.html");
            var html = _MaterialConsumption.ViewMaterialConsumptionReceipt(MaterialConsumptionId, htmlFilePath, htmlHeaderFilePath);
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "MaterialConsumptionReport", "MaterialConsumptionReport" + MaterialConsumptionId.ToString(), Wkhtmltopdf.NetCore.Options.Orientation.Portrait);



            //if (System.IO.File.Exists(tuple.Item2))
            //    System.IO.File.Delete(tuple.Item2); // delete generated pdf file.
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
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


        [HttpPost("InsertDoctorShare")]
        public IActionResult DoctorshareInsert(Doctorshareparam Doctorshareparam)
        {
            var Id = _DoctorShare.Insert(Doctorshareparam);
            return Ok(Id);
        }
    }

}

