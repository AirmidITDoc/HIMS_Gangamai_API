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
            var BillNo1 = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPInterimBillParams.BillDetailsInsert1)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo1;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
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
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc2);

            _unitofWork.SaveChanges();
            return BillNo1;
        }



        //public string ViewIPInterimBillReceipt(int BillNo, string htmlFilePath, string htmlHeaderFilePath)
        //{
        //    // throw new NotImplementedException();

           

        //    SqlParameter[] para = new SqlParameter[1];

        //    para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
        //    string html = File.ReadAllText(htmlFilePath);
        //    string htmlHeader = File.ReadAllText(htmlHeaderFilePath);

        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{HospitalHeader}}", htmlHeader);
        //    StringBuilder items = new StringBuilder("");
        //    int i = 0,j=0;
        //    double T_NetAmount = 0;
        //    string previousLabel = "";
        //    String Label = "";

        //    var Bills = GetDataTableProc("rptIPDInterimBill", para);
         
           

                    
        //    //foreach (DataRow dr in Bills.Rows)
        //    //{
        //    //    i++;
        //    //    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(i).Append("</td>");
        //    //    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
        //    //    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
        //    //    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
        //    //    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
        //    //    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

        //    //    T_NetAmount += dr["NetAmount"].ConvertToDouble();
        //    //}


        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;
        //        if (i == 1 || Label != previousLabel)
        //        {
        //            j = 1;
        //            Label = dr["GroupName"].ConvertToString();
        //            items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
        //        }
             

        //        if (Label == previousLabel)
        //        {
                    
        //            i++;
        //            items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(i).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
        //            items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");
        //            j++;
        //            T_NetAmount += dr["NetAmount"].ConvertToDouble();
        //        }

        //        previousLabel = dr["GroupName"].ConvertToString();
        //    }


        //    html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ConvertToString());
        //    html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo").ConvertToString());
        //    html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
        //    html = html.Replace("{{Age}}", Bills.GetDateColValue("Age").ConvertToString());
        //    html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName").ConvertToString());
        //    html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate"));
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
        //    html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));


        //    html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
        //    html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
        //    //     html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));
        //    // html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt"));
        //    //html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount"));
        //    //html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount"));
        //    //html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount"));
        //    //html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount"));
        //    //html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount"));
        //    html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
        //    html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
        //    html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt"));



        //    html = html.Replace("{{Items}}", items.ToString());

        //    html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
        //    string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace().ToString());
        //    html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


        //    return html;
        //}


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

        public string ViewIPInterimBillReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDInterimBill", para);
            string html = File.ReadAllText(htmlFilePath);

            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false;
            double T_NetAmount = 0;
            int j = Bills.Rows.Count;



            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetDateColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));

            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
            
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            string previousLabel = "";
            String Label = "";


         
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["GroupName"].ConvertToString();

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }


            }
            // T_NetAmount = Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace();

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            return html;
        }
    }
}
