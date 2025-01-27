using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.IO;
using HIMS.API.Utility;
using HIMS.Data.Radiology;
using HIMS.Model.Radiology;
using System.Data;
using HIMS.Model.Administration;
using HIMS.Model.Administration;
using HIMS.Data.Administration;
using HIMS.API.Comman;
using HIMS.Model.CustomerInformation;
using HIMS.Data.CustomerInformation;
using HIMS.Data.Nursing;

namespace HIMS.API.Controllers.Transaction
{
        [ApiController]
        [Route("api/[controller]")]
        public class MRDController : Controller
        {

        public readonly I_Nursing _Nursing;
        public readonly I_CustomerInformation _CustomerInformation;
        public readonly I_RadiologyTemplateResult i_RadiologyTemplate;
        public readonly I_Administration _Administration;
        public readonly IPdfUtility _pdfUtility;
            private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
            public readonly IFileUtility _FileUtility;
            public MRDController(I_Nursing I_Nursing, I_RadiologyTemplateResult i_Radiology,
                I_Administration Administration,
                 I_CustomerInformation I_CustomerInformation,
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, 
                IPdfUtility pdfUtility, IFileUtility fileUtility)
            {
            this._Nursing = I_Nursing;
            this.i_RadiologyTemplate = i_Radiology;
            this._CustomerInformation = I_CustomerInformation;
            _hostingEnvironment = hostingEnvironment;
            this._Administration = Administration;
            _pdfUtility = pdfUtility;
                _FileUtility = fileUtility;
            }

        [HttpPost("InsertPatICDCode")]
        public IActionResult InsertPatICDCode(PatICDCodeParam PatICDCodeParam)
        {
            var RPAP = _Administration.InsertPatICDCode(PatICDCodeParam);
            return Ok(RPAP);
        }

        [HttpPost("UpdatePatICDCode")]
        public IActionResult UpdatePatICDCode(PatICDCodeParam PatICDCodeParam)
        {
            var RPAP = _Administration.UpdatePatICDCode(PatICDCodeParam);
            return Ok(RPAP);
        }

    }
}

