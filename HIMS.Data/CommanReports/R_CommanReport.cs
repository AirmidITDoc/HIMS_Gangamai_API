using HIMS.Common.Utility;
using HIMS.Data.CommanReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_CommanReport : GenericRepository, I_CommanReport
    {
       

        public R_CommanReport(IUnitofWork unitofWork) : base(unitofWork)
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

            //Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");


            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_BillAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_PayTMAmount = 0, G_NEFTPayAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmt = 0;
            double T_BillAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_PayTMAmount = 0, T_NEFTPayAmount = 0,T_AdvanceUsedAmount = 0, T_BalanceAmt = 0;
            double G_OPBillAmount = 0, G_OPCashPayAmount = 0, G_OPChequePayAmount = 0, G_OPCardPayAmount = 0, G_OPPayTMAmount = 0, G_OPNEFTPayAmount = 0;
            double G_BillBillAmount = 0, G_BillCashPayAmount = 0, G_BillChequePayAmount = 0, G_BillCardPayAmount = 0, G_BillPayTMAmount = 0, G_BillNEFTPayAmount = 0;

            double G_AdvBillAmount = 0, G_AdvCashPayAmount = 0, G_AdvChequePayAmount = 0, G_AdvCardPayAmount = 0, G_AdvPayTMAmount = 0, G_AdvNEFTPayAmount = 0;

            double G_RefundBillAmount = 0, G_RefundCashPayAmount = 0, G_RefundChequePayAmount = 0, G_RefundCardPayAmount = 0, G_RefundPayTMAmount = 0, G_RefundNEFTPayAmount = 0;
            double G_OPRefundBillAmount = 0, G_OPRefundCashPayAmount = 0, G_OPRefundChequePayAmount = 0, G_OPRefundCardPayAmount = 0, G_OPRefundPayTMAmount = 0, G_OPRefundNEFTPayAmount = 0;
            double G_RefundAdvBillAmount = 0, G_RefundAdvCashAmount = 0, G_RefundAdvChequeAmount = 0, G_RefundAdvCardAmount = 0, G_RefundAdvPayTMAmount = 0, G_RefundAdvNEFTPayAmount = 0;
            double T_AddBillBillAmount = 0, T_AddBillCashPayAmount = 0, T_AddBillChequePayAmount = 0, T_AddBillCardPayAmount = 0, T_AddBillPayTMAmount = 0, T_AddBillNEFTPayAmount = 0;
            double T_AddBillOPrefundBillAmount = 0, T_AddBillOPrefundCashPayAmount = 0, T_AddBillOPrefundChequePayAmount = 0, T_AddBillOPrefundCardPayAmount = 0, T_AddBillOPrefundPayTMAmount = 0, T_AddBillOPrefundNEFTPayAmount = 0;
            double T_AddBillrefundBillAmount = 0, T_AddBillrefundCashPayAmount = 0, T_AddBillrefundChequePayAmount = 0, T_AddBillrefundCardPayAmount = 0, T_AddBillrefundPayTMAmount = 0, T_AddBillrefundNEFTPayAmount = 0;
            double T_FinalBillAmount = 0, T_FinalCashPayAmount = 0, T_FinalChequePayAmount = 0, T_FinalCardPayAmount = 0, T_FinalPayTMAmount = 0, T_FinalNEFTPayAmount = 0;
            double T_OPAmount = 0, T_IPAmount = 0, T_IPAdvanceAmount = 0, T_OPRefundbillAmount = 0, T_IPRefundbillAmount = 0, T_IPRefundAdvanceAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:25px;border: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='color:black;font-weight:bold;border:1px solid #000;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:20px;text-align:right;vertical-align:middle\">")
                       .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_BalanceAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append("</td></tr>");
                    G_BillAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0;G_PayTMAmount = 0; G_NEFTPayAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmt = 0;

                    items.Append("<tr style=\"font-size:25px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:15px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }


                G_BillAmount += dr["BillAmount"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                G_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();

                previousLabel = dr["Type"].ConvertToString();


               

                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;border-bottom: 1px solid black;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;width: 100px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["AddedByName"].ConvertToString()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='color:black;font-weight:bold;border:1px solid #000;'><td colspan='5' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                        .Append(G_BillAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                          .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_BalanceAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append("</td></tr>");
                }
            }


            foreach (DataRow dr1 in Bills.Rows)
            {


               
                i++; j++;

                if (dr1["Type"].ConvertToString() == "OPD Bill")
                {

                    G_OPBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_OPCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_OPChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_OPCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_OPNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_OPPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }
                if (dr1["Type"].ConvertToString() == "IP Bill")
                {
                   
                    G_BillBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_BillCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_BillChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_BillCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_BillNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_BillPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }

                if (dr1["Type"].ConvertToString() == "IP Advance")
                {
                   
                    G_AdvBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_AdvCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_AdvChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_AdvCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_AdvNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_AdvPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }
                if (dr1["Type"].ConvertToString() == "OP Refund of Bill")
                {

                    G_OPRefundBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_OPRefundCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_OPRefundChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_OPRefundCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_OPRefundNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_OPRefundPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }

                if (dr1["Type"].ConvertToString() == "IP Refund of Bill ")
                {
                    
                       G_RefundBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_RefundCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_RefundChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_RefundCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_RefundNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_RefundPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }

             
                if (dr1["Type"].ConvertToString() == "IP Refund of Advance")
                {
                    G_RefundAdvBillAmount += dr1["BillAmount"].ConvertToDouble();
                    G_RefundAdvCashAmount += dr1["CashPayAmount"].ConvertToDouble();
                    G_RefundAdvChequeAmount += dr1["ChequePayAmount"].ConvertToDouble();
                    G_RefundAdvCardAmount += dr1["CardPayAmount"].ConvertToDouble();
                    G_RefundAdvNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
                    G_RefundAdvPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


                }


            }
            T_AddBillBillAmount = G_BillBillAmount.ConvertToDouble() + G_AdvBillAmount.ConvertToDouble();
            T_AddBillCashPayAmount = G_OPCashPayAmount.ConvertToDouble()+ G_BillCashPayAmount.ConvertToDouble() + G_AdvCashPayAmount.ConvertToDouble();
            T_AddBillChequePayAmount = G_OPChequePayAmount.ConvertToDouble()+ G_BillChequePayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble();
            T_AddBillCardPayAmount = G_OPCardPayAmount.ConvertToDouble()+ G_BillCardPayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble();
            T_AddBillNEFTPayAmount = G_OPNEFTPayAmount.ConvertToDouble()+ G_BillNEFTPayAmount.ConvertToDouble() + G_AdvNEFTPayAmount.ConvertToDouble();
            T_AddBillPayTMAmount = G_OPPayTMAmount.ConvertToDouble()+ G_BillPayTMAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble();
            T_AddBillrefundBillAmount = G_OPPayTMAmount.ConvertToDouble()+ G_RefundBillAmount.ConvertToDouble() + G_RefundAdvBillAmount.ConvertToDouble();

            T_AddBillOPrefundCashPayAmount = G_OPRefundCashPayAmount.ConvertToDouble() + G_RefundAdvCashAmount.ConvertToDouble();
            T_AddBillOPrefundChequePayAmount = G_OPRefundChequePayAmount.ConvertToDouble() + G_RefundAdvChequeAmount.ConvertToDouble();
            T_AddBillOPrefundCardPayAmount = G_OPRefundCardPayAmount.ConvertToDouble() + G_RefundAdvCardAmount.ConvertToDouble();
            T_AddBillOPrefundNEFTPayAmount = G_OPRefundNEFTPayAmount.ConvertToDouble() + G_RefundAdvNEFTPayAmount.ConvertToDouble();
            T_AddBillOPrefundPayTMAmount = G_OPRefundPayTMAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();


            T_AddBillrefundCashPayAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundAdvCashAmount.ConvertToDouble();
            T_AddBillrefundChequePayAmount = G_RefundChequePayAmount.ConvertToDouble() + G_RefundAdvChequeAmount.ConvertToDouble();
            T_AddBillrefundCardPayAmount = G_RefundCardPayAmount.ConvertToDouble() + G_RefundAdvCardAmount.ConvertToDouble();
            T_AddBillrefundNEFTPayAmount = G_RefundNEFTPayAmount.ConvertToDouble() + G_RefundAdvNEFTPayAmount.ConvertToDouble();
            T_AddBillrefundPayTMAmount = G_RefundPayTMAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();

            T_FinalBillAmount = T_AddBillBillAmount.ConvertToDouble() - T_AddBillrefundBillAmount.ConvertToDouble();
            T_FinalCashPayAmount = T_AddBillCashPayAmount.ConvertToDouble() - T_AddBillrefundCashPayAmount.ConvertToDouble();
            //T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalCardPayAmount = T_AddBillCardPayAmount.ConvertToDouble() - T_AddBillrefundCardPayAmount.ConvertToDouble();
            T_FinalChequePayAmount = T_AddBillChequePayAmount.ConvertToDouble() - T_AddBillrefundChequePayAmount.ConvertToDouble();
            T_FinalNEFTPayAmount = T_AddBillNEFTPayAmount.ConvertToDouble() - T_AddBillrefundNEFTPayAmount.ConvertToDouble();
            T_FinalPayTMAmount = T_AddBillPayTMAmount.ConvertToDouble() - T_AddBillrefundPayTMAmount.ConvertToDouble();
          
            T_OPAmount = G_OPCashPayAmount.ConvertToDouble() + G_OPChequePayAmount.ConvertToDouble() + G_OPCardPayAmount.ConvertToDouble() + G_OPNEFTPayAmount.ConvertToDouble() + G_OPPayTMAmount.ConvertToDouble();
            T_IPAmount = G_BillCashPayAmount.ConvertToDouble() + G_BillChequePayAmount.ConvertToDouble() + G_BillCardPayAmount.ConvertToDouble() + G_BillNEFTPayAmount.ConvertToDouble() + G_BillPayTMAmount.ConvertToDouble();
            T_IPAdvanceAmount = G_AdvCashPayAmount.ConvertToDouble() + G_AdvChequePayAmount.ConvertToDouble() + G_AdvCardPayAmount.ConvertToDouble() + G_AdvNEFTPayAmount.ConvertToDouble() + G_AdvPayTMAmount.ConvertToDouble() ;
            T_OPRefundbillAmount = G_OPRefundCashPayAmount.ConvertToDouble() + G_OPRefundChequePayAmount.ConvertToDouble() + G_OPRefundCardPayAmount.ConvertToDouble() + G_OPRefundNEFTPayAmount.ConvertToDouble() + G_OPRefundPayTMAmount.ConvertToDouble();
            T_IPRefundbillAmount = G_RefundCashPayAmount.ConvertToDouble() + G_RefundChequePayAmount.ConvertToDouble() + G_RefundCardPayAmount.ConvertToDouble() + G_RefundNEFTPayAmount.ConvertToDouble() + G_RefundPayTMAmount.ConvertToDouble();
            T_IPRefundAdvanceAmount = G_RefundAdvCashAmount.ConvertToDouble() + G_RefundAdvChequeAmount.ConvertToDouble() + G_RefundAdvCardAmount.ConvertToDouble() + G_RefundAdvPayTMAmount.ConvertToDouble();
        
            //T_FinalPayTMAmount = T_AddPayTMAmount.ConvertToDouble() - T_SubPayTMAmount.ConvertToDouble();

            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() + T_FinalNEFTPayAmount.ConvertToDouble() + T_FinalPayTMAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTPayAmount}}", T_NEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTMAmount}}", T_PayTMAmount.To2DecimalPlace());

            html = html.Replace("{{TotalBalanceAmt}}", T_BalanceAmt.To2DecimalPlace());
            html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{T_BillAmount}}", T_BillAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{G_OPCashPayAmount}}", G_OPCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPCardPayAmount}}", G_OPCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPChequePayAmount}}", G_OPChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPNEFTPayAmount}}", G_OPNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPPayTMAmount}}", G_OPPayTMAmount.To2DecimalPlace());

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

            html = html.Replace("{{G_OPRefundCashPayAmount}}", G_OPRefundCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPRefundCardPayAmount}}", G_OPRefundCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPRefundChequePayAmount}}", G_OPRefundChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPRefundNEFTPayAmount}}", G_OPRefundNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_OPRefundPayTMAmount}}", G_OPRefundPayTMAmount.To2DecimalPlace());



            html = html.Replace("{{G_RefundCashPayAmount}}", G_RefundCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundCardPayAmount}}", G_RefundCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundChequePayAmount}}", G_RefundChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundNEFTPayAmount}}", G_RefundNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundPayTMAmount}}", G_RefundPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{G_RefundAdvCashAmount}}", G_RefundAdvCashAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvCardAmount}}", G_RefundAdvCardAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvChequeAmount}}", G_RefundAdvChequeAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvNEFTPayAmount}}", G_RefundAdvNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{G_RefundAdvPayTMAmount}}", G_RefundAdvPayTMAmount.To2DecimalPlace());

            html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalNEFTPayAmount}}", T_FinalNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());


            html = html.Replace("{{T_OPAmount}}", T_OPAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPAmount}}", T_IPAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPAdvanceAmount}}", T_IPAdvanceAmount.To2DecimalPlace());
            html = html.Replace("{{T_OPRefundbillAmount}}", T_OPRefundbillAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPRefundbillAmount}}", T_IPRefundbillAmount.To2DecimalPlace());
            html = html.Replace("{{T_IPRefundAdvanceAmount}}", T_IPRefundAdvanceAmount.To2DecimalPlace());


            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));



            return html;
        }

        //public string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string htmlHeader)
        //{
        //    //throw new NotImplementedException();
        //    SqlParameter[] para = new SqlParameter[4];

        //    para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
        //    para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

        //    var Bills = GetDataTableProc("rptIP_OP_Comman_DailyCollectionReport", para);

        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{NewHeader}}", htmlHeader);

        //    Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


        //    html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

        //    StringBuilder items = new StringBuilder("");

        //    int i = 0, j = 0;
        //    string previousLabel = "";
        //    double TotalCollection = 0;
        //    double G_BillAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
        //    double T_BillAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;

        //    double G_BillBillAmount = 0, G_BillCashPayAmount = 0, G_BillChequePayAmount = 0, G_BillCardPayAmount = 0, G_BillNEFTPayAmount = 0, G_BillPayTMAmount = 0, G_BillAdvanceUsedAmount = 0, G_BillBalanceAmount = 0;

        //    double G_AdvBillAmount = 0, G_AdvCashPayAmount = 0, G_AdvChequePayAmount = 0, G_AdvCardPayAmount = 0, G_AdvNEFTPayAmount = 0, G_AdvPayTMAmount = 0, G_AdvAdvanceUsedAmount = 0, G_AdvBalanceAmount = 0;

        //    double G_RefundBillAmount = 0, G_RefundCashPayAmount = 0, G_RefundChequePayAmount = 0, G_RefundCardPayAmount = 0, G_RefundNEFTPayAmount = 0, G_RefundPayTMAmount = 0, G_RefundAdvanceUsedAmount = 0, G_RefundBalanceAmount = 0;

        //    double G_RefundAdvBillAmount = 0, G_RefundAdvCashAmount = 0, G_RefundAdvChequeAmount = 0, G_RefundAdvCardAmount = 0, G_RefundAdvNEFTPayAmount = 0, G_RefundAdvPayTMAmount = 0, G_RefundAdvAdvanceUsedAmount = 0, G_RefundAdvBalanceAmount = 0;

        //    double T_AddBillAmount1 = 0, T_AddBillAmount = 0, T_AddCashPayAmount = 0, T_AddCardPayAmount = 0, T_AddChequePayAmount = 0, T_AddNEFTPayAmount = 0, T_AddPayTMAmount = 0, T_AddAdvanceUsedAmount = 0, T_FinalBalanceAmount = 0;

        //    double T_SubBillAmount = 0, T_SubCashPayAmount = 0, T_SubCardPayAmount = 0, T_SubChequePayAmount = 0, T_SubNEFTPayAmount = 0, T_SubPayTMAmount = 0;
        //    double T_AddBillBillAmount = 0, T_AddBillCashPayAmount = 0, T_AddBillChequePayAmount = 0, T_AddBillCardPayAmount = 0, T_AddBillNEFTPayAmount = 0, T_AddBillPayTMAmount = 0, T_AddBillAdvanceUsedAmount = 0, T_AddBillBalanceAmount = 0;
              

        //    double T_AddBillrefundBillAmount = 0, T_AddBillrefundCashPayAmount = 0, T_AddBillrefundChequePayAmount = 0, T_AddBillrefundCardPayAmount = 0, T_AddBillrefundNEFTPayAmount = 0, T_AddBillrefundPayTMAmount = 0, T_AddBillrefundAdvanceUsedAmount = 0, T_AddBillrefundBalanceAmount = 0;
        //    double T_OPBillAmount = 0, T_IPBillAmount = 0;
        //    double T_FinalBillAmount = 0, T_FinalCashPayAmount = 0, T_FinalCardPayAmount = 0, T_FinalChequePayAmount = 0, T_FinalNEFTPayAmount = 0, T_FinalPayTMAmount = 0, T_FinalAdvanceUsedAmount = 0;




           
        //    //double TotalCollection = 0;
        //    //double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NETPayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_DiscAmount = 0, G_NETAmount = 0, G_PaidAmount = 0, G_BalAmount = 0, G_TotAmount = 0;
        //    //double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NETPayAmount = 0, T_NEFTPayAmount = 0, T_PayTmAmount = 0;



        //    //double T_BillCash = 0, T_BillCard = 0, T_BillCheque = 0, T_BillNEFT = 0, T_BillPayTm = 0;
        //    //double T_TotAmount = 0, T_DiscAmount = 0, T_NetAmount = 0, T_BalAmount = 0, T_PaidAmount = 0;
        //    //double T_BillReturnCash = 0, T_BillReturnCard = 0, T_BillReturnCheque = 0, T_BillReturnNEFT = 0, T_BillReturnPAYTM = 0;
        //    //double T_BillReturnTot = 0, T_BillReturnDisc = 0, T_BillReturnNet = 0, T_BillReturnPaid = 0, T_BillReturnBal = 0;
        //    //double T_TotalCash = 0, T_TotalCard = 0, T_TotalCheque = 0, T_TotalNEFT = 0, T_TotalPAYTM = 0;


        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++; j++;
        //        if (i == 1)
        //        {
        //            String Label;
        //            Label = dr["Type"].ConvertToString();
        //            items.Append("<tr style=\"font-size:25px;border: 1px;color:black\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:15px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
        //        }

        //        if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
        //        {
        //            j = 1;


        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
        //              .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                 .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td>")
        //                .Append("</td></tr>");
        //            G_BillAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

        //            items.Append("<tr style=\"font-size:25px;border-bottom: 1px;color:black\"><td colspan=\"14\" style=\"border: 1px solid #d4c3c3; text-align:left; padding: 6px;\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
        //        }


        //        G_BillAmount += dr["BillAmount"].ConvertToDouble();
        //        G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
        //        G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
        //        G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
        //        G_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
        //        G_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
        //        G_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
        //        G_BalanceAmount += dr["BalanceAmt"].ConvertToDouble();
        //        previousLabel = dr["Type"].ConvertToString();

        //        T_BillAmount += dr["BillAmount"].ConvertToDouble();
        //        T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
        //        T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
        //        T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
        //        T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
        //        T_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
        //        T_AdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
        //        T_BalanceAmount += dr["BalanceAmt"].ConvertToDouble();

        //        //items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");

        //        items.Append("<tr style=\"font-size:10px;font-family: sans-serif;border-bottom: 1px solid black;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle;width: 100px;\">").Append(i).Append("</td>");



        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");

        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["AddedByName"].ConvertToString()).Append("</td></tr>");


        //        if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
        //        {

        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
        //                .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                 .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">")
        //                 .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td>")
        //                .Append("</td></tr>");
        //        }
        //    }




        //    foreach (DataRow dr1 in Bills.Rows)
        //    {

        //        i++; j++;
        //        if (dr1["Type"].ConvertToString() == "IP Bill" || dr1["Type"].ConvertToString() == "IP Advance" || dr1["Type"].ConvertToString() == "OPD Bill")
        //        {

        //            T_AddBillAmount += dr1["BillAmount"].ConvertToDouble();
        //            T_AddCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
        //            T_AddCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
        //            T_AddChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
        //            T_AddNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
        //            T_AddPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();

        //        }

        //        if (dr1["Type"].ConvertToString() == "IP Refund of Bill " || dr1["Type"].ConvertToString() == "IP Refund of Advance" || dr1["Type"].ConvertToString() == "OP Refund of Bill")
        //        {

        //            T_SubBillAmount += dr1["CashPayAmount"].ConvertToDouble();
        //            T_SubCashPayAmount += dr1["CashPayAmount"].ConvertToDouble();
        //            T_SubCardPayAmount += dr1["CardPayAmount"].ConvertToDouble();
        //            T_SubChequePayAmount += dr1["ChequePayAmount"].ConvertToDouble();
        //            T_SubNEFTPayAmount += dr1["NEFTPayAmount"].ConvertToDouble();
        //            T_SubPayTMAmount += dr1["PayTMAmount"].ConvertToDouble();


        //        }

        //        if (dr1["Type"].ConvertToString() == "OPD Bill")
        //        {

        //            T_OPBillAmount += dr1["BillAmount"].ConvertToDouble();

        //        }

        //        if (dr1["Type"].ConvertToString() == "IP Bill")
        //        {

        //            T_IPBillAmount += dr1["BillAmount"].ConvertToDouble();

        //        }

        //    }

        //    T_FinalBillAmount = T_OPBillAmount.ConvertToDouble() + T_IPBillAmount.ConvertToDouble();
        //    T_FinalCashPayAmount = T_AddCashPayAmount.ConvertToDouble() - T_SubCashPayAmount.ConvertToDouble();
        //    T_FinalCardPayAmount = T_AddCardPayAmount.ConvertToDouble() - T_SubCardPayAmount.ConvertToDouble();
        //    T_FinalChequePayAmount = T_AddChequePayAmount.ConvertToDouble() - T_SubChequePayAmount.ConvertToDouble();
        //    T_FinalNEFTPayAmount = T_AddNEFTPayAmount.ConvertToDouble() - T_SubNEFTPayAmount.ConvertToDouble();
        //    T_FinalPayTMAmount = T_AddPayTMAmount.ConvertToDouble() - T_SubPayTMAmount.ConvertToDouble();

        //    TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() + T_FinalNEFTPayAmount.ConvertToDouble() + T_FinalPayTMAmount.ConvertToDouble();

        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalNeftpay}}", T_NEFTPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalPayTmpay}}", T_PayTMAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());

        //    html = html.Replace("{{T_BillAmount}}", T_BillAmount.To2DecimalPlace());



        //    html = html.Replace("{{T_AddBillAmount}}", T_FinalBillAmount.To2DecimalPlace());

        //    html = html.Replace("{{G_CashPayAmount}}", G_CashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_ChequePayAmount}}", G_ChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_CardPayAmount}}", G_CardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_NEFTPayAmount}}", G_NEFTPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_PayTMAmount}}", G_PayTMAmount.To2DecimalPlace());


        //    html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalNEFTPayAmount}}", T_FinalNEFTPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());

        //    html = html.Replace("{{T_AddCashPayAmount}}", T_AddCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_AddCardPayAmount}}", T_AddCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_AddChequePayAmount}}", T_AddChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_AddNEFTPayAmount}}", T_AddNEFTPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_AddPayTMAmount}}", T_AddPayTMAmount.To2DecimalPlace());

        //    html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalPayTMAmount}}", T_PayTMAmount.To2DecimalPlace());

        //    html = html.Replace("{{TotalAdvUsed}}", T_AdvanceUsedAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_BillAmount}}", T_BillAmount.To2DecimalPlace());
        //    html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

        //    html = html.Replace("{{G_BillCashPayAmount}}", G_BillCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_BillCardPayAmount}}", G_BillCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_BillChequePayAmount}}", G_BillChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_BillNEFTPayAmount}}", G_BillNEFTPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_BillPayTMAmount}}", G_BillPayTMAmount.To2DecimalPlace());



        //    html = html.Replace("{{G_AdvCashPayAmount}}", G_AdvCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_AdvCardPayAmount}}", G_AdvCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_AdvChequePayAmount}}", G_AdvChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_AdvPayTMAmount}}", G_AdvPayTMAmount.To2DecimalPlace());

        //    html = html.Replace("{{G_RefundCashPayAmount}}", G_RefundCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_RefundCardPayAmount}}", G_RefundCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_RefundChequePayAmount}}", G_RefundChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{g_Refundpaytmamount}}", G_RefundPayTMAmount.To2DecimalPlace());


        //    html = html.Replace("{{G_RefundAdvCash}}", G_RefundAdvCashAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_RefundAdvCard}}", G_RefundAdvCardAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_RefundAdvCheque}}", G_RefundAdvChequeAmount.To2DecimalPlace());
        //    html = html.Replace("{{G_RefundAdvPayTMAmount}}", G_RefundAdvPayTMAmount.To2DecimalPlace());

        //    html = html.Replace("{{T_FinalCashPayAmount}}", T_FinalCashPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalCardPayAmount}}", T_FinalCardPayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalChequePayAmount}}", T_FinalChequePayAmount.To2DecimalPlace());
        //    html = html.Replace("{{T_FinalPayTMAmount}}", T_FinalPayTMAmount.To2DecimalPlace());




        //    html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

        //    html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
        //    html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));
        //    return html;
        //}


        public string ViewDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptDoctorWisePatientCount_Web", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0; 
                double T_count = 0, OPcount = 0, T_Ipcount = 0, T_OPcount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitCount"].ConvertToString()).Append("</td></tr>");

                //T_Ipcount += dr["VisitCount"].ConvertToString();
                T_count += dr["VisitCount"].ConvertToDouble();



                //if (dr["PatientType"].ConvertToString() == "IP Patient")
                //{
                //    Ipcount++;
                //}
                //else if (dr["PatientType"].ConvertToString() == "OP Patient")
                //{
                //    OPcount++;
                //}

            }

            //html = html.Replace("{{count}}", count.ToString());
            //html = html.Replace("{{Opcount}}", OPcount.ToString());



            html = html.Replace("{{T_count}}", T_count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewReferenceDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("RptReferenceDoctorWisePatientCount_Web", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double OPcount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VisitCount"].ConvertToString()).Append("</td>");


                //OPcount += dr["PatientType"].ConvertToDouble();
                if (dr["PatientType"].ConvertToString() == "OP Patient")
                {
                    OPcount++;
                }
            }



            html = html.Replace("{{OPcount}}", OPcount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewConcessionReport(DateTime FromDate, DateTime ToDate,int OP_IP_Type,  int DoctorID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int16 };
            para[3] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("rptIP_OP_ConcessionReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalAdvanceUseAmount = 0, T_TotalCashPayAmount = 0, T_TotalChequePayAmount = 0, T_TotalCardPayAmount = 0, T_TotalNEFTPayAmount = 0, T_TotalPayTMPayAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Visit_Adm_Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvanceUseAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscComments"].ConvertToString()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalAdvanceUseAmount += dr["AdvanceUseAmount"].ConvertToDouble();
                T_TotalCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_TotalChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_TotalCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_TotalNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_TotalPayTMPayAmount += dr["PayTMPayAmount"].ConvertToDouble();
                
            }


            
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceUseAmount}}", T_TotalAdvanceUseAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCashPayAmount}}", T_TotalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequePayAmount}}", T_TotalChequePayAmount.To2DecimalPlace());

            html = html.Replace("{{TotalCardPayAmount}}", T_TotalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTPayAmount}}", T_TotalNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTMPayAmount}}", T_TotalPayTMPayAmount.To2DecimalPlace());
            
            return html;

        }
        public string ViewCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptOP_IP_CreditBills", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalNetPayableAmt = 0, T_TotalPaidAmount = 0, T_TotalBalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_TotalBalanceAmt = T_TotalNetPayableAmt.ConvertToDouble() - T_TotalPaidAmount.ConvertToDouble();
            }


            
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmount}}", T_TotalPaidAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBalanceAmt}}", T_TotalBalanceAmt.To2DecimalPlace());
            return html;

        }
        public string ViewIPDischargeBillGenerationPendingReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[0];
            //para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            //para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("IPPatientDischargeBillGenerationPending", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalNetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalNetAmount += dr["NetAmount"].ConvertToDouble();

            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalNetAmount}}", T_TotalNetAmount.ToString());
            return html;

        }

        public string ViewIPBillGenerationPaymentDueReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptBillGeneratePatientPaymentDue", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalPaidAmt = 0, T_TotalBalanceAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");


                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmt += dr["PaidAmt"].ConvertToDouble();
                T_TotalBalanceAmt = T_TotalTotalAmt.ConvertToDouble() -T_TotalNetPayableAmt.ConvertToDouble();
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalPaidAmt}}", T_TotalPaidAmt.To2DecimalPlace());
            html = html.Replace("{{TotalBalanceAmt}}", T_TotalBalanceAmt.To2DecimalPlace());
            return html;

        }

        public string ViewCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptOPDCollectionReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalCashPayAmount = 0, T_TotalCardPayAmount = 0, T_TotalNEFTPayAmount = 0, T_TotalPayTMAmount = 0, T_TotalRefundAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionReason"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                
                T_TotalCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_TotalCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_TotalNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_TotalPayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                T_TotalRefundAmount += dr["RefundAmount"].ConvertToDouble();



            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalCashPayAmount}}", T_TotalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardPayAmount}}", T_TotalCardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTPayAmount}}", T_TotalNEFTPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalPayTMAmount}}", T_TotalPayTMAmount.To2DecimalPlace());
            html = html.Replace("{{TotalRefundAmount}}", T_TotalRefundAmount.To2DecimalPlace());
            return html;

        }

        public string ViewRefByPatientList(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptRefByAdmittedPatientList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td></tr>");




            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewDailyCollectionSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptCollectionSummary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalOPCollection = 0, T_TotalIPCollection = 0, T_TotalAdvCollection = 0, T_TotalOPBillRefund = 0, T_TotalIPBillRefund = 0, T_TotalIPAdvRefund = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPBillRefund"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPBillRefund"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPAdvRefund"].ConvertToDouble()).Append("</td></tr>");


                T_TotalOPCollection += dr["OPCollection"].ConvertToDouble();
                T_TotalIPCollection += dr["IPCollection"].ConvertToDouble();
                T_TotalAdvCollection += dr["AdvCollection"].ConvertToDouble();
                T_TotalOPBillRefund += dr["OPBillRefund"].ConvertToDouble();
                T_TotalIPBillRefund += dr["IPBillRefund"].ConvertToDouble();
                T_TotalIPAdvRefund += dr["IPAdvRefund"].ConvertToDouble();


            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{TotalOPCollection}}", T_TotalOPCollection.To2DecimalPlace());
            html = html.Replace("{{TotalIPCollection}}", T_TotalIPCollection.To2DecimalPlace());
            html = html.Replace("{{TotalAdvCollection}}", T_TotalAdvCollection.To2DecimalPlace());
            html = html.Replace("{{TotalOPBillRefund}}", T_TotalOPBillRefund.To2DecimalPlace());
            html = html.Replace("{{TotalIPBillRefund}}", T_TotalIPBillRefund.To2DecimalPlace());
            html = html.Replace("{{TotalIPAdvRefund}}", T_TotalIPAdvRefund.To2DecimalPlace());

            return html;

        }

        public string ViewIPCompanyWiseBillReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rtrv_IPCompanyWiseBillInfo", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalTDSAmount = 0, T_TotalCashPayAmount = 0, T_TotalChequePayAmount = 0, T_TotalAdvanceUsedAmount = 0, T_TotalNEFTPayAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TDSAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td></tr>");



                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalTDSAmount += dr["TDSAmount"].ConvertToDouble();
                T_TotalCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_TotalChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_TotalAdvanceUsedAmount += dr["AdvanceUsedAmount"].ConvertToDouble();
                T_TotalNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();

            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{T_TotalTDSAmount}}", T_TotalTDSAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalCashPayAmount}}", T_TotalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalChequePayAmount}}", T_TotalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalAdvanceUsedAmount}}", T_TotalAdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{T_TotalNEFTPayAmount}}", T_TotalNEFTPayAmount.To2DecimalPlace());
            return html;

        }

      

        public string ViewIPCompanyWiseCreditReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();4

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rtrv_IPCompanyWiseCreditReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_BalanceAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();


            }
            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ConvertToDouble().To2DecimalPlace());



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewServicewisepatinetamtReport(DateTime FromDate, DateTime ToDate, int ServiceId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ServiceId", ServiceId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptServiceWisePatAmt", para);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            string html = File.ReadAllText(htmlFilePath);
            double ServiceAmt = 0, Tot_ServiceAmt = 0;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);


            double G_ServiceAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"9\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append(G_ServiceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");
                    G_ServiceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");
                }


                Tot_ServiceAmt += dr["Amount"].ConvertToDouble();

                previousLabel = dr["GroupName"].ConvertToString();



                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["SCount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["Amount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    .Append(Tot_ServiceAmt.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");

                }
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{Tot_ServiceAmt}}", Tot_ServiceAmt.ConvertToDouble().To2DecimalPlace());

            return html;
        }

        public string ViewServicewiseReportwithbill(int ServiceId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];


            para[0] = new SqlParameter("@ServiceId", ServiceId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            //para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptServiceWiseReport_Detail_BillDateWise", para);
            StringBuilder items = new StringBuilder("");
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

         
            int i = 0, j = 0;
            double T_Count = 0, T_NetAmount = 0, T_Amount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["ServiceName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["ServiceName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Patient Wise Type Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_Amount.ToString()).Append("</td></tr>");

                    T_Amount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["ServiceName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChargesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddDoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_Amount += dr["NetAmount"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }
        public string ViewCanclechargeslist(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptCancelChargesList", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            string previousLabel = "";
          
            int i = 0,j = 0;
            double Tot_ServiceAmt = 0, NetAmount = 0, T_NetAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["PatientName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"9\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["PatientName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append(NetAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");
                    NetAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientName"].ConvertToString()).Append("</td></tr>");
                }


                NetAmount += dr["NetAmount"].ConvertToDouble();
               
                previousLabel = dr["PatientName"].ConvertToString();



                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionPercentage"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IsCancelledBy"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IsCancelledDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    .Append(NetAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");

                }

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;
        }
        //public string ViewServiceWiseReport(int ServiceId, DateTime FromDate, DateTime ToDate, int DoctorId,string htmlFilePath, string htmlHeader)
        //{
        //    SqlParameter[] para = new SqlParameter[4];


        //    para[0] = new SqlParameter("@ServiceId", ServiceId) { DbType = DbType.Int64 };
        //    para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
        //    para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

        //    var Bills = GetDataTableProc("rptServiceWiseReport_ChargesDetail", para);
        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;
        //    double T_Count = 0, T_NetAmount = 0, T_Amount = 0;

        //    string previousLabel = "";



        //    foreach (DataRow dr in Bills.Rows)
        //    {

        //        i++; j++;


        //        if (i == 1)
        //        {
        //            String Label;
        //            Label = dr["ServiceName"].ConvertToString();

        //            items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
        //        }
        //        if (previousLabel != "" && previousLabel != dr["ServiceName"].ConvertToString())
        //        {
        //            j = 1;

        //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Patient Wise Type Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
        //               .Append(T_Amount.ToString()).Append("</td></tr>");

        //            T_Amount = 0;
        //            items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td></tr>");

        //        }

        //        //Dcount = Dcount + 1;
        //        //T_Count = T_Count + 1;
        //        previousLabel = dr["ServiceName"].ConvertToString();


        //      items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChargesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
        //        items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddChargesDoctorName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VDoctorName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

        //        items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

        //        T_Amount += dr["NetAmount"].ConvertToDouble();
        //        T_NetAmount += dr["NetAmount"].ConvertToDouble();
        //    }

        //    html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


        //    return html;
        //}
        public string ViewGroupWiseCollectionReport(DateTime FromDate, DateTime ToDate, int GroupId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptGrupWisePeport", para);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            string html = File.ReadAllText(htmlFilePath);
            double ServiceAmt = 0, T_NetPayableAmt = 0, Tot_ServiceAmt = 0;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);


            double G_ServiceAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                     .Append(G_ServiceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");
                    G_ServiceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");
                }


                G_ServiceAmount += dr["NetPayableAmt"].ConvertToDouble();
                Tot_ServiceAmt += dr["NetPayableAmt"].ConvertToDouble();
                previousLabel = dr["GroupName"].ConvertToString();



                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(j).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["patientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
        

                T_NetPayableAmt += dr["NetAmount"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    .Append(Tot_ServiceAmt.To2DecimalPlace()).Append("</td></tr>");

                }
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Tot_ServiceAmt}}", Tot_ServiceAmt.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ConvertToDouble().To2DecimalPlace());

            return html;
        }
        public string ViewGroupwiseSummaryReport(DateTime FromDate, DateTime ToDate, int GroupId, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptGrupWiseSummaryPeport", para);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            string html = File.ReadAllText(htmlFilePath);
            double ServiceAmt = 0, T_NetPayableAmt = 0;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);


          

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;


                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                T_NetPayableAmt += dr["NetAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ConvertToDouble().To2DecimalPlace());

            return html;
        }

        public string ViewGroupwiseRevenueSummary(DateTime FromDate, DateTime ToDate, int GroupId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };


            var Bills = GetDataTableProc("rptGrupWisePeport", para);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            string html = File.ReadAllText(htmlFilePath);
            double ServiceAmt = 0, Tot_ServiceAmt = 0;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);


            double G_ServiceAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;


                items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["patientType"].ConvertToString()).Append("</td></tr>");

                G_ServiceAmount += dr["NetAmount"].ConvertToDouble();

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            html = html.Replace("{{G_ServiceAmount}}", G_ServiceAmount.ConvertToDouble().To2DecimalPlace());

            return html;
        }


        //public string ViewCommCollection(DateTime FromDate, DateTime ToDate, int GroupId, string htmlFilePath, string htmlHeader)
        //{
        //    SqlParameter[] para = new SqlParameter[2];
        //    para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

        //    var Bills = GetDataTableProc("rptOPDCollectionReport", para);


        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{HospitalHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;


        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;

        //        items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");



        //    }



        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;
        //}

        public string ViewBillSummaryWithTCS(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptBillwithTCS", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_BalanceAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_BalanceAmt = dr["BalanceAmt"].ConvertToDouble();



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

        //public string GroupwiseRevenueSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        //{
        //    SqlParameter[] para = new SqlParameter[2];
        //    para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
        //    para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

        //    var Bills = GetDataTableProc("rptGrpSumary", para);


        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;
        //    double T_Netamt = 0, T_paidamt = 0, T_discamt = 0, T_creditamt = 0;

        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;

        //        items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["patientType"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmt"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmt"].ConvertToDouble()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CreditAmt"].ConvertToDouble()).Append("</td></tr>");

        //        T_Netamt += dr["NetAmt"].ConvertToDouble();
        //        T_paidamt += dr["PaidAmt"].ConvertToDouble();
        //        T_discamt += dr["DiscAmt"].ConvertToDouble();
        //        T_creditamt += dr["CreditAmt"].ConvertToDouble();


        //    }

        //    html = html.Replace("{{T_Netamt}}", T_Netamt.ToString());
        //    html = html.Replace("{{T_paidamt}}", T_paidamt.ToString());
        //    html = html.Replace("{{T_discamt}}", T_discamt.ToString());
        //    html = html.Replace("{{T_creditamt}}", T_creditamt.ToString());
        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


        //    return html;

        //}
        public string ViewServiceWiseReportWithoutBill(int ServiceId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[3];


            para[0] = new SqlParameter("@ServiceId", ServiceId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            //para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptServiceWiseReport_Detail_ChargesDateWise", para); 


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, T_NetAmount = 0, T_Amount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["ServiceName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["ServiceName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Patient Wise Type Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_Amount.ToString()).Append("</td></tr>");

                    T_Amount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["ServiceName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["ServiceName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChargesDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_Amount += dr["NetAmount"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            //html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("T_NetAmount").ConvertToDouble().ToString("0.00"));
            //html = html.Replace("{{chkdnetamountflag}}", Bills.GetColValue("T_NetAmount").ConvertToDouble() > 0 ? "table-row" : "none");

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }


        public string ViewDoctorVisitAdmittedWiseGroupReport(DateTime FromDate, DateTime ToDate,int DoctorId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("rptDoctorWiseGroupWise", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetPayableAmt = 0, NetPayableAmt = 0;
            string previousLabel = "";

           

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"2\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //   .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //     .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                            .Append(NetPayableAmt.ConvertToDouble()).Append("</td></tr>");
                    // G_BalanceAmount = 0;
                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                }


                NetPayableAmt = NetPayableAmt;
                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='2' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">  Total </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetPayableAmt.ToString()).Append("</td></tr>");


                }

            }
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewGroupwiseRevenueSummary(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptGrpSumary", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetAmt = 0, T_PaidAmt = 0, T_DiscAmt = 0, T_CreditAmt = 0;
         
           

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    //Label = dr["patientType"].ConvertToString();
                    Label = dr["patientType"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"3\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                   
                }

                if (previousLabel != "" && previousLabel != dr["patientType"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                            .Append("</td></tr>");


                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["patientType"].ConvertToString()).Append("</td></tr>");
                }

                //if (previousLabel != "" && previousLabel != dr["patientType"].ConvertToString())
                //{
                //    j = 1;


                //    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">OP Revenue</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                //            .Append("</td></tr>");


                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["patientType"].ConvertToString()).Append("</td></tr>");
                //}


                previousLabel = dr["patientType"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmt"].ConvertToDouble()).Append("</td></tr>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td></tr>");



                T_NetAmt += dr["NetAmt"].ConvertToDouble();
                T_DiscAmt += dr["DiscAmt"].ConvertToDouble();
                T_PaidAmt += dr["PaidAmt"].ConvertToDouble();
                T_CreditAmt += dr["CreditAmt"].ConvertToDouble();
               
            }


            html = html.Replace("{{T_NetAmt}}", T_NetAmt.ToString());
            html = html.Replace("{{T_DiscAmt}}", T_DiscAmt.ToString());
            html = html.Replace("{{T_PaidAmt}}", T_PaidAmt.ToString());
            html = html.Replace("{{T_CreditAmt}}", T_CreditAmt.ToString());

            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["patientType"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmt"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmt"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CreditAmt"].ConvertToDouble()).Append("</td></tr>");

            //    T_NetAmt += dr["NetAmt"].ConvertToDouble();
            //    T_DiscAmt += dr["DiscAmt"].ConvertToDouble();
            //    T_PaidAmt += dr["PaidAmt"].ConvertToDouble();
               
            //    T_CreditAmt += dr["CreditAmt"].ConvertToDouble();


            //}

            //html = html.Replace("{{T_NetAmt}}", T_NetAmt.ToString());
            //html = html.Replace("{{T_DiscAmt}}", T_DiscAmt.ToString());
            //html = html.Replace("{{T_PaidAmt}}", T_PaidAmt.ToString());
         
            //html = html.Replace("{{T_CreditAmt}}", T_CreditAmt.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }

        public string ViewBillSummaryReportfor2LakhAmount(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];


           
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };

            var Bills = GetDataTableProc("rptIPbillsumry2lakh", para);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            string html = File.ReadAllText(htmlFilePath);
            double ServiceAmt = 0, T_TotalAmt = 0, T_ConcessionAmt = 0, T_NetPayableAmt = 0, T_CashPayAmount = 0, T_ChequePayAmount = 0, T_CardPayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_PaidAmt = 0, T_BalanceAmt = 0;
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);


            double G_ServiceAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                //items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                T_TotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_ConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_NEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_PayTMAmount += dr["PayTMAmount"].ConvertToDouble();
                T_PaidAmt += dr["PaidAmt"].ConvertToDouble();
                T_BalanceAmt += dr["BalanceAmt"].ConvertToDouble();

            }

            html = html.Replace("{{T_TotalAmt}}", T_TotalAmt.ToString());
            html = html.Replace("{{T_ConcessionAmt}}", T_ConcessionAmt.ToString());
            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.ToString());
            html = html.Replace("{{T_ChequePayAmount}}", T_ChequePayAmount.ToString());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.ToString());
            html = html.Replace("{{T_NEFTPayAmount}}", T_NEFTPayAmount.ToString());
            html = html.Replace("{{T_PayTMAmount}}", T_PayTMAmount.ToString());
            html = html.Replace("{{T_PaidAmt}}", T_PaidAmt.ToString());
            html = html.Replace("{{T_BalanceAmt}}", T_BalanceAmt.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }




        public string ViewDoctorAndDepartmentWiseMonthlyCollectionReport(int DepartmentId, int DoctorId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DepartmentId", DepartmentId) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptDoctorAndDepartmentWiseCollection", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Total_Amount = 0;



            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["DoctorName"].ConvertToString();
                    Label = dr["Lbl"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">OP</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                            .Append("</td></tr>");


                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Lbl"].ConvertToString())
                {
                    j = 1;


                    //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">OP</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                    //        .Append("</td></tr>");


                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Lbl"].ConvertToString()).Append("</td></tr>");
                }


                previousLabel = dr["DoctorName"].ConvertToString();

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Total_Amount"].ConvertToDouble()).Append("</td></tr>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPDNo"].ConvertToString()).Append("</td></tr>");



                T_Total_Amount += dr["Total_Amount"].ConvertToDouble();
               
            }


            html = html.Replace("{{T_Total_Amount}}", T_Total_Amount.ToString());

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Total_Amount"].ConvertToDouble()).Append("</td></tr>");


                T_Total_Amount += dr["Total_Amount"].ConvertToDouble();
               


            }

            html = html.Replace("{{T_Total_Amount}}", T_Total_Amount.ToString());
           
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }
       
        public string ViewCashCounterWiseDailyCollection(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int CashCounterId, int UserId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];


        
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@CashCounterId", CashCounterId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@UserId", UserId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCashCounterWiseDailyCollection", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetPayableAmt = 0, T_CashPayAmount = 0, T_ChequePayAmount = 0, T_CardPayAmount = 0 , T_neftpayamount = 0, T_PayTMPayAmount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["CashCounterName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["CashCounterName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_NetPayableAmt.ToString()).Append("</td></tr>");

                    T_NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["CashCounterName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["CashCounterName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
               
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["neftpayamount"].ConvertToDouble()).Append("</td>");
         
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPayAmount"].ConvertToDouble()).Append("</td></tr>");

                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_neftpayamount += dr["neftpayamount"].ConvertToDouble();
                T_PayTMPayAmount += dr["PayTMPayAmount"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.ToString());
            html = html.Replace("{{T_ChequePayAmount}}", T_ChequePayAmount.ToString());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.ToString());
            html = html.Replace("{{T_neftpayamount}}", T_neftpayamount.ToString());
            html = html.Replace("{{T_PayTMPayAmount}}", T_PayTMPayAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }


        public string ViewCashCounterWiseDailyCollectionSummary(DateTime FromDate, DateTime ToDate, int OP_IP_Type, int CashCounterId, int UserId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[5];



            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@CashCounterId", CashCounterId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@UserId", UserId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptCashCounterWiseDailyCollection", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetPayableAmt = 0, T_CashPayAmount = 0, T_ChequePayAmount = 0, T_CardPayAmount = 0, T_neftpayamount = 0, T_PayTMPayAmount = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["CashCounterName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"12\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["CashCounterName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(T_NetPayableAmt.ToString()).Append("</td></tr>");

                    T_NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"12\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["CashCounterName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["CashCounterName"].ConvertToString();


                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

              
               
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["neftpayamount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPayAmount"].ConvertToDouble()).Append("</td></tr>");

                T_NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_neftpayamount += dr["neftpayamount"].ConvertToDouble();
                T_PayTMPayAmount += dr["PayTMPayAmount"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetPayableAmt}}", T_NetPayableAmt.ToString());
            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.ToString());
            html = html.Replace("{{T_ChequePayAmount}}", T_ChequePayAmount.ToString());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.ToString());
            html = html.Replace("{{T_neftpayamount}}", T_neftpayamount.ToString());
            html = html.Replace("{{T_PayTMPayAmount}}", T_PayTMPayAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            return html;
        }
        public string ViewGroupwiseSummaryReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }
    }
}





