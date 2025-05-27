using HIMS.API.Comman;
using HIMS.Common.Extensions;
using HIMS.Data.CommanReports;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HIMS.API.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly I_Report _iReport;
        public ReportsController (I_Report iReport)
        {
            _iReport = iReport;
        }
        //[HttpPost("ViewReport")]
        //public IActionResult ViewReport(ReportRequestModel model)
        //{
        //    switch (model.Mode)
        //    {
        //        #region"OP Reports"

        //        case "RegistrationReport":
        //        default:
        //            break;
        //    }
        //    //model.BaseUrl = Convert.ToString(_configuration["BaseUrl"]);
        //    //model.StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
        //    string byteFile = _iReport.GetReportSetByProc(model);
        //    return Ok(new { base64 = byteFile });
        //}
    }
}
