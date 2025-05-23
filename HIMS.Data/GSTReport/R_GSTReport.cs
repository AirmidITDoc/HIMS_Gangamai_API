﻿using HIMS.Common.Utility;
using HIMS.Data.GSTReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using HIMS.Common.Utility;
using System.Linq;

namespace HIMS.Data.Opd
{
    public class R_GSTReport : GenericRepository, I_GSTReport
    {
       

        public R_GSTReport(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewPurchaseGSTSummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptPurchaseGSTSummaryReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_UnitMRP = 0, T_TotalLandedAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatApplicableAmount"].ConvertToString()).Append("</td></tr>");




                T_TotalAmount += dr["VatApplicableAmount"].ConvertToDouble();
                
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
           
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesProfitDetailDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptDoctorWise_PatientWise_Report", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            double T_UnitMRP = 0,T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0, profitamount = 0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(profitamount.ToString()).Append("</td></tr>");

                    profitamount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                //profitamount = profitamount;
                //T_NetPayableAmt = T_NetPayableAmt + 1;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SalesId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lable"].ConvertToString()).Append("</td>");
            
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["profitamount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(profitamount.ToString()).Append("</td></tr>");


                }

                T_TotalAmount += dr["NetAmount"].ConvertToDouble();
                //T_UnitMRP += dr["UnitMRP"].ConvertToDouble();
                //T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
                T_LandedPrice += dr["TotalLandedAmount"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["profitamount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_UnitMRP}}", T_UnitMRP.To2DecimalPlace());
            //html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }
        public string ViewSalesProfitSummaryDoctorWiseReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptDoctorWise_Summary_Report", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lable"].ConvertToString()).Append("</td>");

                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["profitamount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["NetAmount"].ConvertToDouble();
               
                T_LandedPrice += dr["TotalLandedAmount"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["profitamount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesProfitSummaryReport(DateTime FromDate, DateTime ToDate,int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptSalesProfitReportSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));


            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_DiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
                T_LandedPrice += dr["TotalLandedAmount"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{T_DiscAmount}}", T_DiscAmount.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());
            html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesProfitBillReport(DateTime FromDate, DateTime ToDate, int StoreId, int RegId,string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@RegId", RegId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptSalesProfitReportDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0; 


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_DiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
                T_LandedPrice += dr["TotalLandedAmount"].ConvertToDouble();
                T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_DiscAmount}}", T_DiscAmount.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());
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
            var Bills = GetDataTableProc("m_rptSalesProfitItemWiseReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmount = 0, T_LandedPrice = 0, T_ItemWiseProfitAmount = 0, T_UnitMRP = 0, T_TotalLandedAmount = 0, T_DiscAmount = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
             
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedPrice"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble()).Append("</td>");
            
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemWiseProfitAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ProfitInPer"].ConvertToString()).Append("</td></tr>");



                T_DiscAmount += dr["DiscAmount"].ConvertToDouble();

                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
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
            html = html.Replace("{{T_DiscAmount}}", T_DiscAmount.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }

        public string ViewPurchaseGSTReportSupplierWiseGST(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("PurchaseVatAmountWiseSupplier", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0; 


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PTR"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DISCOUNT AMOUNT"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TOTAL BILL AMOUNT"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TOTAL BILL AMOUNT"].ConvertToDouble();
                //T_UnitMRP += dr["UnitMRP"].ConvertToDouble();
                //T_TotalLandedAmount += dr["TotalLandedAmount"].ConvertToDouble();
                //T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                //T_ItemWiseProfitAmount += dr["ItemWiseProfitAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            //html = html.Replace("{{T_UnitMRP}}", T_UnitMRP.To2DecimalPlace());
            //html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            //html = html.Replace("{{T_LandedPrice}}", T_LandedPrice.To2DecimalPlace());
            //html = html.Replace("{{T_ItemWiseProfitAmount}}", T_ItemWiseProfitAmount.To2DecimalPlace());

            return html;

        }

        public string ViewPurchaseGSTReportSupplierWiseWithoutGST(DateTime FromDate, DateTime ToDate, int StoreID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreID", StoreID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("PurchaseVatAmount_SupplierWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PTR"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DISCOUNT AMOUNT"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CR/DR AMOUNT"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TOTAL BILL AMOUNT"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TOTAL BILL AMOUNT"].ConvertToDouble();
              
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }



        public string ViewPurchaseGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptPurchaseReturnGSTReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_CGSTAmt = 0, T_SGSTAmt = 0, T_IGSTAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

             
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGST"].ConvertToString()).Append("</td>");
        
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
              
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
              
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");



                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_NetAmount += dr["TotalAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }


        public string ViewPurchaseGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptPurchaseVatReportSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


     
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");
             

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
             
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatApplicableAmount"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["VatApplicableAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }


        public string ViewPurchaseRetumGSTReportDateWise(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptPurchaseReturnGSTReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNReturnDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGST"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");

              
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                           items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TotalAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }



        public string ViewPurchaseReturnGSTReportSummary(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptPurchaseReturnGSTReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_CGSTAmt = 0, T_SGSTAmt = 0, T_IGSTAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNReturnDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGST"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");


                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");


                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
               
                T_NetAmount += dr["TotalAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }

        public string ViewSalesGSTReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptSalesGSTPatientWiseReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount =0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
              
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PTR"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRPAmount"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["MRPAmount"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }

        public string ViewSalesGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptSalesGSTDateWiseReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");

                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRPAmount"].ConvertToDouble()).Append("</td></tr>");


                T_NetAmount += dr["MRPAmount"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }

         public string ViewSalesReturnGSTReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReturnVatReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0, T_VatAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SalesReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PTR"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td></tr>");


                T_VatAmount += dr["VatAmount"].ConvertToDouble();
                T_NetAmount += dr["DiscAmount"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_VatAmount}}", T_VatAmount.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;
        }

        public string ViewSalesReturnGSTDateWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReturnVatReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");

                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRPAmount"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["MRPAmount"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }



        public string ViewHSNCodeWiseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("hsnCodeWiseSalesReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Descrption"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableValue"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TaxableValue"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }


        public string ViewGSTRZAPurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptGSTR2APurchase", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceValue"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTPer"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableValue"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TaxableValue"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                //T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            //html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }



        public string ViewGSTR2ASupplierWisePurchaseReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptGSTR2APurchase", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_SGSTAmt = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["InvoiceValue"].ConvertToString()).Append("</td>");
             
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableValue"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td></tr>");



                T_NetAmount += dr["TaxableValue"].ConvertToDouble();
                T_IGSTAmt += dr["IGSTAmt"].ConvertToDouble();
                T_SGSTAmt += dr["SGSTAmt"].ConvertToDouble();
                T_CGSTAmt += dr["CGSTAmt"].ConvertToDouble();
                //T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_IGSTAmt}}", T_IGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.To2DecimalPlace());
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.To2DecimalPlace());
            //html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }
        public string ViewSalesGSTSummaryConsolidated(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
           

            var Bills = GetDataTableProc("GSTSummaryCommanReport", para);



            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double TaxableAmount = 0, CGSTAmt = 0, SGSTAmt = 0, GrossAmount = 0, T_TaxableAmount = 0, T_CGSTAmt = 0, T_SGSTAmt = 0;
            double T_GrossAmount = 0, DocAmtOP = 0, HospitalAmtOP = 0;
            double NetAmountIP = 0, DocAmtIP = 0, HospitalAmtIP = 0;
            string previousLabel = "";

            var sortedBills = Bills.AsEnumerable()
                .OrderBy(dr => dr["lbl"].ToString())
                
                .ToList();

            foreach (DataRow dr in sortedBills)
            {
                i++; j++;

                if (previousLabel != dr["lbl"].ToString())
                {
                    if (previousLabel != "")
                    {
                        items.Append("<tr><td colspan='6' style='border-top:2px solid black;'></td></tr>");
                        items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> Taxable Total Amt</td><td>").Append(TaxableAmount.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(DocAmtOP.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(HospitalAmtOP.ToString("0.00")).Append("</td></tr>");

                        items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> CGST Total Amt</td><td>").Append(CGSTAmt.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(DocAmtIP.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(HospitalAmtIP.ToString("0.00")).Append("</td></tr>");
                        items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> SGST Total Amt</td><td>").Append(SGSTAmt.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(DocAmtIP.ToString("0.00")).Append("</td>");
                        //items.Append("<td>").Append(HospitalAmtIP.ToString("0.00"))
                        items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'>  Gross Total Amt</td><td>").Append(GrossAmount.ToString("0.00")).Append("</td>")
                            .Append("</td></tr>");

                       
                    }

                    items.Append("<tr style='font-weight:bold; font-size:24px;'><td colspan='6'>").Append(dr["lbl"].ToString()).Append("</td></tr>");
                }

                if (dr["lbl"].ToString() == "Sales GST")
                {
                    TaxableAmount += Convert.ToDouble(dr["TaxableAmount"]);
                    CGSTAmt += Convert.ToDouble(dr["CGSTAmt"]);
                    SGSTAmt += Convert.ToDouble(dr["SGSTAmt"]);
                    GrossAmount += Convert.ToDouble(dr["GrossAmount"]);
                }
                else if (dr["lbl"].ToString() == "Sales Returun GST")
                {
                    TaxableAmount += Convert.ToDouble(dr["TaxableAmount"]);
                    CGSTAmt += Convert.ToDouble(dr["CGSTAmt"]);
                    SGSTAmt += Convert.ToDouble(dr["SGSTAmt"]);
                    GrossAmount += Convert.ToDouble(dr["GrossAmount"]);
                }
                else if (dr["lbl"].ToString() == "Sales Net Summary ")
                {
                    TaxableAmount += Convert.ToDouble(dr["TaxableAmount"]);
                    CGSTAmt += Convert.ToDouble(dr["CGSTAmt"]);
                    SGSTAmt += Convert.ToDouble(dr["SGSTAmt"]);
                    GrossAmount += Convert.ToDouble(dr["GrossAmount"]);
                }

                items.Append("<tr><td>").Append(i).Append("</td>");
                items.Append("<td style='font-size:19px;'>").Append(dr["GSTPer"].ToString()).Append("</td>");
                //items.Append("<td style='font-size:19px;'>").Append(dr["GroupName"].ToString()).Append("</td>");
                items.Append("<td style='font-size:19px;'>").Append(Convert.ToDouble(dr["TaxableAmount"]).ToString("0.00")).Append("</td>");
                items.Append("<td style='font-size:19px;'>").Append(Convert.ToDouble(dr["CGSTAmt"]).ToString("0.00")).Append("</td>");
                items.Append("<td style='font-size:19px;'>").Append(Convert.ToDouble(dr["SGSTAmt"]).ToString("0.00")).Append("</td>");
                items.Append("<td style='font-size:19px;'>").Append(Convert.ToDouble(dr["GrossAmount"]).ToString("0.00")).Append("</td></tr>");

                previousLabel = dr["lbl"].ToString();

                T_TaxableAmount += Convert.ToDouble(dr["TaxableAmount"]);
                T_CGSTAmt += Convert.ToDouble(dr["CGSTAmt"]);
                T_SGSTAmt += Convert.ToDouble(dr["SGSTAmt"]);
                T_GrossAmount += Convert.ToDouble(dr["GrossAmount"]);
            }

            if (!string.IsNullOrEmpty(previousLabel))
            {
                items.Append("<tr><td colspan='6' style='border-top:2px solid black;'></td></tr>");
                items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> Taxable Total Amt</td><td>").Append(TaxableAmount.ToString("0.00")).Append("</td></tr>");
                items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> CGST Total Amt</td><td>").Append(CGSTAmt.ToString("0.00")).Append("</td></tr>");
                items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> SGST Total Amt</td><td>").Append(SGSTAmt.ToString("0.00")).Append("</td></tr>");
                items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> Gross Total Amt</td><td>").Append(GrossAmount.ToString("0.00")).Append("</td></tr>");
                //items.Append("<td>").Append(TaxableAmount.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(CGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(SGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(GrossAmount.ToString("0.00")).Append("</td></tr>");

                //items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> Total Amt</td><td>").Append(NetAmountIP.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(TaxableAmount.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(CGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(SGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(GrossAmount.ToString("0.00")).Append("</td></tr>");
                //items.Append("<tr style='font-weight:bold; font-size:20px;'><td colspan='3'> Total Amt</td><td>").Append(NetAmountIP.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(TaxableAmount.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(CGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(SGSTAmt.ToString("0.00")).Append("</td>");
                //items.Append("<td>").Append(GrossAmount.ToString("0.00")).Append("</td></tr>");
            }

            // Replace the placeholders with actual totals and the generated items
            html = html.Replace("{{T_TaxableAmount}}", T_TaxableAmount.ToString("0.00"));
            html = html.Replace("{{T_CGSTAmt}}", T_CGSTAmt.ToString("0.00"));
            html = html.Replace("{{T_SGSTAmt}}", T_SGSTAmt.ToString("0.00"));
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.ToString("0.00"));
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }


        public string ViewGSTB2CSReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("GST_B2CS_Report", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_TaxableAmount = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Stype"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Place_Of_Supply"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTPer"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableAmount"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td></tr>");



                T_TaxableAmount += dr["TaxableAmount"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
               
                //T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            html = html.Replace("{{T_TaxableAmount}}", T_TaxableAmount.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }
        public string ViewGSTB2GSReportConsolidated(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("GST_B2CS_Report", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0, T_IGSTAmt = 0, T_TaxableAmount = 0, T_CGSTAmt = 0, T_GrossAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center;font-size: 17px; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Stype"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Place_Of_Supply"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GSTPer"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TaxableAmount"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrossAmount"].ConvertToDouble()).Append("</td></tr>");



                T_TaxableAmount += dr["TaxableAmount"].ConvertToDouble();
                T_GrossAmount += dr["GrossAmount"].ConvertToDouble();

                //T_GrossAmount += dr["GrossAmount"].ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TaxableAmount}}", T_TaxableAmount.To2DecimalPlace());
            html = html.Replace("{{T_GrossAmount}}", T_GrossAmount.To2DecimalPlace());

            return html;

        }
    }
}





