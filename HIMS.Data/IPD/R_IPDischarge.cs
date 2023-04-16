using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPDischarge:GenericRepository,I_IPDischarge
    {
        public readonly SqlCommand command;

        public R_IPDischarge(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

         public String Insert(IPDischargeParams IPDischargeParams)
        {
            //throw new NotImplementedException();
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DischargeId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = IPDischargeParams.InsertIPDDischarg.ToDictionary();
            dic.Remove("DischargeId");
            var DischargeId = ExecNonQueryProcWithOutSaveChanges("insert_Discharge_1", dic, outputId);


            IPDischargeParams.UpdateAdmission.AdmissionID = Convert.ToInt32(IPDischargeParams.InsertIPDDischarg.AdmissionId);
            var disc2 = IPDischargeParams.UpdateAdmission.ToDictionary();
           ExecNonQueryProcWithOutSaveChanges("update_Admission_3", disc2);

           // IPDischargeParams.UpdateDischargeSummary.DischargeId = Convert.ToInt32(DischargeId);
         //   IPDischargeParams.UpdateDischargeSummary.AdmissionId = IPDischargeParams.UpdateAdmission.AdmissionId;

          //  var disc3 = IPDischargeParams.UpdateDischargeSummary.ToDictionary();
           // ExecNonQueryProcWithOutSaveChanges("update_DischargeSummary_1", disc3);


           // var disc4 = IPDischargeParams.InsertIPSMSTemplete.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("Insert_IPSMSTemplete_1", disc4);
            
            _unitofWork.SaveChanges();
            return DischargeId;

        }

        public bool Update(IPDischargeParams IPDischargeParams)
        {
            // new NotImplementedException();

            var disc4 = IPDischargeParams.UpdateIPDDischarg.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Discharge_1", disc4);


            //IPDischargeParams.UpdateAdmission.AdmissionID = Convert.ToInt32(IPDischargeParams.UpdateIPDDischarg.AdmissionId);
            var disc2 = IPDischargeParams.UpdateAdmission.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_3", disc2);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}

