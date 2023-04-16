using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPBilling:GenericRepository,I_IPBilling
    {
        public R_IPBilling(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(IPBillingParams IPBillingParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            
            var disc1 = IPBillingParams.InsertBillUpdateBillNo.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPBillingParams.BillDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
            }

            IPBillingParams.Cal_DiscAmount_IPBill.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingParams.Cal_DiscAmount_IPBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingParams.AdmissionIPBillingUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_AdmissionforIPBilling", AdmissionID);

            var disc7 = IPBillingParams.IPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);


             IPBillingParams.IPBillBalAmount.BillNo = (int)Convert.ToInt64(BillNo);
             var disc2 = IPBillingParams.IPBillBalAmount.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingParams.IPAdvanceDetailUpdate)
            {
                var disc = a.ToDictionary();
              //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc);
            }

                      
            var disc4 = IPBillingParams.IPAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;



           /* var outputId1 = new SqlParameter
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

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId ",
                Value = 0,
                Direction = ParameterDirection.Output
            };



            foreach (var a in IPBillingParams.InsertPathologyReportHeader)
            {
                var disc1 = a.ToDictionary();
                //disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            }

            foreach (var a in IPBillingParams.InsertRadiologyReportHeader)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            }

            var disc3 = IPBillingParams.InsertBillupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in IPBillingParams.OpBillDetailsInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            var disc6 = IPBillingParams.OPCalDiscAmountBill.ToDictionary();
            disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

            //IPBillingParams.BillDetailsInsert.BillNo = (int)Convert.ToInt64(BillNo);
            var disc7 = IPBillingParams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);

            _unitofWork.SaveChanges();
            return true;*/
        }

       
    }
}