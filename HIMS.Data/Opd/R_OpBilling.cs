using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_OpBilling :GenericRepository,I_OPbilling
    {
        public R_OpBilling(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(OPbillingparams OPbillingparams)
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


            var disc3 = OPbillingparams.InsertBillupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in OPbillingparams.ChargesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                disc5.Remove("ChargeID");
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_OPAddCharges_1", disc5, VarChargeID);

                // Dill Detail Table Insert 
                Dictionary<string, Object> OPBillDet = new Dictionary<string, object>();
                OPBillDet.Add("BillNo", BillNo);
                OPBillDet.Add("ChargesID", ChargeID);
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", OPBillDet);

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

                    ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", PathParams);
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

                    ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", PathParams);
                }
            }

            var disc7 = OPbillingparams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);
            
            
            // foreach (var a in OPbillingparams.InsertPathologyReportHeader)
            // {
            //     var disc1 = a.ToDictionary();
            //     //disc5["BillNo"] = BillNo;
            //     ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            // }

            // foreach (var a in OPbillingparams.InsertRadiologyReportHeader)
            // {
            //     var disc2 = a.ToDictionary();
            //    ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            // }



            // foreach (var a in OPbillingparams.OpBillDetailsInsert)
            //{
            //    var disc5 = a.ToDictionary();
            //    disc5["BillNo"] = BillNo;
            //    ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            // }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            //var disc6 = OPbillingparams.OPCalDiscAmountBill.ToDictionary();
            //disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            //ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

            //IPBillingParams.BillDetailsInsert.BillNo = (int)Convert.ToInt64(BillNo);



            _unitofWork.SaveChanges();
            return BillNo;
        }

    }
}
