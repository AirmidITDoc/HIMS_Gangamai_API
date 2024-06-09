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

     
        string I_IPRefundofAdvance.ViewIPRefundofAdvanceReceipt(int RefundId, string htmlFilePath, string htmlHeader)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPRefundofAdvancePrint", para);
          
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;



            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RefundNo}}", Bills.GetColValue("RefundNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));

            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AdmissinDate}}", Bills.GetColValue("AdmissinDate").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{RefundAmount}}", Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            
            html = html.Replace("{{RefundDate}}", Bills.GetColValue("RefundDate").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));

            string finalamt = conversion(Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            return html;
        }

        public string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            var afterfloating = ConvertNumbertoWords(Convert.ToInt64(j));

            // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + afterfloating + ' ' + " PAISE only";

            return Content;
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
           {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
           {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}
