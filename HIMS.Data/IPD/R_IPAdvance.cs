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
    public class R_IPAdvance: GenericRepository,I_IPAdvance
    {
        public R_IPAdvance(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public String Insert(IPAdvanceParams IPAdvanceParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@AdvanceDetailID",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPAdvanceParams.AdvanceHeaderInsert.ToDictionary();
            disc1.Remove("AdvanceId");
            var AdvanceID = ExecNonQueryProcWithOutSaveChanges("insert_AdvanceHeader_1", disc1, outputId);

            IPAdvanceParams.AdvanceDetailInsert.AdvanceId = (int)Convert.ToInt64(AdvanceID);
            IPAdvanceParams.AdvanceDetailInsert.RefId = IPAdvanceParams.AdvanceHeaderInsert.RefId;
            IPAdvanceParams.AdvanceDetailInsert.OPD_IPD_Id = IPAdvanceParams.AdvanceHeaderInsert.OPD_IPD_Id;
            IPAdvanceParams.AdvanceDetailInsert.OPD_IPD_Type = IPAdvanceParams.AdvanceHeaderInsert.OPD_IPD_Type;
            IPAdvanceParams.AdvanceDetailInsert.AdvanceAmount = IPAdvanceParams.AdvanceHeaderInsert.AdvanceAmount;
            IPAdvanceParams.AdvanceDetailInsert.UsedAmount = IPAdvanceParams.AdvanceHeaderInsert.AdvanceUsedAmount;
            IPAdvanceParams.AdvanceDetailInsert.BalanceAmount = IPAdvanceParams.AdvanceHeaderInsert.BalanceAmount;


            var disc2 = IPAdvanceParams.AdvanceDetailInsert.ToDictionary();            
            disc2.Remove("AdvanceDetailID");
            var AdvanceDetailID = ExecNonQueryProcWithOutSaveChanges("insert_AdvanceDetail_1", disc2, outputId1);

            IPAdvanceParams.IPPaymentInsert.AdvanceId = (int)Convert.ToInt64(AdvanceDetailID);
            var disc3 = IPAdvanceParams.IPPaymentInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc3);


            _unitofWork.SaveChanges();
            return AdvanceDetailID;
        }




        public string ViewAdvanceReceipt(int AdvanceDetailID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
          
            para[0] = new SqlParameter("@AdvanceDetailID", AdvanceDetailID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPDAdvancePrint", para);
            string html = File.ReadAllText(htmlFilePath);
         
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;


            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{reason}}", Bills.GetColValue("reason"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceNo}}", Bills.GetColValue("AdvanceNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
         
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));



            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequeNo}}", Bills.GetColValue("ChequeNo").ConvertToString());
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CardBankName}}", Bills.GetColValue("CardBankName").ConvertToString());
            html = html.Replace("{{CardNo}}", Bills.GetColValue("CardNo").ConvertToString());
            html = html.Replace("{{BankName}}", Bills.GetColValue("BankName").ConvertToString());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("PatientType").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTNo}}", Bills.GetColValue("NEFTNo").ConvertToString());
            html = html.Replace("{{NEFTBankMaster}}", Bills.GetColValue("NEFTBankMaster").ConvertToString());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMTranNo}}", Bills.GetColValue("PayTMTranNo").ConvertToString());
            html = html.Replace("{{PayTMDate}}", Bills.GetColValue("PayTMDate").ConvertToDouble().To2DecimalPlace());

            


            html = html.Replace("{{chkcashflag}}", Bills.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkchequeflag}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkcardflag}}", Bills.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkneftflag}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkpaytmflag}}", Bills.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            string finalamt = conversion(Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() !=null ? "block" : "none");
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

            var Content = beforefloating + ' ' + " RUPEES" +  ' ' + "only";

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

        public string ViewAdvanceSummaryReceipt(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("[rptIPAdvanceSummary]", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_AdvanceAmount = 0, T_UsedAmount = 0, T_BalanceAmount = 0, T_RefundAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdvanceNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["UsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["RefundAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeTime"].ConvertToDateString()).Append(" </td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DiffTimeInHr"].ConvertToString()).Append("</td>");


                T_AdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();
                T_UsedAmount += dr["UsedAmount"].ConvertToDouble();
                T_BalanceAmount += dr["BalanceAmount"].ConvertToDouble();
                T_RefundAmount += dr["RefundAmount"].ConvertToDouble();


            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{T_AdvanceAmount}}", T_AdvanceAmount.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_UsedAmount}}", T_UsedAmount.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_BalanceAmount}}", T_BalanceAmount.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_RefundAmount}}", T_RefundAmount.ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName").ConvertToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ConvertToString());
            html = html.Replace("{{DOA}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm"));

            return html;
        }
    }
}
