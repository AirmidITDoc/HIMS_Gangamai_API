using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_PHAdvanceRefund:GenericRepository,I_PHAdvanceRefund
    {
        public R_PHAdvanceRefund(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public String Insert(PharRefundofAdvanceParams pharRefundofAdvanceParams)
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
            var dic = pharRefundofAdvanceParams.InsertPharRefundofAdvance.ToDictionary();
            dic.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_T_PhAdvRefund_1", dic, outputId);


            pharRefundofAdvanceParams.UpdatePharAdvanceHeader.AdvanceId = Convert.ToInt32(pharRefundofAdvanceParams.InsertPharRefundofAdvance.AdvanceId);
            var disc2 = pharRefundofAdvanceParams.UpdatePharAdvanceHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_PhAdvanceHeader_1", disc2);

            foreach (var a in pharRefundofAdvanceParams.InsertPharRefundofAdvanceDetail)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_T_PHAdvRefundDetail_1", disc);
            }

            foreach (var a in pharRefundofAdvanceParams.UpdatePharAdvanceDetailBalAmount)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("update_T_PHAdvanceDetailBalAmount_1", disc);
            }

            var vPayment = pharRefundofAdvanceParams.InsertPharPayment.ToDictionary();
            vPayment["RefundId"] = RefundId;
            ExecNonQueryProcWithOutSaveChanges("insert_I_PHPayment_1", vPayment);

            _unitofWork.SaveChanges();
            return RefundId;
        }
    }
}
