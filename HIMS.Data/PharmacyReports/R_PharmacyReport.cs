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
            double T_IPAdvance = 0, T_IPRefundAdvance = 0, T_Sales = 0, T_SalesReturn = 0, T_GRNCASH = 0;

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


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
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
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:right;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
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


            foreach (DataRow dr1 in Bills.Rows)
            {



                i++; j++;
              

                if (dr1["Label"].ConvertToString() == "Advance")
                {
                    G_AdvNetAmount +=dr1["NetAmount"].ConvertToDouble();
                    G_AdvCashPayAmount +=dr1["CashPayAmount"].ConvertToDouble();
                    G_AdvChequePayAmount +=dr1["ChequePayAmount"].ConvertToDouble();
                    G_AdvCardPayAmount +=dr1["CardPayAmount"].ConvertToDouble();
                    G_AdvNEFTPayAmount +=dr1["NEFTPayAmount"].ConvertToDouble();
                    G_AdvPayTMAmount +=dr1["PayTMAmount"].ConvertToDouble();

                    
                }

              

                if (dr1["Label"].ConvertToString() == "Refund Of Advance")
                {
                    G_RefundAdvNetAmount += dr1["NetAmount"].ConvertToDouble();
                    G_RefundAdvCash += dr1["CashPayAmount"].ConvertToDouble();
                    G_RefundAdvCheque += dr1["ChequePayAmount"].ConvertToDouble();
                    G_RefundAdvCard += dr1["CardPayAmount"].ConvertToDouble();
                    G_RefundAdvNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_RefundAdvPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }
                if (dr1["Label"].ConvertToString() == "Sales")
                {

                    G_BillNetAmount += dr1["NetAmount"].ConvertToDouble();
                    G_BillCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_BillChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_BillCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_BillNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_BillPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }
                if (dr1["Label"].ConvertToString() == "Sales Return")
                {
                    G_RefundNetAmount += dr1["NetAmount"].ConvertToDouble();
                    G_RefundCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_RefundChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_RefundCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_RefundNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_RefundPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }
                if (dr1["Label"].ConvertToString() == "GRN-CASH")
                {
                    G_GRNNetAmount += dr1["NetAmount"].ConvertToDouble();
                    G_GRNCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_GRNChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_GRNCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_GRNNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_GRNPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


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

           
            T_IPAdvance = G_AdvCashPayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble() + G_AdvNEFTPayAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble();
           
            T_IPRefundAdvance = G_RefundAdvCash.ConvertToDouble() + G_RefundAdvCheque.ConvertToDouble() + G_RefundAdvCard.ConvertToDouble() + G_RefundAdvNEFTPayAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();
            T_Sales = G_BillCashPayAmount.ConvertToDouble() + G_BillChequePayAmount.ConvertToDouble() + G_BillCardPayAmount.ConvertToDouble() + G_BillNEFTPayAmount.ConvertToDouble() + G_BillPayTMAmount.ConvertToDouble();
            T_SalesReturn = G_RefundCashPayAmount.ConvertToDouble() + G_RefundChequePayAmount.ConvertToDouble() + G_RefundCardPayAmount.ConvertToDouble() + G_RefundNEFTPayAmount.ConvertToDouble() + G_RefundPayTMAmount.ConvertToDouble();
            T_GRNCASH = G_GRNCashPayAmount.ConvertToDouble() + G_GRNChequePayAmount.ConvertToDouble() + G_GRNCardPayAmount.ConvertToDouble() + G_GRNNEFTPayAmount.ConvertToDouble() + G_GRNPayTMAmount.ConvertToDouble();
           

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

            html = html.Replace("{{T_IPAdvance}}", T_IPAdvance.To2DecimalPlace());
            html = html.Replace("{{T_IPRefundAdvance}}", T_IPRefundAdvance.To2DecimalPlace());
            html = html.Replace("{{T_Sales}}", T_Sales.To2DecimalPlace());
            html = html.Replace("{{T_SalesReturn}}", T_SalesReturn.To2DecimalPlace());
            html = html.Replace("{{T_GRNCASH}}", T_GRNCASH.To2DecimalPlace());
          






            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));



            return html;
        }
        public string ViewSCHEDULEH1Report(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DrugTypeId", DrugTypeId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptSalesH1DrugReport", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            //double T_TotalAdvanceAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
       
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                //T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());


            return html;

        }
        public string ViewSCHEDULEH1SalesSummaryReport(DateTime FromDate, DateTime ToDate, int DrugTypeId, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DrugTypeId", DrugTypeId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptSalesH1DrugReport", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
              
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");


                T_NetAmount += dr["NetAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;

        }
        public string ViewSalesH1DrugCountReport(DateTime FromDate, DateTime ToDate,  int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
         
            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptSalesH1DrugCount", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            //double T_TotalAdvanceAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Address"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
    

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                //T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());


            return html;

        }

        public string ViewWardWiseHighRiskDrugList(DateTime FromDate, DateTime ToDate, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            para[2] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptIPPatientWardWiseSalesList", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            //double T_TotalAdvanceAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["BedName"].ConvertToString()).Append("</td></tr>");


           


                //T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());


            return html;

        }
        public string ViewPurchaseReOrderList(int StoreId, DateTime FromDate, DateTime ToDate,  string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

           


            var Bills = GetDataTableProc("rpt_PurReOrder_1", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            //double T_TotalAdvanceAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitofMeasurementId"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalQty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td></tr>");





                //T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());


            return html;
        }

        public string ViewPharmacyBillSummaryReport(int StoreId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };




            var Bills = GetDataTableProc("rptPharmacyBillSummaryReport", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BalanceAmount"].ConvertToDouble()).Append("</td></tr>");





                T_NetAmount += dr["NetAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());


            return html;
        }

        public string ViewItemWiseDailySalesReport(DateTime FromDate, DateTime ToDate, int ItemId, int RegNo, int StoreId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            para[2] = new SqlParameter("@ItemId", ItemId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@RegNo", RegNo) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@StoreId", StoreId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptSalesProductWiseDaily", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            //double T_TotalAdvanceAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SalesNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Time"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Type"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["label"].ConvertToString()).Append("</td></tr>");





                //T_TotalAdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{T_TotalAdvanceAmount}}", T_TotalAdvanceAmount.To2DecimalPlace());


            return html;

        }


        public string ViewDoctorWiseProfitReport( DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];


          
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptDrwise_patientwise_proftReport", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_NetAmount = 0, T_Amount = 0, T_ProfitAmount = 0;

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

                    //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='2' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //   .Append(T_Amount.ToString()).Append("</td></tr>");

                    //T_Amount = 0;
                    //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //  .Append(T_NetAmount.ToString()).Append("</td></tr>");
                    //T_NetAmount = 0;
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                      .Append(T_ProfitAmount.ToString()).Append("</td></tr>");
                    T_ProfitAmount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["DoctorName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChargesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalLandedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["profitamount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lable"].ConvertToString()).Append("</td></tr>");
               
                T_Amount += dr["TotalLandedAmount"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                T_ProfitAmount += dr["profitamount"].ConvertToDouble();
            }
            html = html.Replace("{{T_Amount}}", T_Amount.ToString());
            html = html.Replace("{{T_ProfitAmount}}", T_ProfitAmount.ToString());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }
    }
}