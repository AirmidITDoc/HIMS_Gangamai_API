using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPPrescription:GenericRepository,I_IPPrescription
    {
        public R_IPPrescription(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(IPPrescriptionParams IPPrescriptionParams)
        {
            var OP_IP_ID = IPPrescriptionParams.DeleteIP_Prescription.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_T_IP_Prescription", OP_IP_ID);

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@MedicalRecoredId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPPrescriptionParams.InsertIP_MedicalRecord.ToDictionary();
            disc1.Remove("MedicalRecoredId");
            var MedicalRecoredId = ExecNonQueryProcWithOutSaveChanges("insert_T_IPMedicalRecord_1", disc1, outputId1);

            foreach (var a in IPPrescriptionParams.InsertIP_Prescription)
            {
                var disc2 = a.ToDictionary();
                disc2["IPMedID"] = MedicalRecoredId;
                ExecNonQueryProcWithOutSaveChanges("insert_IPPrescription_1", disc2);
            }

                      

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
