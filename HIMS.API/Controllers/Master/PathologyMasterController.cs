using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Master.Pathology;
using HIMS.Model.Master.Pathology;
using HIMS.Data.Pathology;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class PathologyMasterController : ControllerBase
    {
        public readonly I_PathParameterMaster _PathParameterMaster;
        public readonly I_ParameterMasterAgeWise _ParameterMasterAgeWise;
        public readonly I_PathologyCategoryMaster _PathologyCategoryMasterRep;
        public readonly I_PathologyTemplateMaster _PathologyTemplateMasterRep;
        public readonly I_UnitMaster _UnitMasterRep;
        public readonly I_PathologyTestMaster _PathologyTestMasterRep;
        public readonly I_ParameterMaster _ParameterMaster;

        public PathologyMasterController(I_PathParameterMaster PathParameterMasterParams,
                                   I_ParameterMasterAgeWise parameterMasterAgeWise,
                                   I_PathologyCategoryMaster pathologyCategoryMaster,
                                   I_PathologyTemplateMaster pathologyTemplateMaster,
                                   I_UnitMaster unitMaster,
                                   I_PathologyTestMaster pathologyTestMaster,I_ParameterMaster parameterMaster
                                 )
        {
            this._PathParameterMaster = PathParameterMasterParams;
            this._ParameterMasterAgeWise = parameterMasterAgeWise;
            this._PathologyCategoryMasterRep = pathologyCategoryMaster;
            this._PathologyTemplateMasterRep = pathologyTemplateMaster;
            this._UnitMasterRep = unitMaster;
            this._PathologyTestMasterRep = pathologyTestMaster;
            this._ParameterMaster = parameterMaster;
        }


        [HttpPost("ParameterSave")]
        public IActionResult ParameterSave(ParameterMasterParams ParameterMasterParams)
        {
            var ParameterSave = _ParameterMaster.Insert(ParameterMasterParams);
            return Ok(ParameterSave);

        }

        [HttpPost("ParameterUpdate")]
        public IActionResult ParameterUpdate(ParameterMasterParams ParameterMasterParams)
        {
            var ParameterUpdate = _ParameterMaster.Update(ParameterMasterParams);
            return Ok(ParameterUpdate);
        }


        /*[HttpPost("pathParameterSave")]
        public IActionResult pathParameterSave(PathParameterMasterParams pathParameterMasterParams)
        {
            var ParameterSave = _PathParameterMaster.Save(pathParameterMasterParams);
            return Ok(ParameterSave);

        }

        [HttpPost("pathParameterUpdate")]
        public IActionResult ParameterUpdate(PathParameterMasterParams pathParameterMasterParams)
        {
            var ParameterUpdate = _PathParameterMaster.Update(pathParameterMasterParams);
            return Ok(ParameterUpdate);
        }
        */



        //--------------------------------------------------------------------------------

        [HttpPost("ParameterAgeWiseMasterSave")]
        public IActionResult ParameterAgeWiseMasterSave(ParameterMasterAgeWiseParams ParameterMasterAgeWiseParams)
        {
            var ParameterAgeWiseMasterSave = _ParameterMasterAgeWise.Insert(ParameterMasterAgeWiseParams);
            return Ok(ParameterAgeWiseMasterSave);

        }

        [HttpPost("ParameterAgeWiseMasterUpdate")]
        public IActionResult ParameterAgeWiseMasterUpdate(ParameterMasterAgeWiseParams ParameterMasterAgeWiseParams)
        {
            var ParameterAgeWiseMasterUpdate = _ParameterMasterAgeWise.Update(ParameterMasterAgeWiseParams);
            return Ok(ParameterAgeWiseMasterUpdate);

        }
        //----------------------------------------

        [HttpPost("PathologyCategoryMasterSave")]
        public IActionResult PathologyCategoryMasterSave(PathologyCategoryMasterParams pathCategoryParams)
        {
            var PathologyCategoryMasterSave = _PathologyCategoryMasterRep.Insert(pathCategoryParams);
            return Ok(PathologyCategoryMasterSave);

        }

        [HttpPost("PathologyCategoryMasterUpdate")]
        public IActionResult PathologyCategoryMasterUpdate(PathologyCategoryMasterParams pathCategoryParams)
        {
            var PathologyCategoryMasterUpdate = _PathologyCategoryMasterRep.Update(pathCategoryParams);
            return Ok(PathologyCategoryMasterUpdate);

        }
        //----------------------------------------

        [HttpPost("PathologyTemplateMasterSave")]
        public IActionResult PathologyTemplateMasterSave(PathologyTemplateMasterParams pathTemplateParams)
        {
            var PathologyTemplateMasterSave = _PathologyTemplateMasterRep.Insert(pathTemplateParams);
            return Ok(PathologyTemplateMasterSave);

        }

        [HttpPost("PathologyTemplateMasterUpdate")]
        public IActionResult PathologyTemplateMasterUpdate(PathologyTemplateMasterParams pathTemplateParams)
        {
            var PathologyTemplateMasterUpdate = _PathologyTemplateMasterRep.Update(pathTemplateParams);
            return Ok(PathologyTemplateMasterUpdate);

        }
        //----------------------------------------

        [HttpPost("PathologyTestMasterSave")]
        public IActionResult PathologyTestMasterSave(PathologyTestMasterParams pathTestParams)
        {
            var PathologyTestMasterSave = _PathologyTestMasterRep.Insert(pathTestParams);
            return Ok(PathologyTestMasterSave);

        }

        [HttpPost("PathologyTestMasterUpdate")]
        public IActionResult PathologyTestMasterUpdate(PathologyTestMasterParams pathTestParams)
        {
            var PathologyTestMasterUpdate = _PathologyTestMasterRep.Update(pathTestParams);
            return Ok(PathologyTestMasterUpdate);

        }
        //----------------------------------------

        [HttpPost("UnitSave")]
        public IActionResult UnitSave(UnitMasterParams unitParams)
        {
            var UnitMasterSave = _UnitMasterRep.Insert(unitParams);
            return Ok(UnitMasterSave);

        }

        [HttpPost("UnitUpdate")]
        public IActionResult UnitUpdate(UnitMasterParams unitParams)
        {
            var UnitMasterUpdate = _UnitMasterRep.Update(unitParams);
            return Ok(UnitMasterUpdate);

        }
        //----------------------------------------
    }
}
