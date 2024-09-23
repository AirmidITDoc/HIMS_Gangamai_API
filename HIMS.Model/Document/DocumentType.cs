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
}
