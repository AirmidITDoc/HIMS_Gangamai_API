using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace HIMS.Data.IPD
{
    public class R_IPInterimBill:GenericRepository,I_IPInterimBill
    {
        public R_IPInterimBill(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(IPInterimBillParams IPInterimBillParams)
        {
            var ChargesId = IPInterimBillParams.InterimBillChargesUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_InterimBillCharges_1", ChargesId);

           
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPInterimBillParams.InsertBillUpdateBillNo1.ToDictionary();
            disc1.Remove("BillNo");
            disc1.Remove("CashCounterId");
            var BillNo1 = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPInterimBillParams.BillDetailsInsert1)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo1;
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", disc);
            }

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = IPInterimBillParams.IPIntremPaymentInsert.ToDictionary();
            disc2["BillNo"]=BillNo1;
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc2);

            _unitofWork.SaveChanges();
            return BillNo1;
        }

        public String InsertCashCounter(IPInterimBillParams IPInterimBillParams)
        {
            var ChargesId = IPInterimBillParams.InterimBillChargesUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_InterimBillCharges_1", ChargesId);


            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPInterimBillParams.InsertBillUpdateBillNo1.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo1 = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_CashCounter_1", disc1, outputId1);

            foreach (var a in IPInterimBillParams.BillDetailsInsert1)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo1;
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", disc);
            }

            var outputId2 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc2 = IPInterimBillParams.IPIntremPaymentInsert.ToDictionary();
            disc2["BillNo"] = BillNo1;
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc2);

            _unitofWork.SaveChanges();
            return BillNo1;
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

            var Content = beforefloating + ' ' + " RUPEES" +  "  only";

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

        public string ViewIPInterimBillReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPD_DraftBillSummary_Print", para);
            string html = File.ReadAllText(htmlFilePath);

            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false;
            double T_NetAmount = 0;
            //int j = Bills.Rows.Count;
            int j = 0;


            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName").ToString());
            
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));

            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
           
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{OnlinePayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));
            
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{DiscComments}}", Bills.GetColValue("DiscComments"));
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{Doctorname}}", Bills.GetColValue("Doctorname"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

           html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ConvertToDouble() > 0 ? "table-row " : "none");


            html = html.Replace("{{chkdiscflag}}",Bills.GetColValue("ConcessionAmt").ConvertToDouble().ConvertToDouble() > 0 ? "table-row " : "none");

            string previousLabel = "";
            

            double T_TotalAmount = 0, F_TotalAmount=0, ChargesTotalamt=0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:18px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:right;font-weight:bold;\">")

                   .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                    T_TotalAmount = 0;

                    items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                }


                T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


                previousLabel = dr["GroupName"].ConvertToString();

                items.Append("<tr style=\"font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;font-weight:bold;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:20px;'><td colspan='5' style=\"border:1px solid #cccccc;border-bottom:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Amount</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                    //items.Append("<tr style='border:1px solid black;font-family:'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:20px;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    //.Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }



            }

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");


           
            return html;
        }
    }
}
