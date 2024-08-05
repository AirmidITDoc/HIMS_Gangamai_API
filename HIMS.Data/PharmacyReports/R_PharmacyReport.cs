using HIMS.Common.Utility;
using HIMS.Data.PharmacyReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Emit;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_PharmacyReports : GenericRepository, I_PharmacyReports
    {


        public R_PharmacyReports(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string ViewPharmacyDailycollectionReport(DateTime FromDate, DateTime ToDate, int StoreId,int AddedById,string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_rptSalesDailyCollection", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);

            //Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");

            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_NetAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_NetAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;

            double G_BillNetAmount = 0, G_BillCashPayAmount = 0, G_BillChequePayAmount = 0, G_BillCardPayAmount = 0, G_BillNEFTPayAmount = 0, G_BillPayTMAmount = 0;

            double G_AdvNetAmount = 0, G_AdvCashPayAmount = 0, G_AdvChequePayAmount = 0, G_AdvCardPayAmount = 0, G_AdvNEFTPayAmount = 0, G_AdvPayTMAmount = 0;

            double G_RefundNetAmount = 0, G_RefundCashPayAmount = 0, G_RefundChequePayAmount = 0, G_RefundCardPayAmount = 0, G_RefundNEFTPayAmount = 0, G_RefundPayTMAmount = 0;

            double G_RefundAdvNetAmount = 0, G_RefundAdvCash = 0, G_RefundAdvCheque = 0, G_RefundAdvCard = 0, G_RefundAdvNEFTPayAmount = 0, G_RefundAdvPayTMAmount = 0;
            double G_GRNNetAmount = 0, G_GRNCashPayAmount = 0, G_GRNChequePayAmount = 0, G_GRNCardPayAmount = 0, G_GRNNEFTPayAmount = 0, G_GRNPayTMAmount = 0;


            double T_AddBillNetAmount = 0, T_AddBillCashPayAmount = 0, T_AddBillChequePayAmount = 0, T_AddBillCardPayAmount = 0, T_AddBillNEFTPayAmount = 0, T_AddBillPayTMAmount = 0;

            double T_AddBillrefundNetAmount = 0, T_AddBillrefundCashPayAmount = 0, T_AddBillrefundChequePayAmount = 0, T_AddBillrefundCardPayAmount = 0, T_AddBillrefundNEFTPayAmount = 0, T_AddBillrefundPayTMAmount = 0;

            double T_FinalNetAmount = 0, T_FinalCashPayAmount = 0, T_FinalChequePayAmount = 0, T_FinalCardPayAmount = 0, T_FinalNEFTPayAmount = 0, T_FinalPayTMAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Label"].ConvertToString();
                    items.Append("<tr style=\"font-size:25px;border: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                       .Append(G_NetAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                           .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_NetAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_BalanceAmount = 0;  G_AdvanceUsedAmount = 0;

                    items.Append("<tr style=\"font-size:25px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:15px;text-align:left;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");
                }


                G_NetAmount += dr["NetAmount"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                G_BalanceAmount += dr["BalanceAmount"].ConvertToDouble();
                previousLabel = dr["Label"].ConvertToString();


                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:15px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");

                //items.Append("<tr style=\"font-size:15px;font-family: sans-serif;border-bottom: 1px solid black;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;width: 100px;\">").Append(i).Append("</td>");


                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                        .Append(G_NetAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                           .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td></tr>");
                }
            }


            foreach (DataRow dr in Bills.Rows)
            {



                i++; j++;
              

                if (dr["Label"].ConvertToString() == "IP Advance")
                {
                    G_AdvNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_AdvCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_AdvChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_AdvCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_AdvNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                    G_AdvPayTMAmount += dr["PayTMAmount"].ConvertToDouble();

                    
                }

              

                if (dr["Label"].ConvertToString() == "IP Refund of Advance")
                {
                    G_RefundAdvNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_RefundAdvCash += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundAdvCheque += dr["ChequePayAmount"].ConvertToDouble();
                    G_RefundAdvCard += dr["CardPayAmount"].ConvertToDouble();
                    G_RefundAdvNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                    G_RefundAdvPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }
                if (dr["Label"].ConvertToString() == "Sales")
                {

                    G_BillNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_BillCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_BillChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_BillCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_BillNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                    G_BillPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }
                if (dr["Label"].ConvertToString() == "Sales Return ")
                {
                    G_RefundNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_RefundCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_RefundChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_RefundCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_RefundNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                    G_RefundPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }
                if (dr["Label"].ConvertToString() == "GRN-CASH")
                {
                    G_GRNNetAmount += dr["NetAmount"].ConvertToDouble();
                    G_GRNCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                    G_GRNChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                    G_GRNCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                    G_GRNNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                    G_GRNPayTMAmount += dr["PayTMAmount"].ConvertToDouble();


                }
            }
            T_AddBillNetAmount = G_BillNetAmount.ConvertToDouble() + G_AdvNetAmount.ConvertToDouble();
            T_AddBillCashPayAmount = G_BillCashPayAmount.ConvertToDouble() + G_AdvCashPayAmount.ConvertToDouble();
            T_AddBillChequePayAmount = G_BillChequePayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble();
            T_AddBillCardPayAmount = G_BillCardPayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble();
            T_AddBillNEFTPayAmount = G_BillNEFTPayAmount.ConvertToDouble() + G_AdvNEFTPayAmount.ConvertToDouble();
            T_AddBillPayTMAmount = G_BillPayTMAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble();
            T_AddBillrefundNetAmount = G_RefundNetAmount.ConvertToDouble() + G_RefundAdvNetAmount.ConvertToDouble();
            T_AddBillrefundCashPayAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundAdvCash.ConvertToDouble();
            T_AddBillrefundChequePayAmount = G_RefundChequePayAmount.ConvertToDouble() + G_RefundAdvCheque.ConvertToDouble();
            T_AddBillrefundCardPayAmount = G_RefundCardPayAmount.ConvertToDouble() + G_RefundAdvCard.ConvertToDouble();
            T_AddBillrefundNEFTPayAmount = G_RefundNEFTPayAmount.ConvertToDouble() + G_RefundAdvNEFTPayAmount.ConvertToDouble();
            T_AddBillrefundPayTMAmount = G_RefundPayTMAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();

            T_FinalNetAmount = T_AddBillNetAmount.ConvertToDouble() - T_AddBillrefundNetAmount.ConvertToDouble();
            T_FinalCashPayAmount = T_AddBillCashPayAmount.ConvertToDouble() - T_AddBillrefundCashPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalCardPayAmount = T_AddBillCardPayAmount.ConvertToDouble() - T_AddBillrefundCardPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalNEFTPayAmount = T_AddBillNEFTPayAmount.ConvertToDouble() - T_AddBillrefundNEFTPayAmount.ConvertToDouble();
            T_FinalPayTMAmount = T_AddBillPayTMAmount.ConvertToDouble() - T_AddBillrefundPayTMAmount.ConvertToDouble();



            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() + T_FinalNEFTPayAmount.ConvertToDouble() + T_FinalPayTMAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTPayAmount}}", T_NEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTMAmount}}", T_PayTMAmount.To2DecimalPlace());

            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{G_BillCashPayAmount}}", G_BillCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillCardPayAmount}}", G_BillCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillChequePayAmount}}", G_BillChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillNEFTPayAmount}}", G_BillNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_BillPayTMAmount}}", G_BillPayTMAmount.To2DecimalPlace());



            html = html.Replace("{{G_AdvCashPayAmount}}", G_AdvCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvCardPayAmount}}", G_AdvCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvChequePayAmount}}", G_AdvChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvNEFTPayAmount}}", G_AdvNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_AdvPayTMAmount}}", G_AdvPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{G_RefundCashPayAmount}}", G_RefundCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundCardPayAmount}}", G_RefundCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundChequePayAmount}}", G_RefundChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundNEFTPayAmount}}", G_RefundNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundPayTMAmount}}", G_RefundPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{G_RefundAdvCash}}", G_RefundAdvCash.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCard}}", G_RefundAdvCard.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCheque}}", G_RefundAdvCheque.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvNEFTPayAmount}}", G_RefundAdvNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvPayTMAmount}}", G_RefundAdvPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{G_GRNNetPayableAmt}}", G_RefundAdvCash.To2DecimalPlace());
            html = html.Replace("{{G_GRNCashPayAmount}}", G_RefundAdvCard.To2DecimalPlace());
            html = html.Replace("{{G_GRNChequePayAmount}}", G_RefundAdvCheque.To2DecimalPlace());
            html = html.Replace("{{G_GRNCardPayAmount}}", G_RefundAdvNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_GRNNEFTPayAmount}}", G_GRNNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_GRNPayTMAmount}}", G_GRNPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalNEFTPayAmount}}", T_FinalNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());







            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));



            return html;
        }

    }
    }