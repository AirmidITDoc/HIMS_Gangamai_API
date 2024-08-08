using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_OPBillingCredit :GenericRepository,I_OPBillingCredit
    {
        public R_OPBillingCredit(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string Insert(OPBillingCreditparam OPBillingCreditparam)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillDetailId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var VarChargeID = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = OPBillingCreditparam.InsertBillcreditupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            disc3.Remove("CashCounterId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_1", disc3, outputId1);

            foreach (var a in OPBillingCreditparam.ChargesDetailCreditInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                disc5.Remove("ChargeID");
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_OPAddCharges_1", disc5, VarChargeID);

                // Dill Detail Table Insert 
                Dictionary<string, Object> OPBillDet = new Dictionary<string, object>();
                OPBillDet.Add("BillNo", BillNo);
                OPBillDet.Add("ChargesID", ChargeID);
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", OPBillDet);

                if (a.IsPathology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("PathDate", a.ChargesDate);
                    PathParams.Add("PathTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("PathTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("IsSamplecollection", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportHeader_1", PathParams);
                }
                if (a.IsRadiology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("RadDate", a.ChargesDate);
                    PathParams.Add("RadTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("RadTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("IsCancelled", 0);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("m_insert_RadiologyReportHeader_1", PathParams);
                }
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public string InsertCreditCashCounter(OPBillingCreditparam OPBillingCreditparam)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillDetailId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var VarChargeID = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = OPBillingCreditparam.InsertBillcreditupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_CashCounter_1", disc3, outputId1);

            foreach (var a in OPBillingCreditparam.ChargesDetailCreditInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                disc5.Remove("ChargeID");
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_OPAddCharges_1", disc5, VarChargeID);

                // Dill Detail Table Insert 
                Dictionary<string, Object> OPBillDet = new Dictionary<string, object>();
                OPBillDet.Add("BillNo", BillNo);
                OPBillDet.Add("ChargesID", ChargeID);
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", OPBillDet);

                if (a.IsPathology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("PathDate", a.ChargesDate);
                    PathParams.Add("PathTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("PathTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("IsSamplecollection", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportHeader_1", PathParams);
                }
                if (a.IsRadiology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("RadDate", a.ChargesDate);
                    PathParams.Add("RadTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("RadTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("IsCancelled", 0);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("m_insert_RadiologyReportHeader_1", PathParams);
                }
            }

            _unitofWork.SaveChanges();
            return BillNo;
        }
    }
}
