using HIMS.API.Utility;
using HIMS.Data.Document;
using HIMS.Data.Master.Prescription;
using HIMS.Model.Document;
using HIMS.Model.IPD;
using HIMS.Model.Master.Prescription;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly I_Document _IDocumentRep;
        private readonly IFileUtility _IFileUtility;
        public DocumentController(I_Document docTypeMaster, IFileUtility fileUtility)
        {
            this._IDocumentRep = docTypeMaster;
            this._IFileUtility = fileUtility;
        }
        [HttpPost("DocumentTypeSave")]
        public IActionResult DocumentTypeSave(DocumentTypeMasterParams documentTypeMasterParams)
        {

            var DocumentTypSave = _IDocumentRep.Insert(documentTypeMasterParams);
            return Ok(DocumentTypSave);

        }
        [HttpPost("DocumentTypeUpdate")]
        public IActionResult DocumentTypeUpdate(DocumentTypeMasterParams documentTypeMasterParams)
        {
            var DocumentTypUpdate = _IDocumentRep.Update(documentTypeMasterParams);
            return Ok(DocumentTypUpdate);
        }

        [HttpPost("PatientDocumentAttachment")]
        public async Task<IActionResult> PatientDocumentAttachmentAsync([FromForm] PatientDocumentAttachment patientDocumentAttachment)
        {
            foreach (PatientDocumentAttachmentItem objFile in patientDocumentAttachment.Files)
            {
                string NewFileName = objFile.PId + "_" + objFile.DocTypeId;
                string FileName = await _IFileUtility.UploadDocument(objFile.PatientDocFile, "PatientDocumentsFile\\" + objFile.PId, NewFileName);
                objFile.FilePath = FileName;
                objFile.FilePathLocation = FileName;
                objFile.FileName = objFile.PatientDocFile.FileName;
            }
            var res = _IDocumentRep.Save(patientDocumentAttachment.Files);
            return Ok(res.Select(x => new { x.Id, x.FileName }));
        }
        [HttpGet("Patient-Doc-Files")]
        public IActionResult DocFiles(int Id, int PId)
        {
            return Ok(_IDocumentRep.GetFiles(Id, PId).Select(x => new { x.Id, x.FileName }));
        }
        [HttpGet("Patient-Download-Files")]
        public async Task<IActionResult> DownloadFiles(int Id)
        {
            PatientDocumentAttachmentItem item = _IDocumentRep.GetFileById(Id);
            if (item == null)
            {
                return NotFound();
            }
            var fileData = await _IFileUtility.DownloadFile(item.FilePathLocation);
            return File(fileData.Item1, fileData.Item2, fileData.Item3);
        }

    }
}
