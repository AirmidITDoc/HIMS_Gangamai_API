﻿using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IP_Settlement_Process:GenericRepository,I_IP_Settlement_Process
    {
        public R_IP_Settlement_Process(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public String Insert(IP_Settlement_Processparams IP_Settlement_Processparams)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IP_Settlement_Processparams.IPPaymentCreditUpdate.ToDictionary();
            disc1["BillNo"] = IP_Settlement_Processparams.UpdateIpBill.BillNo;
            disc1.Remove("PaymentId");
            var paymentid = ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_New_1", disc1, outputId1);

            var disc = IP_Settlement_Processparams.UpdateIpBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_BillBalAmount_1", disc);


            foreach (var a in IP_Settlement_Processparams.IPsettlementAdvanceDetailUpdate)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc2);
            }


            var disc4 = IP_Settlement_Processparams.IPsettlementAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);

            //commit transaction
            _unitofWork.SaveChanges();
            return paymentid;

        }


        public string ViewSettlementReceipt(int PaymentId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@PaymentId", PaymentId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDPaymentReceiptPrint", para);
          
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;



            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));

            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString());
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{ReceiptNo}}", Bills.GetColValue("ReceiptNo"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));


            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{OnlinePayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));


            string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
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
