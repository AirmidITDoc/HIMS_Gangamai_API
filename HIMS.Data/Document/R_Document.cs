using HIMS.Common.Utility;
using HIMS.Model.Document;
using HIMS.Model.IPD;
using HIMS.Model.Master.Prescription;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

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

        public List<PatientDocumentAttachmentItem> Save(List<PatientDocumentAttachmentItem> patientDocumentAttachment)
        {
            foreach (var a in patientDocumentAttachment)
            {
                var outputId = new SqlParameter
                {
                    SqlDbType = SqlDbType.BigInt,
                    ParameterName = "@Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                var disc = a.ToDictionary();
                disc.Remove("PatientDocFile");
                disc.Remove("Id");
                a.Id = ExecNonQueryProcWithOutSaveChanges("insert_PatientDocumentMaster", disc, outputId).ToString();
            }
            _unitofWork.SaveChanges();
            return patientDocumentAttachment;
        }
        public List<PatientDocumentAttachmentItem> GetFiles(int Id, int PId)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@Id", Id);
            para[1] = new SqlParameter("@PId", PId);
            return GetList<PatientDocumentAttachmentItem>("SELECT * FROM [PatientDocumentMaster] WHERE Id=@Id AND PId=@PId", para);
        }
        public PatientDocumentAttachmentItem GetFileById(int Id)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Id", Id);
            return GetList<PatientDocumentAttachmentItem>("SELECT * FROM [PatientDocumentMaster] WHERE Id=@Id", para).FirstOrDefault();
        }
    }
}
