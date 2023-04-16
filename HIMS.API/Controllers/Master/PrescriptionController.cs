using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.Master.Prescription;
using HIMS.Model.Master.Prescription;

namespace HIMS.API.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        public readonly I_GenericMaster _GenericMasterRep;
        public readonly I_DrugMaster _DrugMasterRep;
        public readonly I_DoseMaster _DoseMasterRep;
        //public readonly I_CertificateMaster _CertificateMasterRep;
        public readonly I_InstructionMaster _InstructionMasterRep;
        //public readonly I_PrescriptionClassMaster _PrescriptionClassMasterRep; 
        public readonly I_PrescriptionTemplateMaster _PrescriptionTemplateMaster;
        
        public PrescriptionController(I_GenericMaster genericMaster,
                                      I_DrugMaster drugMaster,
                                      I_DoseMaster doseMaster,
                                      //I_CertificateMaster certificateMaster,
                                     // I_PrescriptionClassMaster  prescriptionClassMaster,
                                      I_InstructionMaster instructionMaster,I_PrescriptionTemplateMaster prescriptionTemplateMaster ) 
        {
            this._GenericMasterRep = genericMaster;
            this._DoseMasterRep = doseMaster;
            this._InstructionMasterRep = instructionMaster;
            this._DrugMasterRep = drugMaster;
            //  this._CertificateMasterRep = certificateMaster;
            //  this._PrescriptionClassMasterRep = prescriptionClassMaster;
            this._PrescriptionTemplateMaster = prescriptionTemplateMaster;

        }
        [HttpPost("GenericSave")]
        public IActionResult GenericSave(GenericMasterParams genericMasterParams)
        {
            var GenericSave = _GenericMasterRep.Insert(genericMasterParams);
            return Ok(GenericSave);

        }

        [HttpPost("GenericUpdate")]
        public IActionResult GenericUpdate(GenericMasterParams genericMasterParams)
        {
            var GenericUpdate = _GenericMasterRep.Update(genericMasterParams);
            return Ok(GenericUpdate);
        }
        //--------------------------------------------------------------------------------

        [HttpPost("DoseSave")]
        public IActionResult DoseSave(DoseMasterParams doseMasterParams)
        {
            var DoseSave = _DoseMasterRep.Insert(doseMasterParams);
            return Ok(DoseSave);

        }

        [HttpPost("DoseUpdate")]
        public IActionResult DoseUpdate(DoseMasterParams doseMasterParams)
        {
            var DoseUpdate = _DoseMasterRep.Update(doseMasterParams);
            return Ok(DoseUpdate);
        }
        //--------------------------------------------------------------------------------

        [HttpPost("InstructionSave")]
        public IActionResult InstructionSave(InstructionMasterParams instructionMasterParams)
        {
            var InstructionSave = _InstructionMasterRep.Insert(instructionMasterParams);
            return Ok(InstructionSave);

        }

        [HttpPost("InstructionUpdate")]
        public IActionResult InstructionUpdate(InstructionMasterParams instructionMasterParams)
        {
            var InstructionUpdate = _InstructionMasterRep.Update(instructionMasterParams);
            return Ok(InstructionUpdate);
        }
        //--------------------------------------------------------------------------------


        [HttpPost("DrugSave")]
        public IActionResult DurgSave(DrugMasterParams drugMasterParams)
        {
            var DrugSave = _DrugMasterRep.Insert(drugMasterParams);
            return Ok(DrugSave);

        }

        [HttpPost("DrugUpdate")]
        public IActionResult DrugUpdate(DrugMasterParams drugMasterParams)
        {
            var DrugUpdate = _DrugMasterRep.Update(drugMasterParams);
            return Ok(DrugUpdate);
        }
        //--------------------------------------------------------------------------------

        /* [HttpPost("CertificateSave")]
         public IActionResult CertificateSave(CertificateMasterParams certificateMasterParams)
         {
             var CertificateSave = _CertificateMasterRep.Insert(certificateMasterParams);
             return Ok(CertificateSave);

         }

         [HttpPost("CertificateUpdate")]
         public IActionResult CertificateUpdate(CertificateMasterParams certificateMasterParams)
         {
             var CertificateUpdate = _CertificateMasterRep.Update(certificateMasterParams);
             return Ok(CertificateUpdate);
         }*/
        //--------------------------------------------------------------------------------


        /*  [HttpPost("PrescriptionClassSave")]
          public IActionResult PrescriptionClassSave(PrescriptionClassMasterParams prescriptionClassMasterParams)
          {
              var PrescriptionClassSave = _PrescriptionClassMasterRep.Insert(prescriptionClassMasterParams);
              return Ok(PrescriptionClassSave);

          }

          [HttpPost("PrescriptionClassUpdate")]
          public IActionResult PrescriptionClassUpdate(PrescriptionClassMasterParams prescriptionClassMasterParams)
          {
              var PrescriptionClassUpdate = _PrescriptionClassMasterRep.Update(prescriptionClassMasterParams);
              return Ok(PrescriptionClassUpdate);
          }*/

        [HttpPost("PrescriptionTemplateMasterSave")]
        public IActionResult PrescriptionTemplateMasterSave(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams)
        {
            var PrescriptionClassSave = _PrescriptionTemplateMaster.Insert(PrescriptionTemplateMasterParams);
            return Ok(PrescriptionClassSave);

        }

        [HttpPost("PrescriptionTemplateMasterUpdate")]
        public IActionResult PrescriptionTemplateMasterUpdate(PrescriptionTemplateMasterParams PrescriptionTemplateMasterParams)
        {
            var PrescriptionClassUpdate = _PrescriptionTemplateMaster.Update(PrescriptionTemplateMasterParams);
            return Ok(PrescriptionClassUpdate);
        }

        //--------------------------------------------------------------------------------
    }
}
