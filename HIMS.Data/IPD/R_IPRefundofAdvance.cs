using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPRefundofAdvance:GenericRepository,I_IPRefundofAdvance
    {
        public R_IPRefundofAdvance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

      
        public String Insert(IPRefundofAdvanceParams IPRefundofAdvanceParams)
        {
            //throw new NotImplementedException();
           
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var dic = IPRefundofAdvanceParams.InsertIPRefundofAdvance.ToDictionary();
            dic.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_IPAdvRefund_1", dic, outputId);


            IPRefundofAdvanceParams.UpdateAdvanceHeader.AdvanceId = Convert.ToInt32(IPRefundofAdvanceParams.InsertIPRefundofAdvance.AdvanceId);
            var disc2 = IPRefundofAdvanceParams.UpdateAdvanceHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc2);
          

          
            var dic3 = IPRefundofAdvanceParams.InsertIPRefundofAdvanceDetail.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_AdvRefundDetail_1", dic3);

            IPRefundofAdvanceParams.UpdateAdvanceDetailBalAmount.AdvanceDetailID = IPRefundofAdvanceParams.InsertIPRefundofAdvanceDetail.AdvDetailId;
            IPRefundofAdvanceParams.UpdateAdvanceDetailBalAmount.RefundAmount = IPRefundofAdvanceParams.InsertIPRefundofAdvance.RefundAmount;
            IPRefundofAdvanceParams.UpdateAdvanceDetailBalAmount.BalanceAmount = IPRefundofAdvanceParams.UpdateAdvanceHeader.BalanceAmount;
            var disc4 = IPRefundofAdvanceParams.UpdateAdvanceDetailBalAmount.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetailBalAmount_1", disc4);

           // outputId.ParameterName = "@PaymentID";
            IPRefundofAdvanceParams.InsertPayment.BillNo = Convert.ToInt32(IPRefundofAdvanceParams.InsertIPRefundofAdvance.BillId);
            IPRefundofAdvanceParams.InsertPayment.AdvanceId = IPRefundofAdvanceParams.UpdateAdvanceHeader.AdvanceId;
            IPRefundofAdvanceParams.InsertPayment.RefundId = IPRefundofAdvanceParams.InsertIPRefundofAdvance.RefundId;
            var dic6 = IPRefundofAdvanceParams.InsertPayment.ToDictionary();
            //dic6.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", dic6);

            _unitofWork.SaveChanges();
            return RefundId;
        }

       
       }
}
