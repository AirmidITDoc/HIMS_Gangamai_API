using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HIMS.Model.Document
{
    public class DocumentTypeMasterParams
    {
        public InsertDocumentTypeMaster InsertDocumentTypeMaster { get; set; }
        public UpdateDocumentTypeMaster UpdateDocumentTypeMaster { get; set; }
    }
    public class InsertDocumentTypeMaster
    {
        public int? UpId { get; set; }
        public string DocType { get; set; }
        public string ShortCode { get; set; }
    }
    public class UpdateDocumentTypeMaster
    {
        public int? UpId { get; set; }
        public string DocType { get; set; }
        public string ShortCode { get; set; }
        public bool IsActive { get; set; }

    }
    public class PatientDocumentAttachment
    {
        public List<PatientDocumentAttachmentItem> Files { get; set; }
    }
    public class PatientDocumentAttachmentItem
    {
        public string Id { get; set; }
        public long PId { get; set; }
        public long DocTypeId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FilePathLocation { get; set; }
        public string DisplayName { get; set; }
        public IFormFile PatientDocFile { get; set; }
    }

}
