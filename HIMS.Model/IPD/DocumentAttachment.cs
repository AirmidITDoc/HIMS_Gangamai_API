using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HIMS.Model.IPD
{
    public class DocumentAttachment
    {
        public List<DocumentAttachmentItem> Files { get; set; }
    }
    public class DocumentAttachmentItem
    {
        public string Id { get; set; }
        public long OPD_IPD_ID { get; set; }
        public long OPD_IPD_Type { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FilePathLocation { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IFormFile DocFile { get; set; }
    }
}
