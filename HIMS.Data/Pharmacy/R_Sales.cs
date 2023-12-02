using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HIMS.Data.Pharmacy
{
    public class R_Sales : GenericRepository, I_Sales
    {
        public R_Sales(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public String InsertSales(SalesParams salesParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesParams.SalesInsert.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesParams.SalesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesParams.UpdateCurStkSales)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesParams.Cal_DiscAmount_Sales.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesParams.Cal_GSTAmount_Sales.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment["BillNo"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public String InsertSalesWithCredit(SalesCreditParams salesCreditParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@SalesId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc3 = salesCreditParams.SalesInsertCredit.ToDictionary();
            disc3.Remove("SalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Sales_1", disc3, outputId1);

            foreach (var a in salesCreditParams.SalesDetailInsertCredit)
            {
                var disc5 = a.ToDictionary();
                disc5["SalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_SalesDetails_1", disc5);
            }

            foreach (var a in salesCreditParams.UpdateCurStkSalesCredit)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_T_CurStk_Sales_Id_1", disc1);
            }

            var vDiscCal = salesCreditParams.Cal_DiscAmount_SalesCredit.ToDictionary();
            vDiscCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_Sales", vDiscCal);

            var vGSTCal = salesCreditParams.Cal_GSTAmount_SalesCredit.ToDictionary();
            vGSTCal["SalesID"] = BillNo;
            ExecNonQueryProcWithOutSaveChanges("m_Cal_GSTAmount_Sales", vDiscCal);

            _unitofWork.SaveChanges();
            return BillNo;
        }

        public bool PaymentSettlement(SalesParams salesParams)
        {
            var vPayment = salesParams.SalesPayment.ToDictionary();
            vPayment.Remove("PaymentID");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_Pharmacy_New_1", vPayment);

            var vUpdateHeader = salesParams.update_Pharmacy_BillBalAmount.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_Pharmacy_BillBalAmount_1", vUpdateHeader);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewBill(int SalesID, int OP_IP_Type, string htmlFilePath)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "SalesID", SalesID },
                { "OP_IP_Type", OP_IP_Type }
            };
            var Bills = GetDataTableProc("rptSalesPrint", dictionary);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{StoreName}}", Bills.GetColValue("StoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            html = html.Replace("{{GSTIN}}", Bills.GetColValue("GSTIN"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{SalesNo}}", Bills.GetColValue("SalesNo"));
            html = html.Replace("{{Date}}", Bills.GetDateColValue("Date"));
            StringBuilder items = new StringBuilder("");
            int i = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">-</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["BatchExpDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["UnitMRP"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["TotalAmount"].ConvertToString()).Append("</td></tr>");
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{HTotalAmount}}", Bills.GetColValue("HTotalAmount"));
            html = html.Replace("{{CGSTPer}}", Bills.GetColValue("CGSTPer"));
            html = html.Replace("{{CGSTAmt}}", Bills.GetColValue("CGSTAmt"));
            html = html.Replace("{{SGSTPer}}", Bills.GetColValue("SGSTPer"));
            html = html.Replace("{{SGSTAmt}}", Bills.GetColValue("SGSTAmt"));
            html = html.Replace("{{IGSTPer}}", Bills.GetColValue("IGSTPer"));
            html = html.Replace("{{IGSTAmt}}", Bills.GetColValue("IGSTAmt"));
            html = html.Replace("{{DiscAmount}}", Bills.GetColValue("DiscAmount"));
            html = html.Replace("{{TotalGst}}", Bills.GetColValue("TotalGst"));
            html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;

        }
        public string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesDailyCollection", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{HeaderName}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            string previousLabel = "";
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    items.Append("<tr style='border:1px solid black;color:red;'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;
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
                items.Append("<tr style=\"font-size:10px;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append(" ").Append(dr["Label"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:right\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;color:red;'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");

                    items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales + Cash GRN - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "CashPayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "CardPayAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "ChequePayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "NEFTPayAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "PayTMAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "AdvanceUsedAmount").To2DecimalPlace()).Append("</td><td style=\"padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "BalanceAmount").To2DecimalPlace()).Append("</td></tr>");
                }
            }
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
            return html;
        }
        public double GetSum(DataTable dt, string ColName)
        {
            double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash - Return;
        }


        public string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptSalesDailyColSummary", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            //  string previousLabel = "";

            double G_SalesBillAmount = 0, G_SalesDiscAmount = 0, G_SalesNetAmount = 0, G_SalesPaidAmount = 0, G_SalesBalAmount = 0, G_SalesCashAmount = 0, G_SalesTotalCardAmount = 0, G_SalesAmount = 0, G_SalesChequeAmount = 0, G_SalesTotalNEFTAmount = 0, G_SalesOnlineAmount = 0;
            double G_SalesReturnBillAmount = 0, G_SalesReturnDiscAmount = 0, G_SalesReturnNetAmount = 0, G_SalesReturnPaidAmount = 0, G_SalesReturnBalAmount = 0, G_SalesReturnCashAmount = 0, G_SalesReturnCardAmount = 0, G_SalesReturnChequeAmount = 0, G_SalesReturnNEFTAmount = 0, G_SalesReturnOnlineAmount = 0; 
            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["TotalBillAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["DiscAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CashPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CardPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ChequePay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NEFTPay"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["OnlinePay"].ConvertToDouble()).Append("</td></tr>");


                if (dr["Label"].ConvertToString() == "Sales")
                {

                    G_SalesBillAmount += dr["TotalBillAmount"].ConvertToDouble();
                    G_SalesDiscAmount += dr["DiscAmount"].ConvertToDouble();
                    G_SalesNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_SalesPaidAmount += dr["PaidAmount"].ConvertToDouble();
                    G_SalesBalAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesCashAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesTotalCardAmount += dr["CardPay"].ConvertToDouble();
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
                    G_SalesReturnChequeAmount += dr["BalAmount"].ConvertToDouble();
                    G_SalesReturnNEFTAmount += dr["CashPay"].ConvertToDouble();
                    G_SalesReturnOnlineAmount += dr["CardPay"].ConvertToDouble();

                }


            }


            T_TotalBillAmount += G_SalesBillAmount.ConvertToDouble() - G_SalesReturnBillAmount.ConvertToDouble();
            T_TotalDiscAmount += G_SalesDiscAmount.ConvertToDouble() - G_SalesReturnDiscAmount.ConvertToDouble();
            T_TotalNETAmount += G_SalesNetAmount.ConvertToDouble() - G_SalesReturnNetAmount.ConvertToDouble();
            T_TotalPaidAmount += G_SalesPaidAmount.ConvertToDouble() - G_SalesReturnPaidAmount.ConvertToDouble();
            T_TotalBalAmount += G_SalesBalAmount.ConvertToDouble() - G_SalesReturnBalAmount.ConvertToDouble();
            T_TotalCashAmount += G_SalesCashAmount.ConvertToDouble() - G_SalesReturnCashAmount.ConvertToDouble();
            T_TotalCardAmount += G_SalesReturnCardAmount.ConvertToDouble() - G_SalesReturnCardAmount.ConvertToDouble();
            T_TotalChequeAmount += G_SalesChequeAmount.ConvertToDouble() - G_SalesReturnChequeAmount.ConvertToDouble();
            T_TotalNEFTAmount += G_SalesTotalNEFTAmount.ConvertToDouble() - G_SalesReturnNEFTAmount.ConvertToDouble();
            T_TotalOnlineAmount += G_SalesOnlineAmount.ConvertToDouble() - G_SalesReturnOnlineAmount.ConvertToDouble();


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
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

            double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["VatAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td></tr>");


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
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());

            return html;


        }


        public string GetFilePath()
        {
            // for live
            //var dt = GetDataTableQuery("SELECT TOP 1 FilePathLocation FROM ConfigSetting order by ConfigId DESC", null);
            // for local
            var dt = new DataTable();
            dt.Columns.Add("s");
            dt.Rows.Add("");
            return Convert.ToString(dt.Rows[0][0]);
        }

        public string ViewSalesSummaryReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
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
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");

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
            return html;

        }

        public string ViewSalesReportPatientWise(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int AddedBy, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
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
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            return html;

        }

        public string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId,string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesReturnReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesReturnNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',1545,1557,0,10016
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

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
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();
                // exec RptSalesReturnReport '11-01-2023','11-26-2023',1545,1557,10016

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCGST}}", T_TotalCGST.To2DecimalPlace());
            html = html.Replace("{{TotalSGST}}", T_TotalSGST.To2DecimalPlace());
            html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());
            return html;
        }

        public string ViewSalesCreditReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@FromDate",FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@SalesFromNumber",SalesFromNumber) { DbType = DbType.String };
            para[3] = new SqlParameter("@SalesToNumber",SalesToNumber) { DbType = DbType.String };
            para[4] = new SqlParameter("@CreditReasonId",CreditReasonId) { DbType = DbType.Int64 };
            para[5] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("RptPharmacyCreditReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

            }

            html = html.Replace("{{Items}}",items.ToString());
            html = html.Replace("{{FromDate}}",FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}",T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}",T_TotalBalancepay.To2DecimalPlace());

            return html;

        }
    }
}
