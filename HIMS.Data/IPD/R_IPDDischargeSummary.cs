using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPDDischargeSummary:GenericRepository,I_IPDDischargeSummary
    {

        public R_IPDDischargeSummary(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
    
        public String Insert(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DischargesummaryId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var disc = IPDDischargeSummaryParams.InsertIPDDischargSummary.ToDictionary();
            var DischargesummaryId=ExecNonQueryProcWithOutSaveChanges("insert_DischargeSummary_1", disc);
                       
            var disc3 = IPDDischargeSummaryParams.IPSMSInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DischargeSummary_1", disc3);


           /*  var disc4 = IPDischargeParams.InsertIPSMSTemplete.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("update_Admission_2", disc4);


            IPDDischargeSummaryParams.UpdateAdmisionDischargeSummary.AdmissionId = Convert.ToInt32(IPDDischargeSummaryParams.InsertIPDDischargSummary.AdmissionId);
            var disc2 = IPDDischargeSummaryParams.UpdateAdmisionDischargeSummary.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Admission_2", disc2);*/


            _unitofWork.SaveChanges();
            return DischargesummaryId;
        }

        public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams)
        {
          //  throw new NotImplementedException();

            var disc3 = IPDDischargeSummaryParams.UpdateIPDDischargSummary.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DischargeSummary_1", disc3);


            _unitofWork.SaveChanges();
            return true;
        }

        /*   public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams)
           {
               var disc = IPDDischargeSummaryParams.UpdateIPDDischargSummary.ToDictionary();
               ExecNonQueryProcWithOutSaveChanges("ps_Update_DischargeSummary_1", disc);
               _unitofWork.SaveChanges();
               return true;
           }*/
    }
}
