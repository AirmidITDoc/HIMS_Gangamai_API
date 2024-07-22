using HIMS.Common.Utility;
using HIMS.Data.GSTReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using HIMS.Common.Utility;

namespace HIMS.Data.Opd
{
    public class R_GSTReport : GenericRepository, I_GSTReport
    {
       

        public R_GSTReport(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

              
        public string ViewSalesProfitSummaryReport(DateTime FromDate, DateTime ToDate,int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptSalesProfitReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedPrice"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_LandedPrice += dr["LandedPrice"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesProfitBillReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptSalesProfitReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedPrice"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_LandedPrice += dr["LandedPrice"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesProfitItemWiseSummaryReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptSalesProfitItemWiseReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_UnitMRP = 0, T_TotalLandedAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
             
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedPrice"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ProfitInPer"].ConvertToString()).Append("</td></tr>");



                T_LandedPrice += dr["LandedPrice"].ConvertToDouble();
                T_UnitMRP += dr["UnitMRP"].ConvertToDouble();
                T_TotalLandedAmount += dr["TotalLandedAmount"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalLandedAmount}}", T_TotalLandedAmount.To2DecimalPlace());
            html = html.Replace("{{T_UnitMRP}}", T_UnitMRP.To2DecimalPlace());
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }
      

       
    }
}





