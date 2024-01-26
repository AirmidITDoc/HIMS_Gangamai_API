using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

     
        string I_IPRefundofAdvance.ViewIPRefundofAdvanceReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPRefundofAdvancePrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;



            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RefundNo}}", Bills.GetColValue("RefundNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));

            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissinDate}}", Bills.GetColValue("AdmissinDate"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount"));
            html = html.Replace("{{RefundAmount}}", Bills.GetColValue("RefundAmount"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("MobileNo"));
            
            html = html.Replace("{{RefundDate}}", Bills.GetColValue("RefundDate").ConvertToDateString());
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;
        }
    }
}
