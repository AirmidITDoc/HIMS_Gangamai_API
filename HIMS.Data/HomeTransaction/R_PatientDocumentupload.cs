using HIMS.Common.Utility;
using HIMS.Model.HomeTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.HomeTransaction
{
   public class R_PatientDocumentupload :GenericRepository,I_PatientDocumentupload
    {
        public R_PatientDocumentupload(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string Save(PatientDocumentuploadParam PatientDocumentuploadParam)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PatientDocId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            foreach (var a in PatientDocumentuploadParam.InsertUploadDocument)
            {
                /*  var disc3 = _StudyUploadDocumentParams.InsertStudyUploadDocument.ToDictionary();
                  disc3.Remove("StudyDocId");
                  var No = ExecNonQueryProcWithOutSaveChanges("insert_StudyUploadDocument", disc3, outputId1);*/

            }


            var disc3 = PatientDocumentuploadParam.UpdateUploadDocument.ToDictionary();
            disc3.Remove("PatientDocId");
            var No = ExecNonQueryProcWithOutSaveChanges("Insert_T_PatientUploadDocument", disc3, outputId1);

            _unitofWork.SaveChanges();
            return No;
        }

        public bool Update(PatientDocumentuploadParam PatientDocumentuploadParam)
        {
            // throw new NotImplementedException();

            var disc3 = PatientDocumentuploadParam.UpdateUploadDocument.ToDictionary();
            var No = ExecNonQueryProcWithOutSaveChanges("Update_T_PatientUploadDocument", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
