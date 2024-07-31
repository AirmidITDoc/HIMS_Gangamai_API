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
        public readonly I_ParameterMasterAgeWise _ParameterMaster;

        public PathologyMasterController(I_PathParameterMaster PathParameterMasterParams,
                                   I_ParameterMasterAgeWise parameterMasterAgeWise,
                                   I_PathologyCategoryMaster pathologyCategoryMaster,
                                   I_PathologyTemplateMaster pathologyTemplateMaster,
                                   I_UnitMaster unitMaster,
                                   I_PathologyTestMaster pathologyTestMaster,
                                   I_ParameterMasterAgeWise parameterMaster
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


        //--------------------------------------------------------------------------------

        [HttpPost("ParameterAgeWiseMasterSave")]
        public IActionResult ParameterAgeWiseMasterSave(PathParameterMasterParams pathParameterMasterParams)
        {
            var ParameterAgeWiseMasterSave = _ParameterMasterAgeWise.Insert(pathParameterMasterParams);
            return Ok(ParameterAgeWiseMasterSave);

        }

        [HttpPost("ParameterAgeWiseMasterUpdate")]
        public IActionResult ParameterAgeWiseMasterUpdate(PathParameterMasterParams pathParameterMasterParams)
        {
            var ParameterAgeWiseMasterUpdate = _ParameterMasterAgeWise.Update(pathParameterMasterParams);
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
        public IActionResult PathologyTemplateMasterSave(PathologyTemplateMasterParams PathologyTemplateMasterParams)
        {
            var Id = _PathologyTemplateMasterRep.Insert(PathologyTemplateMasterParams);
            return Ok(Id);

        }



        [HttpPost("PathologyTemplateMasterUpdate")]
        public IActionResult PathologyTemplateMasterUpdate(PathologyTemplateMasterParams pathTemplateParams)
        {
            var Id = _PathologyTemplateMasterRep.Update(pathTemplateParams);
            return Ok(Id);

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


//Del_M_PathParaRangeMaster
//Delete_DescriptiveParameterMaster_1
//Insert_ParameterRangeWithAgeMaster_1
//M_Insert_M_PathologyTemplateMaster
//M_Update_M_PathologyTemplateMaster

//PTestMaster----
//Delete_M_PathTestDetailMaster
//Delete_M_PathTemplateDetails