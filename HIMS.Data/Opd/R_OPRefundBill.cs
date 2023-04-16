using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Opd
{ 
    public class R_OPRefundBill : GenericRepository, I_OPRefundBill
    {
        public R_OPRefundBill(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(OPRefundBillParams OPRefundBillParams)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = OPRefundBillParams.InsertRefund.ToDictionary();
            disc1.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_Refund_1", disc1, outputId1);


            foreach (var a in OPRefundBillParams.InsertOPRefundDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["RefundID"] = RefundId;
                disc2["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("insert_T_RefundDetails_1", disc2);
            }

            foreach (var a in OPRefundBillParams.Update_AddCharges_RefundAmount)
            {

                var disc3 = a.ToDictionary();
                disc3["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("Update_AddCharges_RefundAmt", disc3);
            }

          
           /* var disc4 = OPRefundBillParams.OP_DoctorShare_GroupWise_RefundOfBill.ToDictionary();
            disc4["RefundId"] = RefundId;
            ExecNonQueryProcWithOutSaveChanges("OP_DoctorShare_GroupWise_RefundOfBill", disc4);

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };*/

            var disc5 = OPRefundBillParams.InsertOPPayment.ToDictionary();
           // disc5.Remove("PaymentId");
            disc5["RefundId"] = RefundId;
            disc5["BillNo"] = OPRefundBillParams.InsertRefund.BillId;
            disc5["AdvanceId"] = OPRefundBillParams.InsertRefund.AdvanceId;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc5);

            _unitofWork.SaveChanges();      
            return RefundId;
        }
    }
}
