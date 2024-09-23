using HIMS.Data.Document;
using HIMS.Data.Master.Prescription;
using HIMS.Model.Document;
using HIMS.Model.Master.Prescription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly I_Document _IDocumentRep;
        public DocumentController(I_Document docTypeMaster)
        {
            this._IDocumentRep = docTypeMaster;
        }
        [HttpPost("DocumentTypSave")]
        public IActionResult DocumentTypSave(DocumentTypeMasterParams documentTypeMasterParams)
        {

            var DocumentTypSave = _IDocumentRep.Insert(documentTypeMasterParams);
            return Ok(DocumentTypSave);

        }
        [HttpPost("DocumentTypUpdate")]
        public IActionResult DocumentTypUpdate(DocumentTypeMasterParams documentTypeMasterParams)
        {
            var DocumentTypUpdate = _IDocumentRep.Update(documentTypeMasterParams);
            return Ok(DocumentTypUpdate);
        }
    }
}
