using HIMS.Model.HomeTransaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.HomeTransaction
{
   public interface I_PatientDocumentupload
    {
        public string Save(PatientDocumentuploadParam PatientDocumentuploadParam);
        public bool Update(PatientDocumentuploadParam PatientDocumentuploadParam);
    }
}
