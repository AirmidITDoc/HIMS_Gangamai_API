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
    public class R_MaterialConsumption : GenericRepository, I_MaterialConsumption
    {
        public R_MaterialConsumption(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public string Insert(MaterialConsumptionparam MaterialConsumptionparam)
        {
            // throw new NotImplementedException();
            var outputId = new SqlParameter
            {

                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@MaterialConsumptionId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var dic = MaterialConsumptionparam.MaterialconsumptionInsert.ToDictionary();
            dic.Remove("MaterialConsumptionId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_MaterialConsumption_1", dic, outputId);

            _unitofWork.SaveChanges();

            return Id;
        }

        public string ViewMaterialConsumptionReceipt(int MaterialConsumptionId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@MaterialConsumptionId", MaterialConsumptionId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptPrintMaterialConsumption", para);
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            //  string previousLabel = "";

            

            double T_TotalMrpAmount = 0, T_TotalLandAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: center;padding: 3px;height:10px;text-align:center;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                
                //items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["StoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["PerUnitMRPRate"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["MRPTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble()).Append("</td>");  
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["LandedTotalAmount"].ConvertToString()).Append("</td></tr>");



                T_TotalMrpAmount += dr["MRPTotalAmount"].ConvertToDouble();
                T_TotalLandAmount += dr["LandedTotalAmount"].ConvertToDouble(); 
                //T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                //T_TotalPaidAmount += dr["PaidAmount"].ConvertToDouble();
                //T_TotalBalAmount += dr["BalAmount"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{MaterialConsumptionId}}", Bills.GetColValue("MaterialConsumptionId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));

            
            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));

            html = html.Replace("{{T_TotalMrpAmount}}", T_TotalMrpAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalLandAmount}}", T_TotalLandAmount.To2DecimalPlace());
           
            return html;

        }
    }
}

