using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using HIMS.Model.Radiology;
using HIMS.Data.Radiology;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class RadiologyController : Controller
    {
        public readonly I_RadiologyTemplateResult _RadiologyReportHeaderUpdate;

        public RadiologyController(I_RadiologyTemplateResult i_Radiology)
        {
            this._RadiologyReportHeaderUpdate = i_Radiology;
        }

        [HttpPost("RadiologyTemplateResult")]
        public IActionResult RadiologyTemplateResult(RadiologyTemplateResultParams RRHUP)
        {
            var RRHUPI = _RadiologyReportHeaderUpdate.Update(RRHUP);
            return Ok(RRHUPI);
        }


    }

}

