using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Opd;
using HIMS.Data.Opd;
using System.Data;
using System.Data.SqlClient;
using HIMS.Common.Utility;

namespace HIMS.Data.Opd
{
    public class R_PatientFeedback : GenericRepository, I_PatientFeedback
    {
        public R_PatientFeedback(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool Insert(PatientFeedbackParams patientFeedbackParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PatientFeedbackId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            //var disc = patientFeedbackParams.PatientFeedbackInsert.ToDictionary();
            //disc.Remove("CasePaperID");
            //var CasePaperID = ExecNonQueryProcWithOutSaveChanges("Insert_T_OPDCasePaper_1", disc, outputId);

            foreach (var a in patientFeedbackParams.PatientFeedbackInsert)
            {
                var disc1 = a.ToDictionary();
                disc1.Remove("PatientFeedbackId");
                ExecNonQueryProcWithOutSaveChanges("Insert_T_PatientFeedback", disc1, outputId);
            }

            _unitofWork.SaveChanges();
            return (true);
        }



    }
}
