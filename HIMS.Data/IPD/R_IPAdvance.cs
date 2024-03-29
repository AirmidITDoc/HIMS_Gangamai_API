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
        
           ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc3);


            _unitofWork.SaveChanges();
            return AdvanceDetailID;
        }




        public string ViewAdvanceReceipt(int AdvanceDetailID, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[1];
          
            para[0] = new SqlParameter("@AdvanceDetailID", AdvanceDetailID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDAdvancePrint", para);
            string html = File.ReadAllText(htmlFilePath);
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;


            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{reason}}", Bills.GetColValue("reason"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceNo}}", Bills.GetColValue("AdvanceNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));

            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));


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
