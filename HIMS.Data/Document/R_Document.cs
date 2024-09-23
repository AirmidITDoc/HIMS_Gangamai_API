using HIMS.Common.Utility;
using HIMS.Model.Document;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Document
{
    public class R_Document : GenericRepository, I_Document
    {
        public R_Document(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(DocumentTypeMasterParams documentTypeMasterParams)
        {
            var disc = documentTypeMasterParams.UpdateDocumentTypeMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DocumentTypes_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(DocumentTypeMasterParams documentTypeMasterParams)
        {
            var disc = documentTypeMasterParams.InsertDocumentTypeMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_DocumentTypes_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
