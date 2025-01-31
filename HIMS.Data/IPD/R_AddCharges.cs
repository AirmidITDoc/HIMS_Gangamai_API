using HIMS.Common.Utility;
using HIMS.Model.CustomerInformation;
using HIMS.Model.IPD;
using HIMS.Model.Opd;
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

        public bool LabRequestSave(LabRequesChargesParams labRequesChargesParams)
        {
            // throw new NotImplementedException();
            var disc1 = labRequesChargesParams.LabRequestCharges.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Insert_LabRequest_Charges_1", disc1);
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
            dic.Remove("IsPackage");
            if (addChargesParams.AddCharges.IsPackage)
            {
                dic.Add("IsPackage", 0);
            }else
            {
                dic.Add("IsPackage", 0);
            }
            
            var vChargesId = ExecNonQueryProcWithOutSaveChanges("m_insert_IPAddCharges_1", dic, outputId);


           if (addChargesParams.AddCharges.IsPathology)
            {
                Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                PathParams.Add("PathDate", addChargesParams.AddCharges.ChargesDate);
                PathParams.Add("PathTime", addChargesParams.AddCharges.ChargeTime);
                PathParams.Add("OPD_IPD_Type", addChargesParams.AddCharges.OPD_IPD_Type);
                PathParams.Add("OPD_IPD_Id", addChargesParams.AddCharges.OPD_IPD_Id);
                PathParams.Add("PathTestID", addChargesParams.AddCharges.ServiceId);
                PathParams.Add("AddedBy", addChargesParams.AddCharges.AddedBy);
                PathParams.Add("ChargeID", vChargesId);
                PathParams.Add("IsCompleted", 0);
                PathParams.Add("IsPrinted", 0);
                PathParams.Add("IsSamplecollection",0);
                PathParams.Add("TestType", 0);

                ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportHeader_1", PathParams);  
            }

            if (addChargesParams.AddCharges.IsRadiology)
            {
                Dictionary<string, Object> RadParams = new Dictionary<string, object>();

                RadParams.Add("RadDate", addChargesParams.AddCharges.ChargesDate);
                RadParams.Add("RadTime", addChargesParams.AddCharges.ChargeTime);
                RadParams.Add("OPD_IPD_Type", addChargesParams.AddCharges.OPD_IPD_Type);
                RadParams.Add("OPD_IPD_Id", addChargesParams.AddCharges.OPD_IPD_Id);
                RadParams.Add("RadTestID", addChargesParams.AddCharges.ServiceId);
                RadParams.Add("AddedBy", addChargesParams.AddCharges.AddedBy);
                RadParams.Add("IsCancelled", 0);
                RadParams.Add("ChargeID", vChargesId);
                RadParams.Add("IsCompleted", 0);
                RadParams.Add("IsPrinted", 0);
                RadParams.Add("TestType", 0);

                ExecNonQueryProcWithOutSaveChanges("m_insert_RadiologyReportHeader_1", RadParams);
            }
            if (addChargesParams.AddCharges.IsPackage)
            {
                foreach (var obj in addChargesParams.ChargesIPPackageInsert)
                {
                    if (addChargesParams.AddCharges.ServiceId == obj.PackageId)
                    {
                        var disc6 = obj.ToDictionary();
                        disc6["PackageMainChargeID"] = vChargesId;
                        ExecNonQueryProcWithOutSaveChanges("m_insert_IPChargesPackages_1", disc6);
                    }
                }
            }

            //commit transaction
            _unitofWork.SaveChanges();

            return true;
        }

        //public bool InsertIPDPackageBill(AddChargesParameters AddChargesParameters)
        //{
        //    // add AddCharges
        //    var outputId = new SqlParameter
        //    {
        //        SqlDbType = SqlDbType.BigInt,
        //        ParameterName = "@ChargeID",
        //        Value = 0,
        //        Direction = ParameterDirection.Output
        //    };
        //    var dic = AddChargesParameters.SaveAddChargesParameters.ToDictionary();
        //    dic.Remove("ChargeID");
        //    dic.Remove("IsPackage");
        //    if (AddChargesParameters.SaveAddChargesParameters.IsPackage)
        //    {
        //        dic.Add("IsPackage", 0);
        //    }
        //    else
        //    {
        //        dic.Add("IsPackage", 0);
        //    }

        //    var vChargesId = ExecNonQueryProcWithOutSaveChanges("m_insert_IPAddCharges_1", dic, outputId);


           
        //    if (AddChargesParameters.SaveAddChargesParameters.IsPackage)
        //    {
        //        foreach (var obj in AddChargesParameters.ChargesIPPackageInsert)
        //        {
        //            if (AddChargesParameters.SaveAddChargesParameters.ServiceId == obj.PackageId)
        //            {
        //                var disc6 = obj.ToDictionary();
        //                disc6["PackageMainChargeID"] = vChargesId;
        //                ExecNonQueryProcWithOutSaveChanges("m_insert_IPChargesPackages_1", disc6);
        //            }
        //        }
        //    }

        //    //commit transaction
        //    _unitofWork.SaveChanges();

        //    return true;
        //}
        public bool InsertIPDPackageBill(AddChargesParameters AddChargesParameters)

        {

            // add AddCharges

            var outputId = new SqlParameter

            {

                SqlDbType = SqlDbType.BigInt,

                ParameterName = "@ChargeID",

                Value = 0,

                Direction = ParameterDirection.Output

            };

            var dic = AddChargesParameters.SaveAddChargesParameters.ToDictionary();

            dic.Remove("ChargeID");

            dic.Remove("IsPackage");

            if (AddChargesParameters.SaveAddChargesParameters.IsPackage)

            {

                dic.Add("IsPackage", 0);

            }

            else

            {

                dic.Add("IsPackage", 0);

            }

            var vChargesId = ExecNonQueryProcWithOutSaveChanges("m_insert_IPAddCharges_1", dic, outputId);


            if (AddChargesParameters.SaveAddChargesParameters.IsPathology)

            {

                Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                PathParams.Add("PathDate", AddChargesParameters.SaveAddChargesParameters.ChargesDate);

                PathParams.Add("PathTime", AddChargesParameters.SaveAddChargesParameters.ChargeTime);

                PathParams.Add("OPD_IPD_Type", AddChargesParameters.SaveAddChargesParameters.OPD_IPD_Type);

                PathParams.Add("OPD_IPD_Id", AddChargesParameters.SaveAddChargesParameters.OPD_IPD_Id);

                PathParams.Add("PathTestID", AddChargesParameters.SaveAddChargesParameters.ServiceId);

                PathParams.Add("AddedBy", AddChargesParameters.SaveAddChargesParameters.AddedBy);

                PathParams.Add("ChargeID", vChargesId);

                PathParams.Add("IsCompleted", 0);

                PathParams.Add("IsPrinted", 0);

                PathParams.Add("IsSamplecollection", 0);

                PathParams.Add("TestType", 0);

                ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportHeader_1", PathParams);

            }

            if (AddChargesParameters.SaveAddChargesParameters.IsRadiology)

            {

                Dictionary<string, Object> RadParams = new Dictionary<string, object>();

                RadParams.Add("RadDate", AddChargesParameters.SaveAddChargesParameters.ChargesDate);

                RadParams.Add("RadTime", AddChargesParameters.SaveAddChargesParameters.ChargeTime);

                RadParams.Add("OPD_IPD_Type", AddChargesParameters.SaveAddChargesParameters.OPD_IPD_Type);

                RadParams.Add("OPD_IPD_Id", AddChargesParameters.SaveAddChargesParameters.OPD_IPD_Id);

                RadParams.Add("RadTestID", AddChargesParameters.SaveAddChargesParameters.ServiceId);

                RadParams.Add("AddedBy", AddChargesParameters.SaveAddChargesParameters.AddedBy);

                RadParams.Add("IsCancelled", 0);

                RadParams.Add("ChargeID", vChargesId);

                RadParams.Add("IsCompleted", 0);

                RadParams.Add("IsPrinted", 0);

                RadParams.Add("TestType", 0);

                ExecNonQueryProcWithOutSaveChanges("m_insert_RadiologyReportHeader_1", RadParams);

            }

            //commit transaction

            _unitofWork.SaveChanges();

            return true;

        }



        public bool UpdateIPDPackageBill(AddChargesPara AddChargesPara)
        {
            // throw new NotImplementedException();
            var disc = AddChargesPara.AddCharge.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_IPAddCharges", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}

