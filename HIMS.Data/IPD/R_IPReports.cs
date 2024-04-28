using HIMS.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
   public class R_IPReports:GenericRepository, I_IPReports
    {
        public R_IPReports(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptIP_OP_Comman_DailyCollectionReport", para);
           
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");

            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_BillAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_BillAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;


            double T_AddBillAmount1 = 0, T_AddBillAmount = 0, T_AddCashPayAmount = 0, T_AddCardPayAmount = 0, T_AddChequePayAmount = 0, T_AddNEFTPayAmount = 0, T_AddPayTMAmount = 0, T_AddAdvanceUsedAmount = 0, T_FinalBalanceAmount = 0;

            double T_SubBillAmount = 0, T_SubCashPayAmount = 0, T_SubCardPayAmount = 0, T_SubChequePayAmount = 0, T_SubNEFTPayAmount = 0, T_SubPayTMAmount = 0;

            double T_OPBillAmount = 0, T_IPBillAmount = 0;
            double T_FinalBillAmount = 0, T_FinalCashPayAmount = 0, T_FinalCardPayAmount = 0, T_FinalChequePayAmount = 0, T_FinalNEFTPayAmount = 0, T_FinalPayTMAmount = 0, T_FinalAdvanceUsedAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                      .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td>")
                        .Append("</td></tr>");
                    G_BillAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"14\" style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }


                G_BillAmount += dr["BillAmount"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                G_BalanceAmount += dr["BalanceAmt"].ConvertToDouble();
                previousLabel = dr["Type"].ConvertToString();

                T_BillAmount += dr["BillAmount"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                T_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                T_BalanceAmount += dr["BalanceAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AddedByName"].ConvertToString()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='5' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
                         .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td>")
                        .Append("</td></tr>");
                }
            }




            foreach (DataRow dr1 in Bills.Rows)
            {

                i++; j++;
                if (dr1["Type"].ConvertToString() == "IP Bill" || dr1["Type"].ConvertToString() == "IP Advance" || dr1["Type"].ConvertToString() == "OPD Bill")
                {

                    T_AddBillAmount += dr1["BillAmount"].ConvertToDouble();
                    T_AddCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    T_AddCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    T_AddChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    T_AddNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    T_AddPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();

                }

                if (dr1["Type"].ConvertToString() == "IP Refund of Bill " || dr1["Type"].ConvertToString() == "IP Refund of Advance" || dr1["Type"].ConvertToString() == "OP Refund of Bill")
                {

                    T_SubBillAmount += dr1["CashPayAmount"].ConvertToDouble();
                    T_SubCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    T_SubCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    T_SubChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    T_SubNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    T_SubPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }

                if (dr1["Type"].ConvertToString() == "OPD Bill")
                {

                    T_OPBillAmount += dr1["BillAmount"].ConvertToDouble();

                }

                if (dr1["Type"].ConvertToString() == "IP Bill")
                {

                    T_IPBillAmount += dr1["BillAmount"].ConvertToDouble();

                }

            }

            T_FinalBillAmount = T_OPBillAmount.ConvertToDouble() + T_IPBillAmount.ConvertToDouble();
            T_FinalCashPayAmount = T_AddCashPayAmount.ConvertToDouble() - T_SubCashPayAmount.ConvertToDouble();
            T_FinalCardPayAmount = T_AddCardPayAmount.ConvertToDouble() - T_SubCardPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddChequePayAmount.ConvertToDouble() - T_SubChequePayAmount.ConvertToDouble();
            T_FinalNEFTPayAmount = T_AddNEFTPayAmount.ConvertToDouble() - T_SubNEFTPayAmount.ConvertToDouble();
            T_FinalPayTMAmount = T_AddPayTMAmount.ConvertToDouble() - T_SubPayTMAmount.ConvertToDouble();

            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() + T_FinalNEFTPayAmount.ConvertToDouble() + T_FinalPayTMAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNeftpay}}", T_NEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTmpay}}", T_PayTMAmount.To2DecimalPlace());
            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());

            html = html.Replace("{{T_BillAmount}}", T_BillAmount.To2DecimalPlace());



            html = html.Replace("{{T_AddBillAmount}}", T_FinalBillAmount.To2DecimalPlace());

            html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalNEFTPayAmount}}", T_FinalNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
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

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");

            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_BillAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_BillAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;

            double G_IpBillAmount = 0, G_BillCashPayAmount = 0, G_BillCardPayAmount = 0, G_BillChequePayAmount = 0;

            double G_AdvBillAmount = 0, G_AdvCashPayAmount = 0, G_AdvCardPayAmount = 0, G_AdvChequePayAmount = 0;

            double G_RefundBillAmount = 0, G_RefundCashPayAmount = 0, G_RefundCardPayAmount = 0, G_RefundChequePayAmount = 0;

            double G_RefundAdvAmount = 0, G_RefundAdvCash = 0, G_RefundAdvCard = 0, G_RefundAdvCheque = 0;



            double T_AddBillAmount = 0, T_AddBillCashPayAmount = 0, T_AddBillCardPayAmount = 0, T_AddBillChequePayAmount = 0;

            double T_AddBillrefundAmount = 0, T_AddBillrefundCashPayAmount = 0, T_AddBillrefundCardPayAmount = 0, T_AddBillrefundChequePayAmount = 0;

            double T_FinalAmount = 0, T_FinalCashPayAmount = 0, T_FinalCardPayAmount = 0, T_FinalChequePayAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"9\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }


                G_BillAmount += dr["NetPayableAmt"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();

                previousLabel = dr["Type"].ConvertToString();

                //T_BillAmount += dr["NetPayableAmt"].ConvertToDouble();
                //T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                //T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                //T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();

                //T_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();

                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td></tr>");
                }
            }


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;
                if (dr["Type"].ConvertToString() == "IP Bill")
                {

                    G_BillChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_BillCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_BillCardPayAmount += dr["CardPayAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Advance")
                {

                    G_AdvCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_AdvCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_AdvChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Refund of Bill ")
                {

                    G_RefundCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_RefundChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "IP Refund of Advance")
                {

                    G_RefundAdvCash += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundAdvCard += dr["CardPayAmount"].ConvertToDouble();
                    G_RefundAdvCheque += dr["ChequePayAmount"].ConvertToDouble();


                }


            }

            T_AddBillCashPayAmount = G_BillCashPayAmount.ConvertToDouble() + G_AdvCashPayAmount.ConvertToDouble();
            T_AddBillCardPayAmount = G_BillCardPayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble();
            T_AddBillChequePayAmount = G_BillChequePayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble();

            T_AddBillrefundCashPayAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundAdvCash.ConvertToDouble();
            T_AddBillrefundCardPayAmount = G_RefundCardPayAmount.ConvertToDouble() + G_RefundAdvCard.ConvertToDouble();
            T_AddBillrefundChequePayAmount = G_RefundChequePayAmount.ConvertToDouble() + G_RefundAdvCheque.ConvertToDouble();


            T_FinalCashPayAmount = T_AddBillCashPayAmount.ConvertToDouble() - T_AddBillrefundCashPayAmount.ConvertToDouble();
            T_FinalCardPayAmount = T_AddBillCardPayAmount.ConvertToDouble() - T_AddBillrefundCardPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();



            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());

            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{T_BillAmount}}", T_BillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{G_BillCashPayAmount}}", G_BillCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillCardPayAmount}}", G_BillCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillChequePayAmount}}", G_BillChequePayAmount.To2DecimalPlace());


            html = html.Replace("{{G_AdvCashPayAmount}}", G_AdvCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvCardPayAmount}}", G_AdvCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvChequePayAmount}}", G_AdvChequePayAmount.To2DecimalPlace());


            html = html.Replace("{{G_RefundCashPayAmount}}", G_RefundCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundCardPayAmount}}", G_RefundCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundChequePayAmount}}", G_RefundChequePayAmount.To2DecimalPlace());


            html = html.Replace("{{G_RefundAdvCash}}", G_RefundAdvCash.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCard}}", G_RefundAdvCard.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCheque}}", G_RefundAdvCheque.To2DecimalPlace());

            html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());


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


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;

              
                if (i == 1)
                {
                    String Label;
                    Label = dr["lbl"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["lbl"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
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
                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_DiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_NetPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_BalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_CashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_OnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;border-left:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                          .Append(T_AdvpayAmount.To2DecimalPlace()).Append("</td></tr>");

                    items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_FinalTotalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalDiscAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalNETAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalPaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalBalAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalCashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(T_TotalOnlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(T_TotalAdvAmount.To2DecimalPlace()).Append("</td></tr>");
                }



            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
           // html = html.Replace("{{TotalBillAmount}}", T_TotalBillAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalBalAmount}}", T_TotalBalAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalCashAmount}}", T_TotalCashAmount.To2DecimalPlace());
            //html = html.Replace("{{FinalTotal}}", FinalTotal.To2DecimalPlace());
            //html = html.Replace("{{TotalChequeAmount}}", T_TotalChequeAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalNEFTAmount}}", T_TotalNEFTAmount.To2DecimalPlace());
            //html = html.Replace("{{TotalOnlineAmount}}", T_TotalOnlineAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
            //html = html.Replace("{{City}}", HospAddress.GetColValue("City"));
            //html = html.Replace("{{Phone}}", HospAddress.GetColValue("Phone"));

            return html;
        }
    }
}
