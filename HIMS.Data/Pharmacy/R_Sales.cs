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
        public string ViewDailyCollection(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptSalesDailyCollection", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            StringBuilder items = new StringBuilder("");
            int i = 0;
            string previousLabel = "";
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0, T_NetAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    items.Append("<tr><td colspan='4' style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">Total</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
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
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                items.Append("<tr style=\"font-size:10px;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append(" ").Append(dr["Label"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr><td colspan='4' style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">Total</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
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
            html = html.Replace("{{TotalNETAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_BalanceAmount.To2DecimalPlace());
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

    }
}
