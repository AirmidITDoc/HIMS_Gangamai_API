using HIMS.Model.Document;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Document
{
    public interface I_Document
    {
        public bool Insert(DocumentTypeMasterParams documentTypeMasterParams);
        public bool Update(DocumentTypeMasterParams documentTypeMasterParams);
    }
}
