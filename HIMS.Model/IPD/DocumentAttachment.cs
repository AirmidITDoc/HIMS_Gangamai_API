using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HIMS.Model.IPD
{
    public class DocumentAttachment
    {
        public string Id { get; set; }
        public long OPD_IPD_ID { get; set; } = 0;
        public long OPD_IPD_Type { get; set; } = 0;
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string FilePathLocation { get; set; } = "";
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = "";
        public IFormFile DocFile { get; set; }
    }
}
