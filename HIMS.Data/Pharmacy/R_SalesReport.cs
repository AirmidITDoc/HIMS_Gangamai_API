using HIMS.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_SalesReport:GenericRepository,I_SalesReport
    {
        public R_SalesReport(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewSalesCreditReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@CreditReasonId", CreditReasonId) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("RptPharmacyCreditReport", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\" font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress")); 

            return html;

        }



        public string ViewSalesTaxReceipt(int SalesID, int OP_IP_Type, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@SalesID", SalesID) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptSalesPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            double TotalGST = 0, T_HTotalAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_HTotalAmount += dr["TotalAmount"].ConvertToDouble();
            }

            TotalGST = Bills.GetColValue("CGSTAmt").ConvertToDouble() + Bills.GetColValue("SGSTAmt").ConvertToDouble() + Bills.GetColValue("IGSTAmt").ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{TotalGST}}", TotalGST.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{HospitalMobileNo}}", Bills.GetColValue("HospitalMobileNo"));
            html = html.Replace("{{HospitalEmailId}}", Bills.GetColValue("HospitalEmailId"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));

            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            html = html.Replace("{{HTotalAmount}}", T_HTotalAmount.To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount"));

            html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount"));
            html = html.Replace("{{RoundOff}}", Bills.GetColValue("RoundOff"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{GSTIN}}", Bills.GetColValue("GSTIN"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));

            html = html.Replace("{{SalesNo}}", Bills.GetColValue("SalesNo"));
           // html = html.Replace("{{Date}}", Bills.GetColValue("Date"));

            html = html.Replace("{{Date}}", Bills.GetColValue("Time").ConvertToDateString("dd/M/yyyy hh:mm tt"));

            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));



            return html;
        }


        public string ViewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@AddedBy", AddedBy) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReport", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalNETAmount = 0, T_TotalDiscAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;


                items.Append("<tr style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;

        }


        public string ViewSalesReportPatientWise(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@AddedBy", AddedBy) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("RptSalesReport", para);
            string html = File.ReadAllText(htmlFilePath);
          
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ExtAddress"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                ;
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;

        }
        public string ViewSalesReturnSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReturnReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();
              
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            return html;
        }


        public string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReturnReport", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 14px;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

              
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;

        }


        public string ViewSalesReturnReceipt(int SalesReturnId, int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@SalesID", SalesReturnId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rptSalesReturnPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double TotalGST = 0, T_HTotalAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-size: 14px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/mm/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_HTotalAmount += dr["TotalAmount"].ConvertToDouble();
            }

            TotalGST = Bills.GetColValue("CGSTAmt").ConvertToDouble() + Bills.GetColValue("SGSTAmt").ConvertToDouble() + Bills.GetColValue("IGSTAmt").ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{TotalGST}}", TotalGST.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{HospitalMobileNo}}", Bills.GetColValue("HospitalMobileNo"));
            html = html.Replace("{{HospitalEmailId}}", Bills.GetColValue("HospitalEmailId"));

            html = html.Replace("{{OP_IP_Number}}", Bills.GetColValue("OP_IP_Number"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));

            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            html = html.Replace("{{HTotalAmount}}", T_HTotalAmount.To2DecimalPlace());
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount"));

            html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{GSTIN}}", Bills.GetColValue("GSTIN"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));

            html = html.Replace("{{SalesReturnNo}}", Bills.GetColValue("SalesReturnNo"));
            html = html.Replace("{{Date}}", Bills.GetColValue("Date").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));



            return html;
        }


        public string ViewSalesReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, int AddedBy, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@AddedBy", AddedBy) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkdiscflag = false;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

            return html;


        }

        public string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesDailyCollection", para);
         

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;



            double G_SalesBillAmount = 0, G_SalesDiscAmount = 0, G_SalesNetAmount = 0, G_SalesPaidAmount = 0, G_SalesBalAmount = 0, G_SalesCashAmount = 0, G_SalesCardAmount = 0, G_SalesAmount = 0, G_SalesChequeAmount = 0, G_SalesTotalNEFTAmount = 0, G_SalesOnlineAmount = 0;
            double G_SalesReturnBillAmount = 0, G_SalesReturnDiscAmount = 0, G_SalesReturnNetAmount = 0, G_SalesReturnPaidAmount = 0, G_SalesReturnBalAmount = 0, G_SalesReturnCashAmount = 0, G_SalesReturnCardAmount = 0, G_SalesReturnChequeAmount = 0, G_SalesReturnNEFTAmount = 0, G_SalesReturnOnlineAmount = 0;
            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;
            double TotalCollectionSales =0,TotalCollectionSalesReturn = 0, FinalTotal=0,F_TotalColl=0;


            foreach (DataRow dr1 in Bills.Rows)
            {

              
                if (dr1["Label"].ConvertToString() == "Sales")
                {

                    //G_SalesBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    //G_SalesDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    //G_SalesNetAmount += dr["NetAmount"].ConvertToDouble();
                    //G_SalesPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    //G_SalesBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesCashAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_SalesCardAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_SalesChequeAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_SalesTotalNEFTAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_SalesOnlineAmount += dr1["PayTMAmount"].ConvertToDouble();

                }

                if (dr1["Label"].ConvertToString() == "Sales Return")
                {

                    //G_SalesReturnBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    //G_SalesReturnDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    //G_SalesReturnNetAmount += dr["NetAmount"].ConvertToDouble();
                    //G_SalesReturnPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    //G_SalesReturnBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesReturnCashAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_SalesReturnCardAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_SalesReturnChequeAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_SalesReturnNEFTAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_SalesReturnOnlineAmount += dr1["PayTMAmount"].ConvertToDouble();

                }
            }


                foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Label"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;
                    //items.Append("<tr style='font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    // .Append("Total Cash").Append(" </td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total Card").Append("</td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total Cheque").Append("</td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total NEFTPay").Append("</td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total OnlinePay").Append("</td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total Adv.Used").Append("</td><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">")
                    // .Append("Total BalAmt").Append("</td></tr>");


                    items.Append("<tr style='border:1px solid black;font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='4' style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");
                }
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                G_BalanceAmount += dr["BalanceAmount"].ConvertToDouble();
                previousLabel = dr["Label"].ConvertToString();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                T_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                T_BalanceAmount += dr["BalanceAmount"].ConvertToDouble();


                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                // items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["UserName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                //FinalTotal = TotalCollectionSales.ConvertToDouble() - TotalCollectionSalesReturn.ConvertToDouble();

                T_TotalBillAmount = G_SalesBillAmount.ConvertToDouble() - G_SalesReturnBillAmount.ConvertToDouble();
                T_TotalDiscAmount = G_SalesDiscAmount.ConvertToDouble() - G_SalesReturnDiscAmount.ConvertToDouble();
                T_TotalNETAmount = G_SalesNetAmount.ConvertToDouble() - G_SalesReturnNetAmount.ConvertToDouble();
                T_TotalPaidAmount = G_SalesPaidAmount.ConvertToDouble() - G_SalesReturnPaidAmount.ConvertToDouble();
                T_TotalBalAmount = G_SalesBalAmount.ConvertToDouble() - G_SalesReturnBalAmount.ConvertToDouble();
                T_TotalCashAmount = G_SalesCashAmount.ConvertToDouble() - G_SalesReturnCashAmount.ConvertToDouble();
                T_TotalCardAmount = G_SalesCardAmount.ConvertToDouble() - G_SalesReturnCardAmount.ConvertToDouble();
                T_TotalChequeAmount = G_SalesChequeAmount.ConvertToDouble() - G_SalesReturnChequeAmount.ConvertToDouble();
                T_TotalNEFTAmount = G_SalesTotalNEFTAmount.ConvertToDouble() - G_SalesReturnNEFTAmount.ConvertToDouble();
                T_TotalOnlineAmount = G_SalesOnlineAmount.ConvertToDouble() - G_SalesReturnOnlineAmount.ConvertToDouble();
                //F_TotalColl = T_TotalCashAmount.ConvertToDouble() + T_TotalCardAmount.ConvertToDouble() + T_TotalChequeAmount.ConvertToDouble() + T_TotalNEFTAmount.ConvertToDouble() + T_TotalOnlineAmount.ConvertToDouble();



                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {


                    items.Append("<tr style='font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid black;'><td colspan='4' style=\"border: 1px solid #d4c3c3;text-align:center;padding: 6px;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border: 1px solid #d4c3c3; text-align: center;padding: 6px;\">")
                    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">")
                    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">")
                    .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">")
                    .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">")
                    .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3;text-align: center; padding: 6px;\">")
                    .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;



                    items.Append("<tr style='border:1px solid black;font-weight:bold;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='4' style=\"border: 1px solid #d4c3c3;text-align:center; padding: 6px;\">Sales - Sales Return</td><td style=\"border: 1px solid #d4c3c3;text-align:center; padding: 6px;\">")
                         .Append(T_TotalCashAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(T_TotalCardAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalNEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalOnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(FinalTotal.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append("</td></tr>");


                }
            }

          


            TotalCollection = T_TotalCashAmount.ConvertToDouble() + T_TotalCardAmount.ConvertToDouble() + T_TotalChequeAmount.ConvertToDouble() + T_TotalNEFTAmount.ConvertToDouble() + T_TotalOnlineAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNeftpay}}", T_NEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTmpay}}", T_PayTMAmount.To2DecimalPlace());
            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_BalanceAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            return html;
        }


        public string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            
            var Bills = GetDataTableProc("m_rptSalesDailyColSummary", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            
            double TotalCollection = 0, TotalCollectionSales = 0, TotalCollectionSalesReturn = 0, FinalTotal = 0, F_TotalColl = 0;

            
            double G_SalesBillAmount = 0, G_SalesDiscAmount = 0, G_SalesNetAmount = 0, G_SalesPaidAmount = 0, G_SalesBalAmount = 0, G_SalesCashAmount = 0, G_SalesCardAmount = 0, G_SalesAmount = 0, G_SalesChequeAmount = 0, G_SalesTotalNEFTAmount = 0, G_SalesOnlineAmount = 0;
            double G_SalesReturnBillAmount = 0, G_SalesReturnDiscAmount = 0, G_SalesReturnNetAmount = 0, G_SalesReturnPaidAmount = 0, G_SalesReturnBalAmount = 0, G_SalesReturnCashAmount = 0, G_SalesReturnCardAmount = 0, G_SalesReturnChequeAmount = 0, G_SalesReturnNEFTAmount = 0, G_SalesReturnOnlineAmount = 0;
            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++;
                if (dr["Label"].ConvertToString() == "Sales")
                {

                    G_SalesBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    G_SalesDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    G_SalesNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_SalesPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    G_SalesBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesCashAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesCardAmount += dr["CardPay"].ConvertToDouble();
                    G_SalesChequeAmount += dr["ChequePay"].ConvertToDouble();
                    G_SalesTotalNEFTAmount += dr["NEFTPay"].ConvertToDouble();
                    G_SalesOnlineAmount += dr["OnlinePay"].ConvertToDouble();


                }


                if (dr["Label"].ConvertToString() == "Sales Return")
                {

                    G_SalesReturnBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    G_SalesReturnDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    G_SalesReturnNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_SalesReturnPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    G_SalesReturnBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesReturnCashAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesReturnCardAmount += dr["CardPay"].ConvertToDouble();
                    G_SalesReturnChequeAmount += dr["ChequePay"].ConvertToDouble();
                    G_SalesReturnNEFTAmount += dr["NEFTPay"].ConvertToDouble();
                    G_SalesReturnOnlineAmount += dr["OnlinePay"].ConvertToDouble();


                }


                //T_BillAmount += dr["TotalBillAmount"].ConvertToDouble();
                //T_DiscAmount += dr["DiscAmount"].ConvertToDouble();
                //T_NetAmount += dr["NetAmount"].ConvertToDouble();
                //T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                //T_BalAmount += dr["BalAmount"].ConvertToDouble();
                //T_CashAmount += dr["CashPay"].ConvertToDouble();
                //T_CardAmount += dr["CardPay"].ConvertToDouble();
                //T_ChequeAmount += dr["ChequePay"].ConvertToDouble();
                //T_NEFTAmount += dr["NEFTPay"].ConvertToDouble();
                //T_OnlineAmount += dr["OnlinePay"].ConvertToDouble();
                //previousLabel = dr["Label"].ConvertToString();
                if (i == 1)
                {
                    TotalCollection = G_SalesCashAmount.ConvertToDouble() + G_SalesCardAmount.ConvertToDouble() + G_SalesChequeAmount.ConvertToDouble() + G_SalesTotalNEFTAmount.ConvertToDouble() + G_SalesOnlineAmount.ConvertToDouble();
                    TotalCollectionSales = TotalCollection;

                }
                if (i == 2)
                {
                    TotalCollection = G_SalesReturnCashAmount.ConvertToDouble() + G_SalesReturnCardAmount.ConvertToDouble() + G_SalesReturnChequeAmount.ConvertToDouble() + G_SalesReturnNEFTAmount.ConvertToDouble() + G_SalesReturnOnlineAmount.ConvertToDouble();
                    TotalCollectionSalesReturn = TotalCollection;
                }

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Label"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalBillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NEFTPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["OnlinePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(TotalCollection.ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");

                FinalTotal = TotalCollectionSales.ConvertToDouble() - TotalCollectionSalesReturn.ConvertToDouble();

                T_TotalBillAmount = G_SalesBillAmount.ConvertToDouble() - G_SalesReturnBillAmount.ConvertToDouble();
                T_TotalDiscAmount = G_SalesDiscAmount.ConvertToDouble() - G_SalesReturnDiscAmount.ConvertToDouble();
                T_TotalNETAmount = G_SalesNetAmount.ConvertToDouble() - G_SalesReturnNetAmount.ConvertToDouble();
                T_TotalPaidAmount = G_SalesPaidAmount.ConvertToDouble() - G_SalesReturnPaidAmount.ConvertToDouble();
                T_TotalBalAmount = G_SalesBalAmount.ConvertToDouble() - G_SalesReturnBalAmount.ConvertToDouble();
                T_TotalCashAmount = G_SalesCashAmount.ConvertToDouble() - G_SalesReturnCashAmount.ConvertToDouble();
                T_TotalCardAmount = G_SalesCardAmount.ConvertToDouble() - G_SalesReturnCardAmount.ConvertToDouble();
                T_TotalChequeAmount = G_SalesChequeAmount.ConvertToDouble() - G_SalesReturnChequeAmount.ConvertToDouble();
                T_TotalNEFTAmount = G_SalesTotalNEFTAmount.ConvertToDouble() - G_SalesReturnNEFTAmount.ConvertToDouble();
                T_TotalOnlineAmount = G_SalesOnlineAmount.ConvertToDouble() - G_SalesReturnOnlineAmount.ConvertToDouble();
                F_TotalColl = T_TotalCashAmount.ConvertToDouble() + T_TotalCardAmount.ConvertToDouble() + T_TotalChequeAmount.ConvertToDouble() + T_TotalNEFTAmount.ConvertToDouble() + T_TotalOnlineAmount.ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;border:1px solid black;font-weight:bold'><td style=\"border: 1px solid #d4c3c3;text-align:center;padding: 6px;\">Sales - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(T_TotalBillAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalDiscAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalNETAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalBalAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalPaidAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalCashAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(T_TotalCardAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalNEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_TotalOnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(FinalTotal.To2DecimalPlace()).Append("</td></tr>");

                  
                }

            }


          

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCashAmount}}", T_TotalCashAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardAmount}}", T_TotalCardAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequeAmount}}", T_TotalChequeAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTAmount}}", T_TotalNEFTAmount.To2DecimalPlace());
            html = html.Replace("{{TotalOnlineAmount}}", T_TotalOnlineAmount.To2DecimalPlace());
            html = html.Replace("{{FinalTotal}}", FinalTotal.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());
            html = html.Replace("{{F_TotalColl}}", F_TotalColl.To2DecimalPlace());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            //html = html.Replace("{{City}}", HospAddress.GetColValue("City"));
            //html = html.Replace("{{Phone}}", HospAddress.GetColValue("Phone"));

            return html;


        }

        public string ViewPharSalesCashBookReport(DateTime FromDate, DateTime ToDate, string PaymentMode, int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@PaymentMode", PaymentMode) { DbType = DbType.String };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("m_rptSalesCashbook", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalPay = 0, T_TotalRoundOff = 0, T_BalancePay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;v\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RoundOff"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalPay += dr["TotalAmount"].ConvertToDouble();
                T_BalancePay = T_TotalNETAmount.ConvertToDouble() - T_TotalPay.ConvertToDouble();
                T_TotalRoundOff += dr["RoundOff"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalPay}}", T_TotalPay.To2DecimalPlace());
            html = html.Replace("{{T_TotalRoundOff}}", T_TotalRoundOff.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;

        }


        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            //double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            //double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash;
        }

        public string ViewPharmsDailyCollectionsummaryDayandUserwise(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            // SqlParameter[] para1 = new SqlParameter[0];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            // var HospAddress = GetDataTableProc("rtrv_UnitMaster_1",para1);
            var Bills = GetDataTableProc("m_rptSalesDailyColSummary_DayWise", para);
            string html = File.ReadAllText(htmlFilePath);
           
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double T_BillAmount = 0, T_DiscAmount = 0, T_NetAmount = 0, T_PaidAmount = 0, T_BalAmount = 0, T_CashAmount = 0, T_CardAmount = 0, T_SalesAmount = 0, T_ChequeAmount = 0, T_NEFTAmount = 0, T_OnlineAmount = 0;
            double G_SalesBillAmount = 0, G_SalesDiscAmount = 0, G_SalesNetAmount = 0, G_SalesPaidAmount = 0, G_SalesBalAmount = 0, G_SalesCashAmount = 0, G_SalesCardAmount = 0, G_SalesAmount = 0, G_SalesChequeAmount = 0, G_SalesTotalNEFTAmount = 0, G_SalesOnlineAmount = 0;
            double G_SalesReturnBillAmount = 0, G_SalesReturnDiscAmount = 0, G_SalesReturnNetAmount = 0, G_SalesReturnPaidAmount = 0, G_SalesReturnBalAmount = 0, G_SalesReturnCashAmount = 0, G_SalesReturnCardAmount = 0, G_SalesReturnChequeAmount = 0, G_SalesReturnNEFTAmount = 0, G_SalesReturnOnlineAmount = 0;
            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;
                if (dr["Label"].ConvertToString() == "Sales")
                {

                    G_SalesBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    G_SalesDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    G_SalesNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_SalesPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    G_SalesBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesCashAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesCardAmount += dr["CardPay"].ConvertToDouble();
                    G_SalesChequeAmount += dr["ChequePay"].ConvertToDouble();
                    G_SalesTotalNEFTAmount += dr["NEFTPay"].ConvertToDouble();
                    G_SalesOnlineAmount += dr["OnlinePay"].ConvertToDouble();

                }

                if (dr["Label"].ConvertToString() == "Sales Return")
                {

                    G_SalesReturnBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    G_SalesReturnDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    G_SalesReturnNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_SalesReturnPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    G_SalesReturnBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesReturnCashAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesReturnCardAmount += dr["CardPay"].ConvertToDouble();
                    G_SalesReturnChequeAmount += dr["ChequePay"].ConvertToDouble();
                    G_SalesReturnNEFTAmount += dr["NEFTPay"].ConvertToDouble();
                    G_SalesReturnOnlineAmount += dr["OnlinePay"].ConvertToDouble();

                }

                if (i == 1)
                {
                    items.Append("<tr style=\"font-size:20px;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append("Sales").Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;border:1px solid black;'><td colspan='3' style=\"border: 1px solid #d4c3c3;text-align:center;padding: 6px;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_NetAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_CardAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_ChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_NEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                   .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td></tr>");
                    T_BillAmount = 0; T_DiscAmount = 0; T_NetAmount = 0; T_BalAmount = 0; T_PaidAmount = 0; T_CashAmount = 0; T_CardAmount = 0; T_ChequeAmount = 0; T_NEFTAmount = 0; T_OnlineAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");

                }


                T_BillAmount += dr["TotalBillAmount"].ConvertToDouble();
                T_DiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalAmount += dr["BalAmount"].ConvertToDouble();
                T_CashAmount += dr["CashPay"].ConvertToDouble();
                T_CardAmount += dr["CardPay"].ConvertToDouble();
                T_ChequeAmount += dr["ChequePay"].ConvertToDouble();
                T_NEFTAmount += dr["NEFTPay"].ConvertToDouble();
                T_OnlineAmount += dr["OnlinePay"].ConvertToDouble();
                previousLabel = dr["Label"].ConvertToString();

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalBillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NEFTPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["OnlinePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;'><td colspan='3' style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(T_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_NetAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_CardAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_ChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_NEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></tr>");

                    //items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td colspan='2' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "TotalBillAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "DiscAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "NetAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "PaidAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "BalAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "CashPay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(GetSum(Bills, "CardPay").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "ChequePay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "NEFTPay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "OnlinePay").To2DecimalPlace()).Append("</td></tr>");
                }



            }


            T_TotalBillAmount = G_SalesBillAmount.ConvertToDouble() - G_SalesReturnBillAmount.ConvertToDouble();
            T_TotalDiscAmount = G_SalesDiscAmount.ConvertToDouble() - G_SalesReturnDiscAmount.ConvertToDouble();
            T_TotalNETAmount = G_SalesNetAmount.ConvertToDouble() - G_SalesReturnNetAmount.ConvertToDouble();
            T_TotalPaidAmount = G_SalesPaidAmount.ConvertToDouble() - G_SalesReturnPaidAmount.ConvertToDouble();
            T_TotalBalAmount = G_SalesBalAmount.ConvertToDouble() - G_SalesReturnBalAmount.ConvertToDouble();
            T_TotalCashAmount = G_SalesCashAmount.ConvertToDouble() - G_SalesReturnCashAmount.ConvertToDouble();
            T_TotalCardAmount = G_SalesCardAmount.ConvertToDouble() - G_SalesReturnCardAmount.ConvertToDouble();
            T_TotalChequeAmount = G_SalesChequeAmount.ConvertToDouble() - G_SalesReturnChequeAmount.ConvertToDouble();
            T_TotalNEFTAmount = G_SalesTotalNEFTAmount.ConvertToDouble() - G_SalesReturnNEFTAmount.ConvertToDouble();
            T_TotalOnlineAmount = G_SalesOnlineAmount.ConvertToDouble() - G_SalesReturnOnlineAmount.ConvertToDouble();
            TotalCollection = T_TotalCashAmount.ConvertToDouble() + T_TotalCardAmount.ConvertToDouble() + T_TotalChequeAmount.ConvertToDouble() + T_TotalNEFTAmount.ConvertToDouble() + T_TotalOnlineAmount.ConvertToDouble();


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCashAmount}}", T_TotalCashAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardAmount}}", T_TotalCardAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequeAmount}}", T_TotalChequeAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTAmount}}", T_TotalNEFTAmount.To2DecimalPlace());
            html = html.Replace("{{TotalOnlineAmount}}", T_TotalOnlineAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            //html = html.Replace("{{City}}", HospAddress.GetColValue("City"));
            //html = html.Replace("{{Phone}}", HospAddress.GetColValue("Phone"));

            return html;


        }
        public string ViewPurchaseorderReceipt(int PurchaseID, string htmlFilePath, string HeaderName)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@PurchaseID", PurchaseID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptPrintPurchaseOrder", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0, T_NetAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-size:15px;font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemTotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GrandTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                //items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td></tr>");


                T_TotalAmount += dr["ItemTotalAmount"].ConvertToDouble();
                T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                T_TotalDiscAmount += dr["DiscAmount"].ConvertToDouble();
                T_NetAmount += dr["GrandTotalAmount"].ConvertToDouble();
                ///*  T_T*/otalBalancepay += dr["GrandTotalAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_NetAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            html = html.Replace("{{PurchaseNo}}", Bills.GetColValue("PurchaseNo"));
            html = html.Replace("{{PurchaseDate}}", Bills.GetColValue("PurchaseDate"));
            html = html.Replace("{{SupplierName}}", Bills.GetColValue("SupplierName"));
            html = html.Replace("{{Fax}}", Bills.GetColValue("Fax"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{Email}}", Bills.GetColValue("Email"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PurchaseDate}}", Bills.GetColValue("PurchaseTime"));
            //html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            //html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));


            return html;

        }


        public string ViewPharOPExtcountdailyReport(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptPharmacyOP_External_CountDaily", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                if (i == 0)

                    items.Append("<tr style=\"font-size:20px;border: 1px;font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td colspan=\"6\" style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                i++;

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            return html;

        }

        public string ViewPharCompanycreditlistReport(int StoreId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptPharmacyCompanyCreditListForTally", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalPay = 0, T_TotalRoundOff = 0, T_BalancePay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["MDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DEBIT"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CREDIT"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PrintStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmt"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());

            return html;
        }

        public string ViewPharcomwisepatientcreditReceipt(int StoreID, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@StoreID", StoreID) { DbType = DbType.String };

            var Bills = GetDataTableProc("rptPharIpCompPatWiseCredit", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            string Storename = "";



            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                if (i == 0)
                {
                    items.Append("<tr style=\"font-size:20px;border: 1px;\"><td colspan=\"11\" style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PrintStoreName"].ConvertToString()).Append("</td></tr>");
                }
                i++;

                items.Append("<tr style=\"font - family: Calibri, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans - serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");



                //Storename = dr["PrintStoreName"].ConvertToString();
                T_TotalBillAmount += dr["TotalAmt"].ConvertToDouble();
                T_TotalDiscAmount += dr["DiscAmt"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalPaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_TotalBalAmount += dr["BalAmount"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalCashAmount}}", T_TotalCashAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalCardAmount}}", T_TotalCardAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalChequeAmount}}", T_TotalChequeAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalNEFTAmount}}", T_TotalNEFTAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalOnlineAmount}}", T_TotalOnlineAmount.To2DecimalPlace());
            return html;

        }

        public string ViewPharSalesstatement(int OP_IP_ID,int StoreId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@OP_IP_ID", OP_IP_ID) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("RptIPPatientSalesSummary", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            double salesTotal = 0, salesdisc = 0, salesnet = 0, salesbal = 0, salesrefund = 0, salesadv=0, salesrefundTotal = 0, salesrefunddisc = 0, salesrefundnet = 0, salesrefundbal = 0, salesrefundrefund = 0, Total_Total = 0, Total_disc = 0, Total_net = 0, Total_bal = 0, Total_refund = 0, salespaid = 0, salesrefundpaid = 0;
            double salescashTotal = 0, salescard = 0, salescheque = 0, salesneft = 0, salesonline = 0, salesreturncashTotal = 0, salesreturncard = 0, salesreturncheque = 0, salesreturnneft = 0, salesreturnonline = 0;
            double G_NetPayableAmt = 0, G_TotalAmount = 0, G_discAmount = 0 ,G_balAmount = 0 ,G_RefundAmount = 0 ,G_PaidAmount = 0, G_cashAmount = 0, G_chequeAmount = 0, G_neftAmount = 0, G_cardAmount = 0, G_onlineAmount = 0, G_AdvAmount=0;
            double G_BalTotalamt = 0, T_Totalamt = 0, T_Discamt = 0, T_Netamt = 0, T_paidamt = 0, T_Balamt = 0, T_Refundamt = 0, T_Creditreturnamt = 0, T_Cashreturnamt = 0, T_Advamt = 0, T_Advrefundamt = 0, T_Advusedamt = 0, T_AdvBalamt = 0, T_Cashpayamt = 0, T_Cardpayamt = 0, T_Chequepayamt = 0, T_Onlinepayamt = 0, T_Neftpayamt = 0, Net_Billamount =0, Total_PaidAmount = 0;
            double Total_Cash = 0, Total_card = 0, Total_cheque = 0, Total_neft = 0, Total_online = 0;



            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["Label"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='2' style=\"border-left:1px solid #ccc;border-right:1px solid #ccc;border-top:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                       .Append(G_TotalAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_discAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NetPayableAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_balAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_RefundAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_cashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_cardAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_chequeAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       //.Append(G_neftAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_onlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_AdvAmount.To2DecimalPlace()).Append("</td></tr>");

                    G_NetPayableAmt = 0; G_TotalAmount = 0; G_discAmount = 0; G_balAmount = 0; G_RefundAmount = 0; G_PaidAmount = 0; G_cashAmount = 0; G_chequeAmount = 0; G_neftAmount = 0; G_cardAmount = 0; G_onlineAmount = 0; G_AdvAmount=0;


                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color:black\"><td colspan=\"15\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");

                }

                G_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                G_discAmount += dr["DiscAmount"].ConvertToDouble();
                G_NetPayableAmt += dr["NetAmount"].ConvertToDouble();
                G_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                G_balAmount += dr["BalanceAmount"].ConvertToDouble();
                G_RefundAmount += dr["RefundAmt"].ConvertToDouble();
                G_AdvAmount += dr["AdvUsedAmt"].ConvertToDouble();

                G_cashAmount += dr["CashPay"].ConvertToDouble();
                G_cardAmount += dr["CardPay"].ConvertToDouble();
                G_chequeAmount += dr["ChequePay"].ConvertToDouble();
                G_onlineAmount += dr["PayTMAmount"].ConvertToDouble();
                G_neftAmount += dr["NEFTPayAmount"].ConvertToDouble();



                previousLabel = dr["Label"].ConvertToString();

                items.Append("<tr style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border-left: 1px solid #ccc;border-bottom:1px solid #ccc;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;text-align:right;vertical-align:middle\">").Append(dr["Time"].ConvertToDateString("dd/MM/yyyy hh:mm")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;padding-left:10px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;padding-left:10px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["RefundAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
              //  items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["AdvUsedAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                foreach (DataRow dr1 in Bills.Rows)
                {

                    i++; j++;
                    if (dr1["Label"].ConvertToString() == "Sales")
                    {

                        salesTotal += dr1["TotalAmount"].ConvertToDouble();
                        salesdisc += dr1["DiscAmount"].ConvertToDouble();
                        salesnet += dr1["NetAmount"].ConvertToDouble();
                        salesbal += dr1["BalanceAmount"].ConvertToDouble();
                        salesrefund += dr1["RefundAmt"].ConvertToDouble();
                        salespaid += dr1["PaidAmount"].ConvertToDouble();

                        salescashTotal += dr1["CashPay"].ConvertToDouble();
                        salescard += dr1["CardPay"].ConvertToDouble();
                        salescheque += dr1["ChequePay"].ConvertToDouble();
                        salesneft += dr1["NEFTPayAmount"].ConvertToDouble();
                        salesonline += dr1["PayTMAmount"].ConvertToDouble();
                        salesadv += dr1["AdvUsedAmt"].ConvertToDouble();

                    }

                    if (dr["Label"].ConvertToString() == "Sales Return")
                    {
                        salesrefundTotal += dr1["TotalAmount"].ConvertToDouble();
                        salesrefunddisc += dr1["DiscAmount"].ConvertToDouble();
                        salesrefundnet += dr1["NetAmount"].ConvertToDouble();
                        salesrefundbal += dr1["BalanceAmount"].ConvertToDouble();
                        salesrefundrefund += dr1["RefundAmt"].ConvertToDouble();
                        salesrefundpaid += dr1["PaidAmount"].ConvertToDouble();


                        salesreturncashTotal += dr1["CashPay"].ConvertToDouble();
                        salesreturncard += dr1["CardPay"].ConvertToDouble();
                        salesreturncheque += dr1["ChequePay"].ConvertToDouble();
                        salesreturnneft += dr1["NEFTPayAmount"].ConvertToDouble();
                        salesreturnonline += dr1["PayTMAmount"].ConvertToDouble();
                    }


                }


                Total_Total = salesTotal.ConvertToDouble() - salesrefundTotal.ConvertToDouble();
                Total_disc = salesdisc.ConvertToDouble() - salesrefunddisc.ConvertToDouble();
                Total_net = salesnet.ConvertToDouble() - salesrefundnet.ConvertToDouble();
                Total_bal = salesbal.ConvertToDouble() - salesrefundbal.ConvertToDouble();
                Total_refund = salesrefund.ConvertToDouble() - salesrefundrefund.ConvertToDouble();
                Total_PaidAmount = salespaid.ConvertToDouble() - salesrefundpaid.ConvertToDouble();


                Total_Cash = salescashTotal.ConvertToDouble() - salesreturncashTotal.ConvertToDouble();
                Total_card = salescard.ConvertToDouble() - salesreturncard.ConvertToDouble();
                Total_cheque = salescheque.ConvertToDouble() - salesreturncheque.ConvertToDouble();
                Total_neft = salesneft.ConvertToDouble() - salesreturnneft.ConvertToDouble();
                Total_online = salesonline.ConvertToDouble() - salesreturnonline.ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='2' style=\"border-right:1px solid #ccc;border-left:1px solid #ccc;border-top:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                                         .Append(G_TotalAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                         .Append(G_discAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                          .Append(G_NetPayableAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                         .Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                            .Append(G_balAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                           .Append(G_RefundAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                            .Append(G_cashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                        .Append(G_cardAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                        .Append(G_chequeAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                      //  .Append(G_neftAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                        .Append(G_onlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                        .Append("</td></tr>");


                    //items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='2' style=\"border-right:1px solid #ccc;border-top:1px solid black;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales - Sales Return</td><td style=\"border-right:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_Total.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_disc.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                    .Append(Total_net.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                    .Append(Total_bal.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_refund.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                    .Append(Total_Cash.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_card.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                    .Append(Total_cheque.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                //   .Append(Total_neft.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                   .Append(Total_online.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //                    .Append("</td></tr>");

                }

            }

          


            //

            G_BalTotalamt = salesnet.ConvertToDouble()-(T_Creditreturnamt.ConvertToDouble()+ salesadv.ConvertToDouble()+ salescashTotal.ConvertToDouble()+ salesonline.ConvertToDouble());



            //TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble() + T_TotalNEFT.ConvertToDouble() + T_TotalPAYTM.ConvertToDouble();


            html = html.Replace("{{Items}}", items.ToString());


            html = html.Replace("{{salesreturncashTotal}}", salesreturncashTotal.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salesonline}}", salesonline.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salescashTotal}}", salescashTotal.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salesnet}}", salesnet.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salesdisc}}", salesdisc.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salesrefund}}", salesrefund.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{salesTotal}}", salesTotal.ConvertToDouble().To2DecimalPlace());

            
            html = html.Replace("{{T_Totalamt}}", T_Totalamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Discamt}}", T_Discamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Netamt}}", T_Netamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_paidamt}}", T_paidamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Creditreturnamt}}", T_Creditreturnamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Cashreturnamt}}", T_Cashreturnamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Advamt}}", T_Advamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Advrefundamt}}", T_Advrefundamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Advusedamt}}", T_Advusedamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_AdvBalamt}}", T_AdvBalamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Cashpayamt}}", T_Cashpayamt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_Onlinepayamt}}", T_Onlinepayamt.ConvertToDouble().To2DecimalPlace());

            //html = html.Replace("{{Net_Billamount}}", Net_Billamount.To2DecimalPlace());
            //html = html.Replace("{{T_Balamt}}", T_Balamt.To2DecimalPlace());
            html = html.Replace("{{G_BalTotalamt}}", G_BalTotalamt.To2DecimalPlace());
            //html = html.Replace("{{T_Refundamt}}", T_Refundamt.To2DecimalPlace());

            //html = html.Replace("{{T_Cardpayamt}}", T_Cardpayamt.To2DecimalPlace());
            //html = html.Replace("{{T_Chequepayamt}}", T_Chequepayamt.To2DecimalPlace());
            //html = html.Replace("{{T_Neftpayamt}}", T_Neftpayamt.To2DecimalPlace());

            
            //html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount").ConvertToDouble().To2DecimalPlace());
            //html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount").ConvertToDouble().To2DecimalPlace());





            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{HospitalMobileNo}}", Bills.GetColValue("HospitalMobileNo"));
            html = html.Replace("{{HospitalEmailId}}", Bills.GetColValue("HospitalEmailId"));

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));

            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

       
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            
            html = html.Replace("{{Date}}", Bills.GetColValue("Time").ConvertToDateString("dd/M/yyyy hh:mm tt"));

            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceNo}}", Bills.GetColValue("AdvanceNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));

            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvUsedAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));


            html = html.Replace("{{AdvUsedAmt}}", Bills.GetColValue("AdvUsedAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{BalAdvAmount}}", Bills.GetColValue("BalAdvAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvAmount}}", Bills.GetColValue("AdvAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CashPay}}", Bills.GetColValue("CashPay").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMAmount}}", Bills.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvUsedAmt").ConvertToDouble().To2DecimalPlace());

            return html;
        }
    }
}

