using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Opd;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using HIMS.Model.Pathology;
using HIMS.Data.Pathology;

namespace HIMS.API.Controllers.Transaction
{

    [ApiController]
    [Route("api/[controller]")]
    public class PathologyController : Controller
    {
        public readonly I_PathologyTemplateResult _PathologyTemplateResult;
        public readonly I_Pathologysamplecollection _Pathologysamplecollection;
       public readonly I_pathresultentry _Pathresultentry;
        
        public PathologyController(I_PathologyTemplateResult pathologyTemplateResult,
            I_Pathologysamplecollection pathologysamplecollection,I_pathresultentry pathresultentry)
        {
            this._PathologyTemplateResult = pathologyTemplateResult;
            this._Pathologysamplecollection = pathologysamplecollection;
            this._Pathresultentry = pathresultentry;
        }

        [HttpPost("PathologyTemplateResult")]
        public IActionResult PathologyTemplateResult(PathologyTemplateResultParams PTRP)
        {
            var PTR1 = _PathologyTemplateResult.Insert(PTRP);
            return Ok(PTR1);
        }

        [HttpPost("PathSamplecollection")]
        public IActionResult PathSamplecollection(Pathologysamplecollectionparameter PTRP)
        {
            var PTR1 = _Pathologysamplecollection.Update(PTRP);
            return Ok(PTR1);
        }

        [HttpPost("PathResultentryInsert")]
        public IActionResult PathResultentryInsert(pathresultentryparam pathresultentryparam)
        {
            var PTR1 = _Pathresultentry.Insert(pathresultentryparam);
            return Ok(PTR1);
        }
    }

}

