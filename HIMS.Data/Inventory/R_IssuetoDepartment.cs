using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Data.Inventory;
using HIMS.Common.Utility;
using System.Data;
using System.Data.SqlClient;
using HIMS.Model.Inventory;
using System.IO;

namespace HIMS.Data.Inventory
{
    public class R_IssuetoDepartment : GenericRepository, I_IssuetoDepartment
    {
        public R_IssuetoDepartment(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String InsertIssuetoDepartment(IssuetoDepartmentParams issuetoDepartmentParams)
        {
            var vIssueId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@IssueId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = issuetoDepartmentParams.InsertIssuetoDepartmentHeader.ToDictionary();
            disc3.Remove("IssueId");
            var IssueId = ExecNonQueryProcWithOutSaveChanges("m_Insert_IssueToDepartmentHeader_1_New", disc3, vIssueId);

            foreach (var a in issuetoDepartmentParams.InsertIssuetoDepartmentDetail)
            {
                var disc5 = a.ToDictionary();
                disc5["IssueId"] = IssueId;
                ExecNonQueryProcWithOutSaveChanges("m_insert_IssueToDepartmentDetails_1", disc5);

            }
            foreach (var a in issuetoDepartmentParams.updateissuetoDepartmentStock)
            {
                var disc5 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_upd_T_Curstk_issdpt_1", disc5);

            }

            _unitofWork.SaveChanges();
            return IssueId;
        }

        public string ViewIssuetoDeptIssuewise(int IssueId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
          
            para[0] = new SqlParameter("@IssueId", IssueId) { DbType = DbType.Int64};

            var Bills = GetDataTableProc("rptPrintIssueToDepartment", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IssueQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/Mm/yyyy")).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["VatPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }
            T_TotalNETAmount = Math.Round(T_TotalNETAmount);
            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.ToString());
            html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            
             html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));


            html = html.Replace("{{IssueNo}}", Bills.GetColValue("IssueNo"));

            html = html.Replace("{{IssueTime}}", Bills.GetColValue("IssueTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));

            html = html.Replace("{{ToStreName}}", Bills.GetColValue("ToStreName"));


            string finalamt = NumberToWords(T_TotalNETAmount.ToInt());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            return html;
        }

        public string ViewReturnfrdeptdatewise(DateTime FromDate, DateTime ToDate, int FromStoreId, int ToStoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@FromStoreId", FromStoreId) { DbType = DbType.String };
            para[3] = new SqlParameter("@ToStoreId", ToStoreId) { DbType = DbType.String };
           
            var Bills = GetDataTableProc("rptReturnFromDepDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalLRateAmount = 0, T_TotalBalancepay = 0, T_TotalMRPAmount = 0, T_TotalPurchase = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ReturnTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["LandedRateTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["MRPTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PurchaseTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["Remark"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["AddedBy"].ConvertToString()).Append("</td></tr>");


                T_TotalLRateAmount += dr["LandedRateTotalAmount"].ConvertToDouble();
                T_TotalMRPAmount += dr["MRPTotalAmount"].ConvertToDouble();

                T_TotalPurchase += dr["PurchaseTotalAmount"].ConvertToDouble();
               // T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalLRateAmount}}", T_TotalLRateAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalMRPAmount}}", T_TotalMRPAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalPurchase}}", T_TotalPurchase.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }

        public string ViewReturnfromDeptReturnIdwise(int ReturnId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@ReturnId", ReturnId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptReturnFromDepartment", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ReturnTime"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PurchaseTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["UnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["UnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["TotalLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ReturnQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["RemainingQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IssueTime"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                //T_TotalVatAmount += dr["VatPercentage"].ConvertToDouble();
                //T_TotalNETAmount += dr["LandedTotalAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            //html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

          
            return html;
        }



        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
