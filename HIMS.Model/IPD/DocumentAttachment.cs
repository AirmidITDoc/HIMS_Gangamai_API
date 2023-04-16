using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class DocumentAttachment
    {
        public deleteDocument deleteDocument { get; set; }
        public List<InsertdocumentAttach> InsertdocumentAttach { get; set; }

    }

    public class deleteDocument
    {
        public int AdmId { get; set; } = 0;

    }
    public class InsertdocumentAttach
    {
        public long OPD_IPD_ID { get; set; } = 0;
        public long OPD_IPD_Type { get; set; } = 0;
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public string FilePathLocation { get; set; } = "";
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = "";

    }
}
