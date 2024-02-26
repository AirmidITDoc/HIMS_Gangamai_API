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

            var vDaraftStatus = salesParams.SalesDraftStatusUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_T_SalDraHeader_IsClosed_1", vDaraftStatus);

            _unitofWork.SaveChanges();
            return BillNo;
        }


        public String InsertSalesDraftBill(SalesParams salesParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DSalesId",
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


            var disc3 = salesParams.SalesDraftbillInsert.ToDictionary();
            disc3.Remove("DSalesId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_T_SalesDraftHeader_1", disc3, outputId1);

            foreach (var a in salesParams.SalesDraftbillDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["DSalesID"] = BillNo;
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_T_SalesDraftDet_1", disc5);
            }

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
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Label"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                     .Append("Total Cash").Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total Card").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total Cheque").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total NEFTPay").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total OnlinePay").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total Adv.Used").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append("Total BalAmt").Append("</td></tr>");


                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");
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


                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(j).Append("</td>");
               // items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["UserName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:right\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                  .Append("Total Cash").Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total Card").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total Cheque").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total NEFTPay").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total OnlinePay").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total Adv.Used").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                  .Append("Total BalAmt").Append("</td></tr>");



                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");

                    items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales + Cash GRN - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "CashPayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "CardPayAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "ChequePayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "NEFTPayAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "PayTMAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "AdvanceUsedAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "BalanceAmount").To2DecimalPlace()).Append("</td></tr>");
                }
            }

            TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble() + T_NEFTPayAmount.ConvertToDouble() + T_PayTMAmount.ConvertToDouble();

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
        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            //double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            //double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash;
        }


        public string ViewDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];
            // SqlParameter[] para1 = new SqlParameter[0];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            // var HospAddress = GetDataTableProc("rtrv_UnitMaster_1",para1);
            var Bills = GetDataTableProc("m_rptSalesDailyColSummary", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            //string previousLabel = "";
            double TotalCollection = 0, TotalCollectionSales = 0, TotalCollectionSalesReturn=0,FinalTotal=0, F_TotalColl=0;
                       
            //double T_BillAmount = 0, T_DiscAmount = 0, T_NetAmount = 0, T_PaidAmount = 0, T_BalAmount = 0, T_CashAmount = 0, T_CardAmount = 0, T_SalesAmount = 0, T_ChequeAmount = 0, T_NEFTAmount = 0, T_OnlineAmount = 0;
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

                //TotalCollection = G_SalesCashAmount.ConvertToDouble() + G_SalesCardAmount.ConvertToDouble() + G_SalesChequeAmount.ConvertToDouble() + G_SalesTotalNEFTAmount.ConvertToDouble() + G_SalesOnlineAmount.ConvertToDouble();

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
                if(i==2)
                {
                    TotalCollection = G_SalesReturnCashAmount.ConvertToDouble() + G_SalesReturnCardAmount.ConvertToDouble() + G_SalesReturnChequeAmount.ConvertToDouble() + G_SalesReturnNEFTAmount.ConvertToDouble() + G_SalesReturnOnlineAmount.ConvertToDouble();
                    TotalCollectionSalesReturn = TotalCollection;
                }

                    items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;color:blue;border-bottom: 1px solid black;\">").Append(dr["Label"].ConvertToString()).Append("</td>");
              
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;border-bottom: 1px solid black;\">").Append(dr["TotalBillAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;border-bottom: 1px solid black;\">").Append(dr["DiscAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;border-bottom: 1px solid black;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["NEFTPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["OnlinePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(TotalCollection.ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;border-bottom: 1px solid black;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");

                FinalTotal = TotalCollectionSales.ConvertToDouble() - TotalCollectionSalesReturn.ConvertToDouble();
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "TotalBillAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "DiscAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "NetAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "BalAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "PaidAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "CashPay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(GetSum(Bills, "CardPay").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "ChequePay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "NEFTPay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(GetSum(Bills, "OnlinePay").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(FinalTotal.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                     .Append(GetSum(Bills, "UserName").ToString()).Append("</td></tr>");
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
            F_TotalColl = T_TotalCashAmount.ConvertToDouble() + T_TotalCardAmount.ConvertToDouble() + T_TotalChequeAmount.ConvertToDouble() + T_TotalNEFTAmount.ConvertToDouble() + T_TotalOnlineAmount.ConvertToDouble();


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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


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

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

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

            double T_TotalAmount = 0, T_TotalNETAmount = 0, T_TotalDiscAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;


                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:right;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:right;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                ;
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

            return html;

        }

        //public string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        //{
        //    SqlParameter[] para = new SqlParameter[5];
        //    para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    para[2] = new SqlParameter("@SalesFromNumber", SalesFromNumber) { DbType = DbType.String };
        //    para[3] = new SqlParameter("@SalesToNumber", SalesToNumber) { DbType = DbType.String };
        //    para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
        //    var Bills = GetDataTableProc("RptSalesReturnReport", para);
        //    string html = File.ReadAllText(htmlFilePath);
        //    string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{HeaderName}}", htmlHeader);
        //    StringBuilder items = new StringBuilder("");
        //    int i = 0;

        //    double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;

        //        items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;margin-left:10px !important;text-align:center;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle;padding-left:10px;\">").Append(dr["SalesReturnNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


        //        T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
        //        T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

        //        //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',1545,1557,0,10016
        //    }

        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

        //    html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
        //    html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));

        //    return html;

        //}





        public string ViewSalesReturnPatientwiseReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
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

            double T_TotalNETAmount = 0, T_TotalSalespay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;margin-left:10px !important;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:left;vertical-align:middle;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;padding-left:10px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SalesDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle;padding-left:10px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;padding-left:10px;\">").Append(dr["AgingDays"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SalesAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");


                T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                T_TotalSalespay += dr["SalesAmount"].ConvertToDouble();

                
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalSalespay}}", T_TotalSalespay.To2DecimalPlace());

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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;height:10px;text-align:right;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IGSTAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

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


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            return html;
        }

        public string ViewSalesCreditReport(DateTime FromDate, DateTime ToDate, string SalesFromNumber, string SalesToNumber, int CreditReasonId, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
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
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalBalancepay = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


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



        public string ViewOPBillingpatientwiseReport(string PBillNo, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@PBillNo", PBillNo) { DbType = DbType.String };

            var Bills = GetDataTableProc("Retrieve_OPBill", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            //  string previousLabel = "";

            double G_BillAmount = 0, G_DiscAmount = 0, G_NetAmount = 0, G_BalAmount = 0, G_SalesBalAmount = 0, G_SalesCashAmount = 0, G_SalesTotalCardAmount = 0, G_SalesAmount = 0, G_SalesChequeAmount = 0, G_SalesTotalNEFTAmount = 0, G_SalesOnlineAmount = 0;

            double T_TotalBillAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["BillDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ServiceId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalBillAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ConcessionAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                T_TotalBillAmount += dr["TotalBillAmt"].ConvertToDouble();
                T_TotalDiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNETAmount += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalBalAmount += dr["BalanceAmt"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalCashAmount}}", T_TotalCashAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalCardAmount}}", T_TotalCardAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalChequeAmount}}", T_TotalChequeAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalNEFTAmount}}", T_TotalNEFTAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalOnlineAmount}}", T_TotalOnlineAmount.To2DecimalPlace());
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

                items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString()).Append("</td>");
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
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{GSTIN}}", Bills.GetColValue("GSTIN"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));

            html = html.Replace("{{SalesNo}}", Bills.GetColValue("SalesNo"));
            html = html.Replace("{{Date}}", Bills.GetColValue("Date"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{DL_NO}}", Bills.GetColValue("DL_NO"));



            return html;
        }

        public string ViewSalesReturnReceipt(int SalesReturnId, int OP_IP_Type, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@SalesID", SalesReturnId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rptSalesReturnPrint", para);
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

                items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/mm/yyyy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
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

        public string ViewPharmsDailyCollectionsummaryDayandUserwise(DateTime FromDate, DateTime ToDate, int StoreId, int AddedById, string htmlFilePath, string HeaderName)
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
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
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
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append("Sales").Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_NetAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_CardAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_ChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_NEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                   .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\"></td></tr>");
                    T_BillAmount = 0; T_DiscAmount = 0; T_NetAmount = 0; T_BalAmount = 0; T_PaidAmount = 0; T_CashAmount = 0; T_CardAmount = 0; T_ChequeAmount = 0; T_NEFTAmount = 0; T_OnlineAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");

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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["UserName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["TotalBillAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NEFTPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["OnlinePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_NetAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_CardAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_ChequeAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_NEFTAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\"><td></td></tr>");

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

                items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["Rate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ItemTotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["SGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IGSTAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["GrandTotalAmount"].ConvertToDouble()).Append("</td></tr>");
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

        public string ViewPharSalesCashBookReport(DateTime FromDate, DateTime ToDate,string PaymentMode, int StoreId, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@PaymentMode", PaymentMode) { DbType = DbType.String };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.String };

            var Bills = GetDataTableProc("m_rptSalesCashbook", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            double T_TotalNETAmount = 0, T_TotalPay = 0, T_TotalRoundOff = 0,T_BalancePay=0; 

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["RoundOff"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


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

                if(i==0)
                    
                items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                

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

                items.Append("<tr><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["MDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["DEBIT"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CREDIT"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PrintStoreName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-bottom:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["SalesNo"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


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
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"11\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PrintStoreName"].ConvertToString()).Append("</td></tr>");
                }
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: center;padding: 3px;height:10px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["DiscAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");



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
    }
}