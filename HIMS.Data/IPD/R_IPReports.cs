﻿using HIMS.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Numerics;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPReports : GenericRepository, I_IPReports
    {
        public R_IPReports(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewIPSalesBillReport(int OP_IP_ID, int StoreId,string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@OP_IP_ID", OP_IP_ID) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPSalesBill", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_TotalAmount = 0, TotalAmt = 0, PatientName = 0, AdmittedDoctorName = 0, AdmissionDate = 0, BillNo = 0, CompBillDate = 0, IPDNo = 0, RoomName = 0;

            string previousLabel = "";


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["SalesType"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["SalesType"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Bill Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(TotalAmt.To2DecimalPlace()).Append("</td></tr>");

                    TotalAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["SalesType"].ConvertToString()).Append("</td></tr>");

                }
                TotalAmt += dr["TotalAmount"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                TotalAmt = TotalAmt;
                T_Count = T_Count + 1;
                previousLabel = dr["SalesType"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
         
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["UnitMRP"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Bill Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(TotalAmt.To2DecimalPlace()).Append("</td></tr>");


                }
            }

              
            //BillNo += dr["BillNo"].ConvertToDouble();
            //CompBillDate += dr["CompBillDate"].ConvertToDouble();
            //IPDNo += dr["IPDNo"].ConvertToDouble();
            //RoomName += dr["RoomName"].ConvertToDouble();

            foreach (DataRow dr1 in Bills.Rows)
            {



                i++; j++;

                if (dr1["SalesType"].ConvertToString() == "Sales")
                {

                    T_TotalAmount += dr1["TotalAmount"].ConvertToDouble();


                }
                if (dr1["SalesType"].ConvertToString() == "Sales Return")
                {

                    T_TotalAmount += dr1["TotalAmount"].ConvertToDouble();


                }


            
        }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{CompBillDate}}", Bills.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));



            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            //html = html.Replace("{{PatientName}}", PatientName.ToString());
            //html = html.Replace("{{AdmittedDoctorName}}", AdmittedDoctorName.ToString());
            //html = html.Replace("{{AdmissionDate}}", AdmissionDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{BillNo}}", BillNo.ToString());
            //html = html.Replace("{{CompBillDate}}", CompBillDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{IPDNo}}", IPDNo.ToString());
            //html = html.Replace("{{RoomName}}", RoomName.ToString());
            return html;

        }

        public string ViewIPSalesBillWithReturnReport(int OP_IP_ID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@OP_IP_ID", OP_IP_ID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptIPSalesBill_withReturn", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, G_Total = 0,T_TotalAmount = 0, G_TotalAmt = 0, TotalAmt = 0, T_NetAmount = 0, AdmittedDoctorName = 0, AdmissionDate = 0, BillNo = 0, CompBillDate = 0, IPDNo = 0, RoomName = 0;

            string previousLabel = "";


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["SalesType"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["SalesType"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Bill Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(TotalAmt.To2DecimalPlace()).Append("</td></tr>");

                    TotalAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["SalesType"].ConvertToString()).Append("</td></tr>");

                }
                TotalAmt += dr["TotalAmount"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                G_TotalAmt += dr["TotalAmount"].ConvertToDouble();
                G_Total += dr["TotalAmount"].ConvertToDouble();
                T_NetAmount = G_Total.ConvertToDouble() - G_TotalAmt.ConvertToDouble();
                TotalAmt = TotalAmt;
                T_Count = T_Count + 1;
                previousLabel = dr["SalesType"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["UnitMRP"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Bill Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(TotalAmt.To2DecimalPlace()).Append("</td></tr>");


                }
            }


            //BillNo += dr["BillNo"].ConvertToDouble();
            //CompBillDate += dr["CompBillDate"].ConvertToDouble();
            //IPDNo += dr["IPDNo"].ConvertToDouble();
            //RoomName += dr["RoomName"].ConvertToDouble();

            foreach (DataRow dr1 in Bills.Rows)
            {



                i++; j++;

                if (dr1["SalesType"].ConvertToString() == "Sales")
                {

                    G_TotalAmt += dr1["TotalAmount"].ConvertToDouble();


                }
                if (dr1["SalesType"].ConvertToString() == "Sales Return")
                {

                    G_Total += dr1["TotalAmount"].ConvertToDouble();


                }



            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{CompBillDate}}", Bills.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));


            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            T_NetAmount = G_Total.ConvertToDouble() - G_TotalAmt.ConvertToDouble();

            html = html.Replace("{{G_TotalAmt}}", G_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{G_Total}}", G_Total.To2DecimalPlace());
            //html = html.Replace("{{PatientName}}", PatientName.ToString());
            //html = html.Replace("{{AdmittedDoctorName}}", AdmittedDoctorName.ToString());
            //html = html.Replace("{{AdmissionDate}}", AdmissionDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{BillNo}}", BillNo.ToString());
            //html = html.Replace("{{CompBillDate}}", CompBillDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{IPDNo}}", IPDNo.ToString());
            //html = html.Replace("{{RoomName}}", RoomName.ToString());
            return html;

        }

        public string ViewIPFinalBill(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptIPComBlPrtSummary_WithAdd", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_TotalAmount = 0, T_TotalAmt = 0, PatientName = 0, AdmittedDoctorName = 0, AdmissionDate = 0, BillNo = 0, CompBillDate = 0, IPDNo = 0, RoomName = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_TotalAmt.ToString()).Append("</td></tr>");

                    T_TotalAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                }

                T_TotalAmt = T_TotalAmt;
                //T_Count = T_Count + 1;
                previousLabel = dr["GroupName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align left;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(T_TotalAmt.ToString()).Append("</td></tr>");


                }
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                //BillNo += dr["BillNo"].ConvertToDouble();
                //CompBillDate += dr["CompBillDate"].ConvertToDouble();
                //IPDNo += dr["IPDNo"].ConvertToDouble();
                //RoomName += dr["RoomName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{CompBillDate}}", Bills.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));



            html = html.Replace("{{T_TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            //html = html.Replace("{{PatientName}}", PatientName.ToString());
            //html = html.Replace("{{AdmittedDoctorName}}", AdmittedDoctorName.ToString());
            //html = html.Replace("{{AdmissionDate}}", AdmissionDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{BillNo}}", BillNo.ToString());
            //html = html.Replace("{{CompBillDate}}", CompBillDate.ToString("dd/MM/yyyy"));
            //html = html.Replace("{{IPDNo}}", IPDNo.ToString());
            //html = html.Replace("{{RoomName}}", RoomName.ToString());
            return html;

        }

        public string ViewIPDAdvanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptIPDAdvance", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAdvanceAmount = 0;





            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: center; padding: 6px;\">").Append(dr["AdvanceId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;  font-size: 20px;text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: center; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: right; padding: 6px;\">").Append(dr["AdvanceAmount"].ConvertToString()).Append("</td></tr>");


                T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();
               




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());






            return html;

        }

        public string ViewBillReport(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("rptIPDBillDateWise", para);




            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_BillAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_PaidAmount = 0, T_BalanceAmt = 0, T_CashPay = 0, T_ChequePay = 0, T_CardPay = 0, T_NeftPay = 0, T_PayTMPay = 0, T_AdvanceUsedAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NeftPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvUsdPay"].ConvertToDouble()).Append("</td></tr>");

                T_BillAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
                T_CashPay += dr["CashPay"].ConvertToDouble();
                T_ChequePay += dr["ChequePay"].ConvertToDouble();
                T_CardPay += dr["CardPay"].ConvertToDouble();
                T_NeftPay += dr["NeftPay"].ConvertToDouble();
                T_PayTMPay += dr["PayTMPay"].ConvertToDouble();
                T_AdvanceUsedAmount += dr["AdvUsdPay"].ConvertToDouble();
            }


            html = html.Replace("{{T_BillAmt}}", T_BillAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
            html = html.Replace("{{T_CashPay}}", T_CashPay.ToString());
            html = html.Replace("{{T_ChequePay}}", T_ChequePay.ToString());
            html = html.Replace("{{T_CardPay}}", T_CardPay.ToString());
            html = html.Replace("{{T_NeftPay}}", T_NeftPay.ToString());
            html = html.Replace("{{T_PayTMPay}}", T_PayTMPay.ToString());
            html = html.Replace("{{T_AdvanceUsedAmount}}", T_AdvanceUsedAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }



        public string ViewBillSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
     
            var Bills = GetDataTableProc("rptIPDBillDetails", para);




            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_PaidAmount = 0, T_BalanceAmt = 0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["PatientName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["PatientName"].ConvertToString())
                {
                    j = 1;

                    //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //   .Append(Dcount.ToString()).Append("</td></tr>");

                    //Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientName"].ConvertToString()).Append("</td></tr>");

                }
                previousLabel = dr["PatientName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
           
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillTotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();

            }


            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
           
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewIPCreditReport(DateTime FromDate, DateTime ToDate, int RegId,string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@RegId", RegId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("m_rptIPDCreditBills", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalPaidAmount = 0, T_TotalBalanceAmt = 0;





            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; text-align: center; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: center; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: center; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: left; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: right; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: right; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; text-align: right; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;text-align: right; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; text-align: right; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
       


                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_TotalBalanceAmt += dr["BalanceAmt"].ConvertToDouble();




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalanceAmt}}", T_TotalBalanceAmt.To2DecimalPlace());





            return html;

        }
        public string ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptBillGeneratePatientPaymentDue", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalPaidAmt = 0, T_TotalBalanceAmt = 0;





            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmt += dr["PaidAmt"].ConvertToDouble();
                T_TotalBalanceAmt += dr["BalanceAmt"].ConvertToDouble();




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalPaidAmt}}", T_TotalPaidAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalBalanceAmt}}", T_TotalBalanceAmt.To2DecimalPlace());





            return html;

        }
        public string ViewRefundofAdvanceReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptIPDRefundOfAdvance", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_RefundAmount = 0;





            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3;  font-size: 20px;text-align: center; padding: 6px;\">").Append(dr["RefundDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: center; padding: 6px;\">").Append(dr["RegId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
               

                items.Append("<td style=\"border: 1px solid #d4c3c3;  font-size: 20px;text-align: right; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_RefundAmount += dr["RefundAmount"].ConvertToDouble();




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_RefundAmount}}", T_RefundAmount.To2DecimalPlace());





            return html;

        }
        public string ViewRefundofBillReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptIPDRefundOfBill", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_RefundAmount = 0;





            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center;font-size: 20px; padding: 6px;\">").Append(dr["RefundDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center;font-size: 20px; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center;font-size: 20px; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; font-size: 20px;padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; font-size: 20px;padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_RefundAmount += dr["RefundAmount"].ConvertToDouble();




            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_RefundAmount}}", T_RefundAmount.To2DecimalPlace());





            return html;

        }


        public string ViewIPDischargeAndBillGenerationPendingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[0];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("IPPatientDischargeBillGenerationPending", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_NetAmount = 0,Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align left;\">").Append(dr["PatientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            return html;

        }

        public string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptIPDailyCollectionReport", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);

            //Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");

            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;  
            double G_NetPayableAmt = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_PayTMAmount = 0,G_AdvanceUsedAmount = 0;
            double T_NetPayableAmt = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0;

            double G_BillNetPayableAmt = 0, G_BillCashPayAmount = 0, G_BillChequePayAmount = 0, G_BillCardPayAmount = 0, G_BillPayTMAmount = 0;

            double G_AdvNetPayableAmt = 0, G_AdvCashPayAmount = 0,  G_AdvChequePayAmount = 0, G_AdvCardPayAmount = 0, G_AdvPayTMAmount = 0;

            double G_RefundNetPayableAmt = 0, G_RefundCashPayAmount = 0, G_RefundChequePayAmount = 0, G_RefundCardPayAmount = 0, G_RefundPayTMAmount = 0;

            double G_RefundAdvNetPayableAmt = 0,  G_RefundAdvCash = 0, G_RefundAdvCheque = 0, G_RefundAdvCard = 0, G_RefundAdvPayTMAmount = 0;



            double T_AddBillNetPayableAmt = 0, T_AddBillCashPayAmount = 0, T_AddBillChequePayAmount = 0, T_AddBillCardPayAmount = 0, T_AddBillPayTMAmount = 0;

            double T_AddBillrefundNetPayableAmt = 0, T_AddBillrefundCashPayAmount = 0, T_AddBillrefundChequePayAmount = 0, T_AddBillrefundCardPayAmount = 0, T_AddBillrefundPayTMAmount = 0;

            double T_FinalNetPayableAmt = 0, T_FinalCashPayAmount = 0, T_FinalChequePayAmount = 0,  T_FinalCardPayAmount = 0, T_FinalPayTMAmount = 0;
            double  T_IPAmount = 0, T_IPAdvanceAmount = 0, T_OPRefundbillAmount = 0, T_IPRefundbillAmount = 0, T_IPRefundAdvanceAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:25px;border: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"10\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                       .Append(G_NetPayableAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_NetPayableAmt = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0;

                    items.Append("<tr style=\"font-size:25px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:15px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }


                G_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();

                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();

                previousLabel = dr["Type"].ConvertToString();


                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:15px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");

                //items.Append("<tr style=\"font-size:15px;font-family: sans-serif;border-bottom: 1px solid black;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;width: 100px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                        .Append(G_NetPayableAmt.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                         .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td></tr>");
                }
            }


            foreach (DataRow dr in Bills.Rows)
            {



                i++; j++;
                if (dr["Type"].ConvertToString() == "IP Bill")
                {

                    G_BillNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                    G_BillCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_BillChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_BillCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_BillPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Advance")
                {
                    G_AdvNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                    G_AdvCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_AdvChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_AdvCardPayAmount += dr["CardPayAmount"].ConvertToDouble();

                    G_AdvPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Refund of Bill ")
                {
                    G_RefundNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                    G_RefundCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_RefundCardPayAmount += dr["CardPayAmount"].ConvertToDouble();

                    G_RefundPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Refund of Advance")
                {
                    G_RefundAdvNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                    G_RefundAdvCash += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundAdvCheque += dr["ChequePayAmount"].ConvertToDouble();
                    G_RefundAdvCard += dr["CardPayAmount"].ConvertToDouble();

                    G_RefundAdvPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }


            }
            T_AddBillNetPayableAmt = G_BillNetPayableAmt.ConvertToDouble() + G_AdvNetPayableAmt.ConvertToDouble();
            T_AddBillCashPayAmount = G_BillCashPayAmount.ConvertToDouble() + G_AdvCashPayAmount.ConvertToDouble();
            T_AddBillChequePayAmount = G_BillChequePayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble();
            T_AddBillCardPayAmount = G_BillCardPayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble();

            T_AddBillPayTMAmount = G_BillPayTMAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble();
            T_AddBillrefundNetPayableAmt = G_RefundNetPayableAmt.ConvertToDouble() + G_RefundAdvNetPayableAmt.ConvertToDouble();
            T_AddBillrefundCashPayAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundAdvCash.ConvertToDouble();
            T_AddBillrefundChequePayAmount = G_RefundChequePayAmount.ConvertToDouble() + G_RefundAdvCheque.ConvertToDouble();
            T_AddBillrefundCardPayAmount = G_RefundCardPayAmount.ConvertToDouble() + G_RefundAdvCard.ConvertToDouble();

            T_AddBillrefundPayTMAmount = G_RefundPayTMAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();

            T_FinalNetPayableAmt = T_AddBillNetPayableAmt.ConvertToDouble() - T_AddBillrefundNetPayableAmt.ConvertToDouble();
            T_FinalCashPayAmount = T_AddBillCashPayAmount.ConvertToDouble() - T_AddBillrefundCashPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalCardPayAmount = T_AddBillCardPayAmount.ConvertToDouble() - T_AddBillrefundCardPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalPayTMAmount = T_AddBillPayTMAmount.ConvertToDouble() - T_AddBillrefundPayTMAmount.ConvertToDouble();

            T_IPAmount = G_BillCashPayAmount.ConvertToDouble() + G_BillChequePayAmount.ConvertToDouble() + G_BillCardPayAmount.ConvertToDouble() +  G_BillPayTMAmount.ConvertToDouble();
            T_IPAdvanceAmount = G_AdvCashPayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble();
           
            T_IPRefundbillAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundChequePayAmount.ConvertToDouble() + G_RefundCardPayAmount.ConvertToDouble() + G_RefundPayTMAmount.ConvertToDouble();
            T_IPRefundAdvanceAmount = G_RefundAdvCash.ConvertToDouble() + G_RefundAdvCheque.ConvertToDouble() + G_RefundAdvCard.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();
            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() + T_FinalPayTMAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTMAmount}}", T_PayTMAmount.To2DecimalPlace());

            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{G_BillCashPayAmount}}", G_BillCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillCardPayAmount}}", G_BillCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillChequePayAmount}}", G_BillChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillPayTMAmount}}", G_BillPayTMAmount.To2DecimalPlace());



            html = html.Replace("{{G_AdvCashPayAmount}}", G_AdvCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvCardPayAmount}}", G_AdvCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvChequePayAmount}}", G_AdvChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvPayTMAmount}}", G_AdvPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{G_RefundCashPayAmount}}", G_RefundCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundCardPayAmount}}", G_RefundCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundChequePayAmount}}", G_RefundChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundPayTMAmount}}", G_RefundPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{G_RefundAdvCash}}", G_RefundAdvCash.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCard}}", G_RefundAdvCard.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCheque}}", G_RefundAdvCheque.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvPayTMAmount}}", G_RefundAdvPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{T_IPAmount}}", T_IPAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPAdvanceAmount}}", T_IPAdvanceAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPRefundbillAmount}}", T_IPRefundbillAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPRefundAdvanceAmount}}", T_IPRefundAdvanceAmount.To2DecimalPlace());






            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));



            return html;
        }

        public string ViewOPIPBillSummaryReceipt(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[2];
            // SqlParameter[] para1 = new SqlParameter[0];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptOP_IP_BillSummaryReport", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double T_NetPayAmount = 0, T_DiscAmount = 0, T_TotalAmount = 0, T_PaidAmount = 0, T_BalAmount = 0, T_CashAmount = 0, T_CardAmount = 0, T_SalesAmount = 0, T_ChequeAmount = 0, T_NEFTAmount = 0, T_OnlineAmount = 0, T_AdvpayAmount = 0;
            double FinalTotal = 0;
            double T_FinalTotalAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalAdvAmount = 0, T_TotalOnlineAmount = 0;
            double AdvUsdPay = 0, PayTMPay = 0, CashPay = 0, BalanceAmt = 0, PaidAmount = 0, TotalAmt = 0, ConcessionAmt = 0, NetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["lbl"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;color:black\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["lbl"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_TotalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_NetPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_AdvpayAmount.To2DecimalPlace()).Append("</td></tr>");
                    T_TotalAmount = 0; T_DiscAmount = 0; T_NetPayAmount = 0; T_BalAmount = 0; T_PaidAmount = 0; T_CashAmount = 0; T_CardAmount = 0; T_ChequeAmount = 0; T_NEFTAmount = 0; T_OnlineAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["lbl"].ConvertToString()).Append("</td></tr>");

                }


                T_NetPayAmount += dr["NetPayableAmt"].ConvertToDouble();
                T_DiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                T_CashAmount += dr["CashPay"].ConvertToDouble();
                T_OnlineAmount += dr["PayTMPay"].ConvertToDouble();
                T_AdvpayAmount += dr["AdvUsdPay"].ConvertToDouble();

                T_FinalTotalAmount += dr["TotalAmt"].ConvertToDouble();
                T_TotalNETAmount += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalDiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalPaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_TotalBalAmount += dr["BalanceAmt"].ConvertToDouble();
                T_TotalCashAmount += dr["CashPay"].ConvertToDouble();
                T_TotalOnlineAmount += dr["PayTMPay"].ConvertToDouble();
                T_TotalAdvAmount += dr["AdvUsdPay"].ConvertToDouble();

                previousLabel = dr["lbl"].ConvertToString();

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
       
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PayTMPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["AdvUsdPay"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_NetPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;border-left:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                          .Append(T_AdvpayAmount.To2DecimalPlace()).Append("</td></tr>");

                    items.Append("<tr style='border:1px solid black;color:black;font-weight:bold;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_FinalTotalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalDiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalNETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalPaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalBalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalCashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_TotalOnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalAdvAmount.To2DecimalPlace()).Append("</td></tr>");
                }

                TotalAmt += dr["TotalAmt"].ConvertToDouble();
                ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                PaidAmount += dr["PaidAmount"].ConvertToDouble();
                BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
                CashPay += dr["CashPay"].ConvertToDouble();
                PayTMPay += dr["PayTMPay"].ConvertToDouble();
                AdvUsdPay += dr["AdvUsdPay"].ConvertToDouble();
            }
            html = html.Replace("{{TotalAmt}}", TotalAmt.To2DecimalPlace());
            html = html.Replace("{{ConcessionAmt}}", ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{NetPayableAmt}}", NetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", PaidAmount.To2DecimalPlace());
            html = html.Replace("{{BalanceAmt}}", BalanceAmt.To2DecimalPlace());
            html = html.Replace("{{CashPay}}", CashPay.To2DecimalPlace());
            html = html.Replace("{{PayTMPay}}", PayTMPay.To2DecimalPlace());
            html = html.Replace("{{AdvUsdPay}}", AdvUsdPay.To2DecimalPlace());


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;
        }
        public string ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, BigInteger DoctorId, BigInteger WardId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int16 };
            para[3] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("rptListofAdmission", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Count"].ConvertToString()).Append("</td>");

                T_Count += dr["Count"].ConvertToDouble();
            }
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;

        }
        public string ViewIPDDischargeReportWithMarkStatus(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptIPDischargeMarkStatusReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px;  padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px;  padding: 6px;text-align:center;\">").Append(dr["IsMarkForDisNurDateTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px;  padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px;  padding: 6px;text-align:center;\">").Append(dr["DischargeTime"].ConvertToDateString()).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px;  padding: 6px;text-align:center;\">").Append(dr["DiffTimeInHr"].ConvertToString()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewIPDDischargeReportWithBillSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptDischargewithBillSummaryReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DischargeTypeName"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append(" </td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
           
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Diagnosis"].ConvertToString()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            return html;

        }

        public string ViewDepartmentWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[0];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("Rtrv_IPDepartWsSumry", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px;padding: 6px;text-align:center;\">").Append(dr["Lbl"].ConvertToString()).Append("</td>");

                T_Count += dr["Lbl"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewDoctorWiseCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[0];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("Rtrv_IPDocWsSumry", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px;padding: 6px;text-align:left;\">").Append(dr["DocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:center;\">").Append(dr["Lbl"].ConvertToString()).Append("</td>");
                T_Count += dr["Lbl"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewOPToIPConvertedListWithServiceAvailed(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptOPtoIPCotAdmList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Medical = 0, T_Pathology = 0, T_Radiology = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px;padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 20px;padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:right;\">").Append(dr["Medical"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:right;\">").Append(dr["Pathology"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text-align:right;\">").Append(dr["Radiology"].ConvertToDouble()).Append("</td></tr>");
                T_Medical += dr["Medical"].ConvertToDouble();
                T_Pathology += dr["Pathology"].ConvertToDouble();
                T_Radiology += dr["Radiology"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Medical}}", T_Medical.To2DecimalPlace());
            html = html.Replace("{{T_Pathology}}", T_Pathology.To2DecimalPlace());
            html = html.Replace("{{T_Radiology}}", T_Radiology.To2DecimalPlace());
            return html;

        }

       

        public string ViewCurrentrefadmittedlist(int DoctorId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("RptCurrentRefAdmittedReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text_align:center;\">").Append(dr["AdmissionID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text_align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text_align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text_align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 20px; padding: 6px;text_align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

            }



            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());

            return html;
        }

        public string ViewIPDischargeTypeReport(int DoctorId, DateTime FromDate, DateTime ToDate, int DischargeTypeId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[3] = new SqlParameter("@DischargeTypeId", DischargeTypeId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptDischargeTypeReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_NetAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Diagnosis"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Procedurename1"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Procedurename2"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Procedurename3"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");

                T_Count += dr["PatientName"].ConvertToDouble();
                T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();

            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }

        public string ViewIPDAdmissionList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptListofAdmission", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmittedDocName"].ConvertToDateString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmittedDocName"].ConvertToDateString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-size:21px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmittedDocName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmittedDocName"].ConvertToDateString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:center;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:center;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:left;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size:19px;padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:left;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:left;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size:19px; padding: 6px;text-align:left;\">").Append(dr["RoomName"].ConvertToString()).Append("</td></tr>");
                //if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                //{

                //    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                //         .Append(Dcount.ToString()).Append("</td></tr>");


                //}
                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }

        public string ViewIPDAdmissionListCompanyWiseDetails(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
          
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptListofAdmission", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BedName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["PatientName"].ConvertToDouble();

            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }



        public string ViewIPDCurrentAdmittedList(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];

            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_ChargesAmount = 0, T_AdvanceAmount = 0, T_BalPayAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
       
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["ChargesAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdvanceAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BalPayAmt"].ConvertToDouble()).Append("</td></tr>");


                T_ChargesAmount += dr["ChargesAmount"].ConvertToDouble();
                T_AdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();
                T_BalPayAmt += dr["BalPayAmt"].ConvertToDouble();
                //if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                //{

                //    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                //         .Append(Dcount.ToString()).Append("</td></tr>");


                //}
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{T_ChargesAmount}}", T_ChargesAmount.ToString());
            html = html.Replace("{{T_AdvanceAmount}}", T_AdvanceAmount.ToString());
            html = html.Replace("{{T_BalPayAmt}}", T_BalPayAmt.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }

        public string ViewIPDCurrentAdmittedWardWiseCharges(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];

            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0, T_ChargesAmount = 0, T_AdvanceAmount = 0, T_BalPayAmt = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["RoomName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"17\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["RoomName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"17\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RoomName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["RoomName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ChargesAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["AdvanceAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["BalPayAmt"].ConvertToDouble()).Append("</td></tr>");
                T_ChargesAmount += dr["ChargesAmount"].ConvertToDouble();
                T_AdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();
                T_BalPayAmt += dr["BalPayAmt"].ConvertToDouble();

            }


            html = html.Replace("{{T_ChargesAmount}}", T_ChargesAmount.ToString());
            html = html.Replace("{{T_AdvanceAmount}}", T_AdvanceAmount.ToString());
            html = html.Replace("{{T_BalPayAmt}}", T_BalPayAmt.ToString());
            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }



        public string ViewIPDCurrentAdmittedDoctorWiseCharges(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, int CompanyId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];

            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmittedDoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"17\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmittedDoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"17\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmittedDoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Age"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["GenderName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionTime"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ChargesAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["AdvanceAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["BalPayAmt"].ConvertToDouble()).Append("</td></tr>");
                T_Count += dr["BalPayAmt"].ConvertToDouble();

            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }



        public string ViewDateWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDatewiseAdmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }

        public string ViewMonthWiseAdmissionCount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptMonthWiseadmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmMonthYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmCount"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["AdmCount"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewDateWiseDoctorWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDatewiseAdmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }

                T_Count += dr["PatientName"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewDateWiseDoctorWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDatewiseAdmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }

                T_Count += dr["DoctorName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewDateWiseDepartmentWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDatewiseAdmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_Count += dr["PatientName"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }

        public string ViewDateWiseDepartmentWiseAdmissionCountSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDatewiseAdmissionCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy"))
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy");

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
               
            }
            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
          
            return html;

        }
        public string ViewDrWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDrwisecollectionDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, NetPayableAmt=0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                NetPayableAmt = NetPayableAmt;
                //T_NetPayableAmt = T_NetPayableAmt + 1;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BillDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetPayableAmt.ToString()).Append("</td></tr>");


                }
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
              T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            //html = html.Replace("{{T_TotalAmt"}}", T_TotalTotalAmt".To2DecimalPlace());
            //html = html.Replace("{{T_ConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            //html = html.Replace("{{T_NetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            return html;

        }
        public string ViewDrWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDrwisecollectionDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            return html;

        }

        public string ViewDepartmentWiseCollectionDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDrwisecollectionDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, NetPayableAmt = 0;
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                NetPayableAmt = NetPayableAmt + 1;
                T_NetPayableAmt = T_NetPayableAmt + 1;
                previousLabel = dr["DepartmentName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["BillDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");



                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetPayableAmt.ToString()).Append("</td></tr>");


                }
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            return html;

        }

        public string ViewDepartmentWiseCollectionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptDrwisecollectionDetail", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:right;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            return html;

        }
        public string ViewCompanyWiseAdmissionCountDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptCompanywiseadmissioncount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; font-size: 19px;padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:left;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:left;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3;font-size: 19px; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewCompanyWiseAdmissionSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rptCompanywiseadmissioncount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["CompanyName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }

        public string ViewCompanyWiseBillDetailReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_CNetPayableAmt = 0, T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0 , NetPayableAmt = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["CompanyName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["CompanyName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                }

                NetPayableAmt = NetPayableAmt + 1;
                T_CNetPayableAmt = T_CNetPayableAmt + 1;
                previousLabel = dr["CompanyName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TDSAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetPayableAmt.ToString()).Append("</td></tr>");


                }
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            return html;

        }

        public string ViewCompanyWiseBillSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0,j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");
                //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                //T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            //html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.To2DecimalPlace());
            return html;

        }


        public string ViewCompanyWiseCreditReportDetail(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rtrv_IPCompanyWiseCreditReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_BalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

               
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");
                
                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.To2DecimalPlace());
            return html;

        }

        public string ViewCompanyWiseCreditReportSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("rtrv_IPCompanyWiseCreditReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_BalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.To2DecimalPlace());
            return html;

        }
        public string ViewIPAdmitPatientwardwisechargesReport(int DoctorId, int WardId, int CompanyId, string htmlFilePath, string HeaderName)
        {

            SqlParameter[] para = new SqlParameter[3];
            // SqlParameter[] para1 = new SqlParameter[0];
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReportwithCharges", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            //html = html.Replace("{{NewHeader}}" );

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double T_ChargesAmount = 0, T_BalAmount = 0, T_AdvAmount = 0, G_ChargesAmount = 0, G_BalAmount = 0, G_AdvAmount = 0;
            double FinalTotal = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["WardId"].ConvertToString();
                    items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                    items.Append("<tr style=\"font-size:20px;border: 1px;\"><td colspan=\"11\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["WardId"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_ChargesAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_AdvAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                   .Append(T_BalAmount.To2DecimalPlace()).Append("</td></tr>");
                    T_ChargesAmount = 0; T_BalAmount = 0; T_AdvAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"11\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["WardId"].ConvertToString()).Append("</td></tr>");

                }


                T_ChargesAmount += dr["ChargesAmount"].ConvertToDouble();
                T_AdvAmount += dr["AdvanceAmount"].ConvertToDouble();
                T_BalAmount += dr["BalPayAmt"].ConvertToDouble();


                previousLabel = dr["WardId"].ConvertToString();

                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdmissionID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ChargesAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalPayAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_ChargesAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(T_AdvAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;border-left:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                          .Append(T_BalAmount.To2DecimalPlace()).Append("</td></tr>");

                }



            }



            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{T_ChargesAmount}}", T_ChargesAmount.To2DecimalPlace());
            html = html.Replace("{{T_AdvAmount}}", T_AdvAmount.To2DecimalPlace());
            html = html.Replace("{{T_BalAmount}}", T_BalAmount.To2DecimalPlace());


            return html;
        }
        public string ViewIPDDischargeTypeCompanyWise(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DischargeTypeId", DischargeTypeId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptDischargeTypeReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DischargeTypeName"].ConvertToDateString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DischargeTypeName"].ConvertToDateString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["DischargeTypeName"].ConvertToDateString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewIPDDischargeTypeCompanyWiseCount(DateTime FromDate, DateTime ToDate, int DoctorId, int DischargeTypeId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DischargeTypeId", DischargeTypeId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptDischargeTypeReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["CompanyName"].ConvertToDateString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"3\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["CompanyName"].ConvertToDateString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='2' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"3\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["CompanyName"].ConvertToDateString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;font-size:20px;\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td></tr>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='2' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }
        public string ViewIPDRefDoctorWise(DateTime FromDate, DateTime ToDate,  string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
           
            var Bills = GetDataTableProc("RptRefDoctorWiseAdmission", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, Dcount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["RefDoctorName"].ConvertToDateString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToDateString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(Dcount.ToString()).Append("</td></tr>");

                    Dcount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                }

                Dcount = Dcount + 1;
                T_Count = T_Count + 1;
                previousLabel = dr["RefDoctorName"].ConvertToDateString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["City"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["Phone"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:center;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;text-align:left;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td></tr>");
           
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(Dcount.ToString()).Append("</td></tr>");


                }
                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }

        public string ViewIPDDischargeDetails(DateTime FromDate, DateTime ToDate, int DischargeTypeId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DischargeTypeId", DischargeTypeId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptDischargeTypeDetailsReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_BalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"text-align: center;font-size:19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; font-size:19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size:19px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center;font-size:19px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size:19px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size:19px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left;font-size:19px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Diagnosis"].ConvertToString()).Append("</td></tr>");

               
          
            

           }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
           
            return html;

        }


        public string ViewIPDAdmissionListCompanyWiseSummary(DateTime FromDate, DateTime ToDate, int DoctorId, int WardId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCompanyWiseAdmissionCountReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");



                items.Append("<td style=\"text-align: left;font-size:20px;  border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");
                //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyId"].ConvertToString()).Append("</td></tr>");


                //T_Count += dr["CompanyId"].ConvertToDouble();


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_Count}}", T_Count.To2DecimalPlace());
            return html;

        }


    }
}
