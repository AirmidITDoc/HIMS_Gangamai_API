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
    public class R_InsertIPDraft :GenericRepository, I_InsertIPDraft
    {
        public R_InsertIPDraft(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

      
        public String Insert(InsertIPDraftParams InsertIPDraftParams)
        {
            //throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DRBNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc = InsertIPDraftParams.IPIntremdraftbillInsert.ToDictionary();
            disc.Remove("DRBNo");
            var DRNo = ExecNonQueryProcWithOutSaveChanges("insert_DRBill_1", disc, outputId);


            foreach (var a in InsertIPDraftParams.InterimBillDetailsInsert)
            {
                var disc1 = a.ToDictionary();
                disc1["DRNo"] = DRNo;
                ExecNonQueryProcWithOutSaveChanges("insert_T_DRBillDet_1", disc1);
            }


            _unitofWork.SaveChanges();
            return DRNo;
        }

        public string ViewIPDraftBillReceipt(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            double T_NetAmount = 0;
            string previousLabel = "";
            String Label = "";


            var Bills = GetDataTableProc("rptIPDDraftBillPrintSummary", para);

          


            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++;
            //    if (i == 1 || Label != previousLabel)
            //    {
            //        j = 1;
            //        Label = dr["GroupName"].ConvertToString();
            //        items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
            //    }
            //    previousLabel = dr["GroupName"].ConvertToString();

            //    if (Label == previousLabel)
            //    {

            //        i++;
            //        items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
            //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
            //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
            //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
            //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
            //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

            //        T_NetAmount += dr["TotalAmt"].ConvertToDouble();
            //        j++;
            //    }


            //}


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:15px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                previousLabel = dr["GroupName"].ConvertToString();
                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["TotalAmt"].ConvertToDouble();
                    j++;
                }

               
            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));


            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ToString());
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName").ToString());
            html = html.Replace("{{AgeYear}}", Bills.GetDateColValue("AgeYear").ToString());
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName").ToString());
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName").ToString());
            html = html.Replace("{{RefDoctorName}}", Bills.GetColValue("RefDoctorName").ToString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName").ToString());
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName").ToString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName").ToString());
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode").ToString());


            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));


            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

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
