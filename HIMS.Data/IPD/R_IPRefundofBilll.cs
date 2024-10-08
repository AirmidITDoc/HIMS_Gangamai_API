﻿using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace HIMS.Data.IPD
{
    public class R_IPRefundofBilll:GenericRepository,I_IPRefundofBilll
    {
        public R_IPRefundofBilll(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public String Insert(IPRefundofBilllparams IPRefundofBilllparams)
        {
            //throw new NotImplementedException();
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = IPRefundofBilllparams.InsertIPRefundofNew.ToDictionary();
            dic.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("insert_Refund_1", dic, outputId);


            foreach (var a in IPRefundofBilllparams.InsertRefundDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["RefundID"] = RefundId;
                ExecNonQueryProcWithOutSaveChanges("insert_T_RefundDetails_1", disc2);
            }

             foreach (var a in IPRefundofBilllparams.UpdateAddChargesDetails)
            {
                var disc3 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_AddCharges_RefundAmt", disc3);
            }

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc5 = IPRefundofBilllparams.IPDInsertPayment.ToDictionary();
           // disc5.Remove("PaymentId");
            disc5["RefundId"] = RefundId;
            disc5["BillNo"] = IPRefundofBilllparams.InsertIPRefundofNew.BillId;
            disc5["AdvanceId"] = IPRefundofBilllparams.InsertIPRefundofNew.AdvanceId;
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc5);

            _unitofWork.SaveChanges();
            return RefundId;

        }


        //exec rptIPRefundofBillPrint 10268 
        string I_IPRefundofBilll.ViewIPRefundofBillReceipt(int RefundId, string htmlFilePath, string htmlHeader)
        {
           

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPRefundofBillPrint", para);
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
            html = html.Replace("{{Addedby}}", Bills.GetColValue("AddedBy"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AdmissinDate}}", Bills.GetColValue("AdmissinDate"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{RefundAmount}}", Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));

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

            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{RefundTime}}", Bills.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

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

