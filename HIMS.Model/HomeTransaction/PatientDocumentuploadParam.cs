using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.HomeTransaction
{
   public class PatientDocumentuploadParam
    {
        public List<InsertUploadDocument> InsertUploadDocument { get; set; }
        public UpdateUploadDocument UpdateUploadDocument { get; set; }

    }
    public class InsertUploadDocument
    {
        public int PatientDocId { get; set; }
        public int RegId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int CreatedBy { get; set; }
    }
    public class UpdateUploadDocument
    {
        public string Operation { get; set; }
        public int PatientDocId { get; set; }
        public int RegId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int UpdatedBy { get; set; }
    }

}