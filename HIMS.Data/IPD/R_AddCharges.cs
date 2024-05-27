using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_AddCharges : GenericRepository, I_Addcharges
    {
        public R_AddCharges(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool delete(AddChargesParams AddChargesParams)
        {
            // throw new NotImplementedException();
            var disc1 = AddChargesParams.DeleteCharges.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Delete_IPAddcharges", disc1);
            _unitofWork.SaveChanges();

            return true;
        }

        public bool Save(AddChargesParams addChargesParams)
        {
            // add AddCharges
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = addChargesParams.AddCharges.ToDictionary();
            dic.Remove("ChargeID");
            var ChargesId = ExecNonQueryProcWithOutSaveChanges("insert_IPAddCharges_1", dic, outputId);


           if (addChargesParams.AddCharges.IsPathology)
            {
                Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                PathParams.Add("PathDate", addChargesParams.AddCharges.ChargesDate);
                PathParams.Add("PathTime", addChargesParams.AddCharges.ChargesDate);
                PathParams.Add("OPD_IPD_Type", addChargesParams.AddCharges.OPD_IPD_Type);
                PathParams.Add("OPD_IPD_Id", addChargesParams.AddCharges.OPD_IPD_Id);
                PathParams.Add("PathTestID", addChargesParams.AddCharges.ServiceId);
                PathParams.Add("AddedBy", addChargesParams.AddCharges.AddedBy);
                PathParams.Add("ChargeID", ChargesId);
                PathParams.Add("IsCompleted", 0);
                PathParams.Add("IsPrinted", 0);
                PathParams.Add("IsSamplecollection",0);
                PathParams.Add("TestType", 0);

                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", PathParams);  
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
            //}

            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }
    }
}
