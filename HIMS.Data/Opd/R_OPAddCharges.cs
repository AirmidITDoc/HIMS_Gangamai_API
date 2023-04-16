using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OPAddCharges : GenericRepository, I_OPAddCharges
    {
        public R_OPAddCharges(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }



        public bool Save(OPDAddchargesparams OPAddChargesParams)
        {
            // add AddCharges
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            foreach (var a in OPAddChargesParams.OPDAddChargesInsert)
            {
                var dic = a.ToDictionary();
                dic.Remove("ChargeID");
                var ChargesId = ExecNonQueryProcWithOutSaveChanges("insert_IPAddCharges_1", dic, outputId);

            }

            /* if (OPAddChargesParams.IsPathology)
                 {
                     Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                     PathParams.Add("PathDate", OPAddChargesParams.ChargesDate);
                     PathParams.Add("PathTime", OPAddChargesParams.ChargesDate);
                     PathParams.Add("OPD_IPD_Type", OPAddChargesParams.OPD_IPD_Type);
                     PathParams.Add("OPD_IPD_Id", OPAddChargesParams.OPD_IPD_Id);
                     PathParams.Add("PathTestID", OPAddChargesParams.ServiceId);
                     PathParams.Add("AddedBy", OPAddChargesParams.AddedBy);
                     PathParams.Add("ChargeID", ChargeID);
                     PathParams.Add("IsCompleted", 0);
                     PathParams.Add("IsPrinted", 0);
                     PathParams.Add("IsSamplecollection", 0);
                     PathParams.Add("TestType", 0);

                     ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", PathParams);
                 //}
             }



             //if (addChargesParams.IsRadiology)
             //{
             //    Dictionary<string, Object> RadParams = new Dictionary<string, object>();

             //    RadParams.Add("RadDate", addChargesParams.ChargesDate);
             //    RadParams.Add("RadTime", addChargesParams.ChargesDate);
             //    RadParams.Add("OPD_IPD_Type", addChargesParams.OPD_IPD_Type);
             //    RadParams.Add("OPD_IPD_Id", addChargesParams.OPD_IPD_Id);
             //    RadParams.Add("RadTestID", addChargesParams.ServiceId);
             //    RadParams.Add("AddedBy", addChargesParams.AddedBy);
             //    RadParams.Add("IsCancelled", 0);
             //    RadParams.Add("ChargeID", ChargesId);
             //    RadParams.Add("IsCompleted", 0);
             //    RadParams.Add("IsPrinted", 0);
             //    RadParams.Add("IsSamplecollection", 0);
             //    RadParams.Add("TestType", 0);

             //    ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", RadParams);
             //}*/

            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }

    }
}
    