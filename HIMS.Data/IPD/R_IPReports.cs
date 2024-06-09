using HIMS.Common.Utility;
using System;
using System.Collections.Generic;
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

       
        public string ViewIPCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("rptIPDCreditBills", para);
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
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");


                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td></tr>");


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
            para[2] = new SqlParameter("@DoctorId", ToDate) { DbType = DbType.DateTime };
            para[3] = new SqlParameter("@WardId", ToDate) { DbType = DbType.DateTime };
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
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Count"].ConvertToString()).Append("</td>");

                T_Count += dr["Count"].ConvertToDouble();
            }
            html = html.Replace("{{Count}}", T_Count.To2DecimalPlace());
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
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IsMarkForDisNurDateTime"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeTime"].ConvertToDateString()).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiffTimeInHr"].ConvertToString()).Append("</td>");


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
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeTypeName"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append(" </td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Diagnosis"].ConvertToString()).Append("</td>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
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
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td>");

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
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td>");
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
            int i = 0;
            double T_TotalMedical = 0, T_TotalPathologyAmount = 0, T_TotalRadiologyAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Medical"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Pathology"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Radiology"].ConvertToDouble()).Append("</td>");
                T_TotalMedical += dr["Medical"].ConvertToDouble();
                T_TotalPathologyAmount += dr["Pathology"].ConvertToDouble();
                T_TotalRadiologyAmt += dr["Radiology"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalMedical}}", T_TotalMedical.To2DecimalPlace());
            html = html.Replace("{{TotalPathologyAmount}}", T_TotalPathologyAmount.To2DecimalPlace());
            html = html.Replace("{{TotalRadiologyAmt}}", T_TotalRadiologyAmt.To2DecimalPlace());
            return html;

        }

        public string ViewIPDAdmissionWardWiseCharges(int DoctorId, int WardId, int CompanyId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[3];
            // SqlParameter[] para1 = new SqlParameter[0];
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@WardId", WardId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@CompanyId", CompanyId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCurrentAdmittedListReportwithCharges", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);

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
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"11\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["WardId"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
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

                items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
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
                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
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

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionID"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");

            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());

            return html;
        }

        public string ViewIPDDischargetypewise(int DoctorId, DateTime FromDate, DateTime ToDate, int DischargeTypeId, string htmlFilePath, string htmlHeader)
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
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MobileNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Diagnosis"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeTypeName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeTime"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");

            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());



            html = html.Replace("{{Items}}", items.ToString());

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
            int i = 0;
            double T_TotalPatientName = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNO"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                T_TotalPatientName += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{PatientName}}", T_TotalPatientName.To2DecimalPlace());
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
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmMonthYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmCount"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["AdmCount"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Count}}", T_Count.To2DecimalPlace());
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
            double T_Count = 0;
            string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            //}

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                T_Count += dr["PatientName"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Count}}", T_Count.To2DecimalPlace());
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
            int i = 0;
            double T_TotalDoctorName = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                T_TotalDoctorName += dr["DoctorName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalDoctorName}}", T_TotalDoctorName.To2DecimalPlace());
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
            double T_Count = 0;
            string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            //}

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["DepartmentName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                T_Count += dr["PatientName"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Count}}", T_Count.To2DecimalPlace());
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
            int i = 0;
            double T_TotalDepartmentName = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                T_TotalDepartmentName += dr["DepartmentName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalDepartmentName}}", T_TotalDepartmentName.To2DecimalPlace());
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
            //double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0;
            string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            //}

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");
                //T_TotalTotalAmt" += dr["TotalAmt"].ConvertToDouble();
                //T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                //T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{TotalTotalAmt"}}", T_TotalTotalAmt".To2DecimalPlace());
            //html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            //html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
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
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
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
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0;
            string previousLabel = "";

            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitTime"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

            //}

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append("</td></tr>");
                    // G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");
                }



                previousLabel = dr["DepartmentName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{ConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{NetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
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
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
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

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AgeYear"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["PatientName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{PatientName}}", T_Count.To2DecimalPlace());
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

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["CompanyName"].ConvertToDouble();
            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Count}}", T_Count.To2DecimalPlace());
            return html;

        }
       
        }
}
