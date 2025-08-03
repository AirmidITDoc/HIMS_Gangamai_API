using HIMS.Data.Master.Radiology;
using HIMS.Model.Master.Radiology;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class RadiologyMasterController : ControllerBase
    {
        public readonly I_CategoryMaster _CategoryMasterRep;
        public readonly I_RadiologyTemplateMaster _RadiologyTemplateMasterRep;
        public readonly I_RadiologyTestMaster _RadiologyTestMasterRep;


        public RadiologyMasterController(I_CategoryMaster categoryMaster, 
                                   I_RadiologyTemplateMaster radiologyTemplateMaster,
                                   I_RadiologyTestMaster radiologyTestMaster
                                 )
        {
            this._CategoryMasterRep = categoryMaster;
            this._RadiologyTemplateMasterRep = radiologyTemplateMaster;
            this._RadiologyTestMasterRep = radiologyTestMaster;

        }

        [HttpPost("CategorySave")]
        public IActionResult CategorySave(CategoryMasterParams categoryMasterParams)
        {
            var CategorySave = _CategoryMasterRep.Insert(categoryMasterParams);
            return Ok(CategorySave);

        }

        [HttpPost("CategoryUpdate")]
        public IActionResult CategoryUpdate(CategoryMasterParams categoryMasterParams)
        {
            var CategoryUpdate = _CategoryMasterRep.Update(categoryMasterParams);
            return Ok(CategoryUpdate);
        }
        //----------------------------------------
        [HttpPost("RadiologyTemplateMasterSave")]
        public IActionResult RadiologyTemplateMasterSave(RadiologyTemplateMasterParams rtMasterParams)
        {
            var RadiologyTemplateMasterSave = _RadiologyTemplateMasterRep.Insert(rtMasterParams);
            return Ok(RadiologyTemplateMasterSave);

        }

        [HttpPost("RadiologyTemplateMasterUpdate")]
        public IActionResult RadiologyTemplateMasterUpdate(RadiologyTemplateMasterParams rtMasterParams)
        {
            var RadiologyTemplateMasterUpdate = _RadiologyTemplateMasterRep.Update(rtMasterParams);
            return Ok(RadiologyTemplateMasterUpdate);

        }
        //----------------------------------------
        //----------------------------------------
        [HttpPost("RadiologyTestMasterSave")]
        public IActionResult RadiologyTestMasterSave(RadiologyTestMasterParams rtMasterParams)
        {
            var RadiologyTestMasterSave = _RadiologyTestMasterRep.Insert(rtMasterParams);
            return Ok(RadiologyTestMasterSave);

        }

        [HttpPost("RadiologyTestMasterUpdate")]
        public IActionResult RadiologyTestMasterUpdate(RadiologyTestMasterParams rtMasterParams)
        {
            var RadiologyTestMasterUpdate = _RadiologyTestMasterRep.Update(rtMasterParams);
            return Ok(RadiologyTestMasterUpdate);

        }
        //----------------------------------------
    }
}
