using HIMS.API.Utility;
using HIMS.Data.IPD;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Transaction
{


    [ApiController]
    [Route("api/[controller]")]
    public class IPReportController : Controller
    {
        public readonly I_IPReports _IPReports;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public IPReportController(
           I_IPReports iPReports, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _IPReports = iPReports;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        [HttpGet("view-IPFinalBill")]
        public IActionResult ViewIPFinalBill(DateTime FromDate, DateTime ToDate,int AdmissionID)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPFinalBill.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPFinalBill(FromDate, ToDate, AdmissionID, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPFinalBill", "IPFinalBill", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDDischargeTypeCompanyWise")]
        public IActionResult ViewIPDDischargeTypeCompanyWise(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDDischargeTypeCompanyWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeTypeCompanyWise(FromDate, ToDate, DoctorId, DischargeTypeId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeTypeCompanyWise", "IPDDischargeTypeCompanyWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-IPDDischargeTypeCompanyWiseCount")]
        public IActionResult ViewIPDDischargeTypeCompanyWiseCount(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDDischargeTypeCompanyWiseCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeTypeCompanyWiseCount(FromDate, ToDate, DoctorId, DischargeTypeId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeTypeCompanyWiseCount", "IPDDischargeTypeCompanyWiseCount", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-IPDRefDoctorWise")]
        public IActionResult ViewIPDRefDoctorWise(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDRefDoctorWise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDRefDoctorWise(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDRefDoctorWise", "IPDRefDoctorWise", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDDischargeDetails")]
        public IActionResult ViewIPDDischargeDetails(DateTime FromDate, DateTime ToDate, int DischargeTypeId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDDischargeDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeDetails(FromDate, ToDate, DischargeTypeId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeDetails", "IPDDischargeDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


       

        [HttpGet("view-IPDAdvanceReport")]
        public IActionResult ViewIPDAdvanceReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPAdvanceReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDAdvanceReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDAdvanceReport", "IPDAdvanceReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-BillReport")]
        public IActionResult ViewBillReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_BillReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewBillReport(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "BillReport", "BillReport", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-RefundofAdvanceReport")]
        public IActionResult ViewRefundofAdvanceReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_RefunfOfAdvanceReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewRefundofAdvanceReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefundofAdvanceReport", "RefundofAdvanceReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-RefundofBillReport")]
        public IActionResult ViewRefundofBillReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_RefunfOfBillReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewRefundofBillReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefundofBillReport", "RefundofBillReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDischargeAndBillGenerationPendingReport")]
        public IActionResult IPDischargeAndBillGenerationPendingReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDischargeAndBillGenerationPendingReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDischargeAndBillGenerationPendingReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDischargeAndBillGenerationPendingReport", "IPDischargeAndBillGenerationPendingReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPBillGenerationPaymentDueReport")]
        public IActionResult ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPBillGenerationPaymentDueReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPBillGenerationPaymentDueReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPBillGenerationPaymentDueReport", "IPBillGenerationPaymentDueReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-BillSummaryReport")]
        public IActionResult ViewBillSummaryReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_BillSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewBillSummaryReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "BillSummaryReport", "BillSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPAdmitPatientwardwisechargesReport")]
        public IActionResult ViewIPAdmitpatientwardwisechargesReport(int DoctorId, int WardId, int CompanyId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReports_AdmittedPatientWardwisecharges.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPAdmitPatientwardwisechargesReport(DoctorId, WardId, CompanyId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReports_AdmittedPatientWardwisecharges", "IPReports_AdmittedPatientWardwisecharges" + WardId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPCurrentrefadmittedReport")]
        public IActionResult ViewIPCurrentrefadmittedReport(int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReports_CurrentRefDoctorAdmitted.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCurrentrefadmittedlist(DoctorId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReports_CurrentRefDoctorAdmitted", "IPReports_CurrentRefDoctorAdmitted" + DoctorId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPDischargeTypeReport")]
        public IActionResult ViewIPDischargeTypeReport(int DoctorId, DateTime FromDate, DateTime ToDate, int DischargeTypeId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReportDischargeType.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargetypewise(DoctorId, FromDate, ToDate, DischargeTypeId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPReportDischargeType", "IPReportDischargeType" + DoctorId, Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDAdmissionListCompanyWiseSummary")]
        public IActionResult ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDAdmissionListCompanyWiseSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDAdmissionListCompanyWiseSummary(FromDate, ToDate, DoctorId, WardId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDAdmissionListCompanyWiseSummary", "IPDAdmissionListCompanyWiseSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);



            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPDailyCollectionReport")]
        public IActionResult ViewIPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReports_IPDDailycollection.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDailyCollectionReceipt(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDailycollection", "IPDDailycollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);



            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-OPIPBILLSummaryReport")]
        public IActionResult ViewOPIPBillSummaryReceipt(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_OPIPBillsummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewOPIPBillSummaryReceipt(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPIPBillsummary", "OPIPBillsummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-IPCreditReport")]
        public IActionResult ViewIPCreditReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPCreditReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPCreditReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPCreditReport", "IPCreditReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-IPDAdmissionList")]
        public IActionResult ViewIPDAdmissionList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDAdmissionList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDAdmissionList(FromDate, ToDate, DoctorId, WardId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDAdmissionList", "IPDAdmissionList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDAdmissionListCompanyWiseDetails")]
        public IActionResult ViewIPDAdmissionListCompanyWiseDetails(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDAdmissionListCompanyWiseDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDAdmissionListCompanyWiseDetails(FromDate, ToDate, DoctorId, WardId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDAdmissionListCompanyWiseDetails", "IPDAdmissionListCompanyWiseDetails", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDCurrentAdmittedList")]
        public IActionResult ViewIPDCurrentAdmittedList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDCurrentAdmittedList.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDCurrentAdmittedList(FromDate, ToDate, DoctorId, WardId, CompanyId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDCurrentAdmittedList", "IPDCurrentAdmittedList", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-IPDCurrentAdmittedWardWiseCharges")]
        public IActionResult ViewIPDCurrentAdmittedWardWiseCharges(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDCurrentAdmittedWardWiseChanges.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDCurrentAdmittedWardWiseCharges(FromDate, ToDate, DoctorId, WardId, CompanyId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDCurrentAdmittedWardWiseCharges", "IPDCurrentAdmittedWardWiseCharges", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-IPDDischargeReportWithMarkStatus")]
        public IActionResult ViewIPDDischargeReportWithMarkStatus(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDDischargeReportWithMarkStatus.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeReportWithMarkStatus(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeReportWithMarkStatus", "IPDDischargeReportWithMarkStatus", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-IPDDischargeReportWithBillSummary")]
        public IActionResult ViewIPDDischargeReportWithBillSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDDischargeReportWithBillSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewIPDDischargeReportWithBillSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPDDischargeReportWithBillSummary", "IPDDischargeReportWithBillSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentWiseCountSummary")]
        public IActionResult ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDepartmentWiseCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDepartmentWiseCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseCountSummary", "DepartmentWiseCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DoctorWiseCountSummary")]
        public IActionResult ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPDoctorWiseCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDoctorWiseCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseCountSummary", "DoctorWiseCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-OPToIPConvertedListWithServiceAvailed")]
        public IActionResult ViewOPToIPConvertedListWithServiceAvailed(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_OPtoIPConvertedListWithServiceAvailed.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewOPToIPConvertedListWithServiceAvailed(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPToIPConvertedListWithServiceAvailed", "OPToIPConvertedListWithServiceAvailed", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DateWiseAdmissionCount")]
        public IActionResult ViewDateWiseAdmissionCount(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DatewiseAdmissionCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDateWiseAdmissionCount(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DateWiseAdmissionCount", "DateWiseAdmissionCount", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-MonthWiseAdmissionCount")]
        public IActionResult ViewMonthWiseAdmissionCount(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_MonthwiseAdmissionCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewMonthWiseAdmissionCount(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "MonthWiseAdmissionCount", "MonthWiseAdmissionCount", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DateWiseDoctorWiseAdmissionCountDetail")]
        public IActionResult ViewDateWiseDoctorWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DateWiseDoctorWiseAdmissionCountDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDateWiseDoctorWiseAdmissionCountDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DateWiseDoctorWiseAdmissionCountDetail", "DateWiseDoctorWiseAdmissionCountDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DateWiseDoctorWiseAdmissionCountSummary")]
        public IActionResult ViewDateWiseDoctorWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DateWiseDoctorWiseAdmissionCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDateWiseDoctorWiseAdmissionCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DateWiseDoctorWiseAdmissionCountSummary", "DateWiseDoctorWiseAdmissionCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DateWiseDepartmentWiseAdmissionCountDetail")]
        public IActionResult ViewDateWiseDepartmentWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DateWiseDepartmentWiseAdmissionCountDetails .html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDateWiseDepartmentWiseAdmissionCountDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DateWiseDepartmentWiseAdmissionCountDetail", "DateWiseDepartmentWiseAdmissionCountDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DateWiseDepartmentWiseAdmissionCountSummary")]
        public IActionResult ViewDateWiseDepartmentWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DateWiseDepartmentWiseAdmissionCountSummary .html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDateWiseDepartmentWiseAdmissionCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DateWiseDepartmentWiseAdmissionCountSummary", "DateWiseDepartmentWiseAdmissionCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DrWiseCollectionDetail")]
        public IActionResult ViewDrWiseCollectionDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DrWiseCollectionDetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDrWiseCollectionDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DrWiseCollectionDetail", "DrWiseCollectionDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DrWiseCollectionSummary")]
        public IActionResult ViewDrWiseCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DrWiseCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDrWiseCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DrWiseCollectionSummary", "DrWiseCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }



        [HttpGet("view-DepartmentWiseCollectionDetail")]
        public IActionResult ViewDepartmentWiseCollectionDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DepartmentWiseCollectionDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDepartmentWiseCollectionDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseCollectionDetail", "DepartmentWiseCollectionDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DepartmentWiseCollectionSummary")]
        public IActionResult ViewDepartmentWiseCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_DepartmentWiseCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewDepartmentWiseCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseCollectionSummary", "DepartmentWiseCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-CompanyWiseAdmissionCountDetail")]
        public IActionResult ViewCompanyWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseAdmissionCountDetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseAdmissionCountDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseAdmissionCountDetail", "CompanyWiseAdmissionCountDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-CompanyWiseAdmissionSummary")]
        public IActionResult ViewCompanyWiseAdmissionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseAdmissionCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseAdmissionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseAdmissionSummary", "CompanyWiseAdmissionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-CompanyWiseBillDetailReport")]
        public IActionResult ViewCompanyWiseBillDetailReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseBillDetailReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseBillDetailReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseBillDetailReport", "CompanyWiseBillDetailReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }



        [HttpGet("view-CompanyWiseBillSummaryReport")]
        public IActionResult ViewCompanyWiseBillSummaryReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseBillSummaryReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseBillSummaryReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseBillSummaryReport", "CompanyWiseBillSummaryReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }



        [HttpGet("view-CompanyWiseCreditReportDetail")]
        public IActionResult ViewCompanyWiseCreditReportDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseCreditReportDetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseCreditReportDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseCreditReportDetail", "CompanyWiseCreditReportDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-CompanyWiseCreditReportSummary")]
        public IActionResult ViewCompanyWiseCreditReportSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_CompanyWiseCreditReportSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _IPReports.ViewCompanyWiseCreditReportSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CompanyWiseCreditReportSummary", "CompanyWiseCreditReportSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
    }
}
    


      
