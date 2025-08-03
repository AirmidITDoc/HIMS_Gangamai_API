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
using HIMS.Model.IPD;
using HIMS.Data.IPD;

namespace HIMS.API.Controllers.Transaction
{
        [ApiController]
        [Route("api/[controller]")]
        public class MRDController : Controller
        {

        public readonly I_Nursing _Nursing;
        public readonly I_Mrdmedicalcertificate _Mrdmedicalcertificate;
        public readonly I_CustomerInformation _CustomerInformation;
   
        public readonly I_RadiologyTemplateResult i_RadiologyTemplate;
        public readonly I_Administration _Administration;
        public readonly IPdfUtility _pdfUtility;
            private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
            public readonly IFileUtility _FileUtility;
            public MRDController(I_Nursing I_Nursing, I_RadiologyTemplateResult i_Radiology,
                I_Administration Administration, I_Mrdmedicalcertificate Mrdmedicalcertificate,
                 I_CustomerInformation I_CustomerInformation,
                
                
        
                Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment, 
                IPdfUtility pdfUtility, IFileUtility fileUtility)
            {
            this._Nursing = I_Nursing;
            this.i_RadiologyTemplate = i_Radiology;
            this._CustomerInformation = I_CustomerInformation;
            _hostingEnvironment = hostingEnvironment;
            this._Administration = Administration;
            this._Mrdmedicalcertificate = Mrdmedicalcertificate;
            
            _pdfUtility = pdfUtility;
                _FileUtility = fileUtility;
            }

        [HttpPost("InsertPatICDCode")]
        public IActionResult InsertPatICDCode(PatICDCodeParam PatICDCodeParam)
        {
            var RPAP = _Mrdmedicalcertificate.InsertPatICDCode(PatICDCodeParam);
            return Ok(RPAP);
        }

        [HttpPost("UpdatePatICDCode")]
        public IActionResult UpdatePatICDCode(PatICDCodeParams PatICDCodeParam)
        {
            var RPAP = _Mrdmedicalcertificate.UpdatePatICDCode(PatICDCodeParam);
         
            
            return Ok(RPAP);
        }
        [HttpPost("SaveMICDCodingMaster")]
        public IActionResult SaveMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {
            var Id = _Mrdmedicalcertificate.SaveMICDCodingMaster(MICDCodingMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMICDCodingMaster")]
        public IActionResult UpdateMICDCodingMaster(MICDCodingMasterParam MICDCodingMasterParam)
        {
            var Id = _Mrdmedicalcertificate.UpdateMICDCodingMaster(MICDCodingMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
      
        [HttpPost("InsertMrdMedicolegalCertificate")]
        public IActionResult InsertMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            var Id = _Mrdmedicalcertificate.InsertMrdMedicolegalCertificate(MrdMedicolegalCertificateparam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Updated successfully", Id);
            return Ok(Response);
        }

        [HttpPost("UpdateMrdMedicolegalCertificate")]
        public IActionResult UpdateMrdMedicolegalCertificate(MrdMedicolegalCertificateparam MrdMedicolegalCertificateparam)
        {
            var Id = _Mrdmedicalcertificate.UpdateMrdMedicolegalCertificate(MrdMedicolegalCertificateparam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Updated successfully", Id);
            return Ok(Response);
        }

        [HttpPost("SaveMICDCdeheadMaster")]
        public IActionResult SaveMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {
            var Id = _Mrdmedicalcertificate.SaveMICDCdeheadMaster(MICDCdeheadMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record added successfully", Id);
            return Ok(Response);
        }
        [HttpPost("UpdateMICDCdeheadMaster")]
        public IActionResult UpdateMICDCdeheadMaster(MICDCdeheadMasterParam MICDCdeheadMasterParam)
        {
            var Id = _Mrdmedicalcertificate.UpdateMICDCdeheadMaster(MICDCdeheadMasterParam);
            var Response = ApiResponseHelper.GenerateResponse<string>(ApiStatusCode.Status200OK, "Record Update successfully", Id);
            return Ok(Response);
        }
    }
}

