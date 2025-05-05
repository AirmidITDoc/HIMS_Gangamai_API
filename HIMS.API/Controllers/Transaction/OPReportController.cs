

using HIMS.API.Comman;
using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.OPReports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore.Options;

namespace HIMS.API.Controllers.Transaction


{
    [ApiController]
    [Route("api/[controller]")]
    public class OPReportController : Controller
    {
        public readonly I_OPBillingReport _OPbilling;
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public OPReportController(
           I_OPBillingReport oPbilling, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _OPbilling = oPbilling;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;

        }
       
        [HttpGet("view-SimpleReportFormat")]
        public IActionResult ViewRegistrationReportDemo(DateTime FromDate, DateTime ToDate)
        {
            
            string vDate = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_tt");

            //switch (model.Mode)
            //{
            //    #region"OP Reports"

            //    case "RegistrationReport":
                    string[] headerList = { "Sr.No", "UHID", "Patient Name", "Address", "City", "Pin Code", "Age", "Gender Name", "Mobile No" };
                    string[] colList = { "RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };

                    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");

                    var html = _OPbilling.ViewSimpleReportFormat("rptListofRegistration", FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath), colList, headerList);
                    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RegistrationReportUsingTemplate", "RegistrationList_" + vDate, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            //    case "AppointmentListReport":
            //        string[] headerList = { "Sr.No", "UHID", "Patient Name", "Address", "City", "Pin Code", "Age", "Gender Name", "Mobile No" };
            //        string[] colList = { "RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };

            //        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
            //        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");

            //        var html = _OPbilling.ViewSimpleReportFormat("rptOPAppointmentListReport", FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath), colList, headerList);
            //        var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RegistrationReportUsingTemplate", "RegistrationList_" + vDate, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
                
            //    default:
            //        break;
            //}
            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }


        [HttpGet("view-OPDailyCollectionReport")]
        

        public IActionResult ViewOPDailycollectionReport(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPDailycollectionuserwise.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDailyCollectionReceipt(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPNewDailycollection", "OPNewDailycollection", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }
        [HttpGet("view-DepartmentWisecountSummury")]
        public IActionResult ViewOPDeptwisecountsummaryReport(DateTime FromDate, DateTime ToDate, int DepartmentId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseCountSummury.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWisecountSummury(FromDate, ToDate, DepartmentId, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWisecountSummury", "DepartmentWisecountSummury", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-OPDoctorWiseVisitCountSummary")]

        
        public IActionResult ViewDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate, int DoctorId)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitSummury.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDoctorWiseVisitCountSummary(FromDate, ToDate,  DoctorId,htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }



        [HttpGet("view-OPAppoinmentListWithServiseAvailed")]
        public IActionResult ViewOPAppoinmentListWithServiseAvailed(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppointmentListWithServiseAvailed.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPAppoinmentListWithServiseAvailed(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPAppoinmentListWithServiseAvailed", "OPAppoinmentListWithServiseAvailed", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);


            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }

        [HttpGet("view-RegistrationReport")]
        public IActionResult ViewRegistrationReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RegistrationReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewRegistrationReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RegistrationReport ", "RegistrationReport ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
        }




        [HttpGet("view-AppointmentListReport")]
        public IActionResult ViewOPAppointmentListReport(int Doctor_Id,DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppoitnmentListReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPAppointmentListReport(Doctor_Id,FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "AppointmentListReport", "AppointmentListReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });


        }


        [HttpGet("view-DoctorWiseVisitReport")]
        public IActionResult ViewDoctorWiseVisitReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseVisitReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseVisitReport ", "DoctorWiseVisitReport ", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-RefDoctorWiseReport")]
        public IActionResult ViewRefDoctorWiseReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RefDoctorWiseReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewRefDoctorWiseReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "RefDoctorWiseReport ", "RefDoctorWiseReport ", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

     


        [HttpGet("view-CrossConsultationReport")]
        public IActionResult ViewCrossConsultationReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_CrossConsultationReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewCrossConsultationReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "CrossConsultationReport", "CrossConsultationReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

            }

        //[HttpGet("view-OPDoctorWiseVisitCountSummary")]
        //public IActionResult ViewOPDoctorWiseVisitCountSummary(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitSummury.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _OPbilling.ViewOPDoctorWiseVisitCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        //}


        [HttpGet("view-OPDoctorWiseNewOldPatientReport")]
        public IActionResult ViewOPDoctorWiseNewOldPatientReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseNewAndOldPatientReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDoctorWiseNewOldPatientReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDoctorWiseNewOldPatientReport", "OPDoctorWiseNewOldPatientReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DepartmentWiseOpdCollectionSummary")]
        public IActionResult ViewDepartmentWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOpdCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOpdCollectionSummary", "DepartmentWiseOpdCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DayWiseOpdCountDetails")]
        public IActionResult ViewDayWiseOpdCountDetails(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDayWiseOpdCountDetails(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DayWiseOpdCountDetails", "DayWiseOpdCountDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DayWiseOpdCountSummry")]
        public IActionResult ViewDayWiseOpdCountSummry(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDayWiseOpdCountSummry(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DayWiseOpdCountSummry", "DayWiseOpdCountSummry", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DepartmentWiseOpdCountSummary")]
        public IActionResult ViewDepartmentWiseOpdCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOpdCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOpdCountSummary", "DepartmentWiseOpdCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-DoctorWiseOpdCountSummary")]
        public IActionResult ViewDoctorWiseOpdCountSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOpdCountSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseOpdCountSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseOpdCountSummary", "DoctorWiseOpdCountSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-DoctorWiseOpdCollectionSummary")]
        public IActionResult ViewDoctorWiseOpdCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOPDCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDoctorWiseOpdCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DoctorWiseOpdCollectionSummary", "DoctorWiseOpdCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

            }
        [HttpGet("view-OPCollectionSummary")]
        public IActionResult ViewOPCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPCollectionReportSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPCollectionSummary", "OPCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-BillReportSummary")]

        public IActionResult ViewBillReportSummary(DateTime FromDate, DateTime ToDate, int AddedById)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_BillReportSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewBillReportSummary(FromDate, ToDate, AddedById, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "BillReportSummary", "BillReportSummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-BillReportSummarySummary")]
        public IActionResult ViewBillReportSummarySummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_BillReportSummarySummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewBillReportSummarySummary(FromDate, ToDate,  htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "BillReportSummarySummary", "BillReportSummarySummary", Wkhtmltopdf.NetCore.Options.Orientation.Landscape);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }

        [HttpGet("view-OPDBillBalanceReport")]
        public IActionResult ViewOPDBillBalanceReport(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPDBillBalanceReport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDBillBalanceReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDBillBalanceReport", "OPDBillBalanceReport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-OPDRefundOfBill")]
        public IActionResult ViewOPDRefundOfBill(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPDRefundOfBill.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOPDRefundOfBill(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPDRefundOfBill", "OPDRefundOfBill", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentServiceGroupWiseCollectionSummary")]
        public IActionResult ViewDepartmentServiceGroupWiseCollectionSummary(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentServiceGroupWiseCollectionSummary.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentServiceGroupWiseCollectionSummary(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentServiceGroupWiseCollectionSummary", "DepartmentServiceGroupWiseCollectionSummary", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }


        [HttpGet("view-OpPatientCreditList")]
        public IActionResult ViewOpPatinetcreditlist(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPD_Creditreport.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewOpPatientCreditList(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "OPD_Creditreport", "OPD_Creditreport", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });


        }



        //[HttpGet("view-OpRefundofbillList")]
        //public IActionResult ViewOpRefundofbilllist(DateTime FromDate, DateTime ToDate)
        //{
        //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "Op_RefundofBill.html");
        //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //    var html = _OPbilling.ViewOPrefundbilllistReport(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
        //    var tuple = _pdfUtility.GeneratePdfFromHtml(html, "Op_RefundofBill", "Op_RefundofBill", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        //    return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        //}



        [HttpGet("view-DepartmentWiseOPDCount")]
        public IActionResult ViewDepartmentWiseOPDCount(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOPDCount.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOPDCount(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOPDCount", "DepartmentWiseOPDCount", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DrWiseOPDCountDetail")]
        public IActionResult ViewDrWiseOPDCountDetail(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DrWiseOPDDetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDrWiseOPDCountDetail(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DrWiseOPDCountDetail", "DrWiseOPDCountDetail", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DrWiseOPDCollectionDetails")] 
        public IActionResult ViewDrWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DrWiseOPDCollectionDetail.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDrWiseOPDCollectionDetails(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DrWiseOPDCollectionDetails", "DrWiseOPDCollectionDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentWiseOPDCollectionDetails")]
        public IActionResult ViewDepartmentWiseOPDCollectionDetails(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOPDCollectionDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentWiseOPDCollectionDetails(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWiseOPDCollectionDetails", "DepartmentWiseOPDCollectionDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
        [HttpGet("view-DepartmentServiceGroupWiseCollectionDetails")]
        public IActionResult ViewDepartmentServiceGroupWiseCollectionDetails(DateTime FromDate, DateTime ToDate)
        {
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentServiceGroupWiseCollectionDetails.html");
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
            var html = _OPbilling.ViewDepartmentServiceGroupWiseCollectionDetails(FromDate, ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath));
            var tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentServiceGroupWiseCollectionDetails", "DepartmentServiceGroupWiseCollectionDetails", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });

        }
    }
}




