using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pharmacy
{
    public  class R_MaterialAcceptance : GenericRepository, I_MaterialAcceptance
    {
        public R_MaterialAcceptance(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool UpdateMaterialAcceptance(MaterialAcceptParams materialAcceptParams)
        {
            var vMaterialAcceptUdpate = materialAcceptParams.MaterialAcceptIssueHeader.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_AcceptMaterial_Store_1", vMaterialAcceptUdpate);
            
            foreach (var a in materialAcceptParams.MaterialAcceptIssueDetails)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_update_AcceptMaterialIssueDet_1", disc1);
            }
            
            var vMaterialAcceptStockUpdate = materialAcceptParams.MaterialAcceptStockUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_AcceptMaterialStock_1", vMaterialAcceptStockUpdate);

            _unitofWork.SaveChanges();
            return true;
        }

        public bool UpdateStockToMainStock(MaterialAcceptParams materialAcceptParams)
        {
            var vUpdateStockToMainStock = materialAcceptParams.UpdateStockToMainStock.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_StockToMainStore_1", vUpdateStockToMainStock);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewMaterialReceivedfrDept(int IssueId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@IssueId", IssueId) { DbType = DbType.Int64 };

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
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
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
