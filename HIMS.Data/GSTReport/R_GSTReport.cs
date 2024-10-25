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
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

             
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GRNDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GrnNumber"].ConvertToString()).Append("</td>");
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
            double T_NetAmount = 0;


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



                T_NetAmount += dr["TotalAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
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
            var Bills = GetDataTableProc("RptSalesVatReport", para);


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
            var Bills = GetDataTableProc("RptSalesVatReport", para);


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

    }
}





