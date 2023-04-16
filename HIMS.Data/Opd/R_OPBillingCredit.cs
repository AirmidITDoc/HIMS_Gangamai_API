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
            // throw new NotImplementedException();
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

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId ",
                Value = 0,
                Direction = ParameterDirection.Output
            };



            foreach (var a in OPBillingCreditparam.InsertPathologyReportHeadercredit)
            {
                var disc1 = a.ToDictionary();
                //disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            }

            foreach (var a in OPBillingCreditparam.InsertRadiologyReportHeadercredit)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            }

            var disc3 = OPBillingCreditparam.InsertBillcreditupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in OPBillingCreditparam.OpBillDetailscreditInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            var disc6 = OPBillingCreditparam.OPCalDiscAmountBillcredit.ToDictionary();
            disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

          

            _unitofWork.SaveChanges();
            return BillNo;
        }

    }
}
