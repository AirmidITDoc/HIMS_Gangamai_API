using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPPrescriptionReturn:GenericRepository,I_IPPrescriptionReturn
    {
        public R_IPPrescriptionReturn(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(IPPrescriptionReturnParams IPPrescriptionReturnParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PresReId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            

            var disc2 = IPPrescriptionReturnParams.IPPrescriptionReturnH.ToDictionary();
            disc2.Remove("PresReId");
            var PresReID = ExecNonQueryProcWithOutSaveChanges("Insert_T_IPPrescriptionReturnH_1", disc2, outputId);

            IPPrescriptionReturnParams.IPPrescriptionReturnD.PresReId=Convert.ToInt32( PresReID);
            var disc1 = IPPrescriptionReturnParams.IPPrescriptionReturnD.ToDictionary();
           // disc1.Remove("PresReId");
            ExecNonQueryProcWithOutSaveChanges("Insert_T_IPPrescriptionReturnD_1", disc1);

          
            _unitofWork.SaveChanges();
            return true;
        }

    }
}
