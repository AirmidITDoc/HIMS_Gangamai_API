using HIMS.Common.Utility;
using HIMS.Data.Opd.OP;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OPBillingReport : GenericRepository, I_OPBillingReport
    {
        
        public R_OPBillingReport(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIP_OP_Comman_DailyCollectionReport", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NETPayAmount = 0;
            double T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NETPayAmount = 0;

            //double G_UserCashPayAmount = 0, G_UserCardPayAmount = 0, G_UserChequePayAmount = 0, G_UserNETPayAmount = 0;
            //double T_UserCashPayAmount = 0, T_UserCardPayAmount = 0, T_UserChequePayAmount = 0, T_UserNETPayAmount = 0;


            double T_BillCash = 0, T_BillCard = 0, T_BillCheque = 0;
            double T_BillReturnCash = 0, T_BillReturnCard = 0, T_BillReturnCheque = 0;
            double T_TotalCash = 0, T_TotalCard = 0, T_TotalCheque = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["Type"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Type Wise GrandTotal</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NETPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;border-bottom:1px solid #000;border-left:1px solid #000;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
                }
                G_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();
                G_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                G_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                G_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();


                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_NETPayAmount += dr["NetPayableAmt"].ConvertToDouble();

                previousLabel = dr["AddedByName"].ConvertToString();

                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(j).Append("</td>");
                // items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["UserName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: left;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["ReceiptNo"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:right\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    //  items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"></td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //.Append("Total NetPay").Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //.Append("Total Cash").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //.Append("Total Cheque").Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                    //.Append("Total Card").Append("</td></tr>");



                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NETPayAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td>")
                        .Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\"></td></tr>");

                    //items.Append("<tr style='border:1px solid black;color:blue;font-weight:bold'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Sales + Cash GRN - Sales Return</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "NetPayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "CashPayAmount").To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "ChequePayAmount").To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    //    .Append(GetSum(Bills, "CardPayAmount").To2DecimalPlace()).Append("</td></tr>");
                }
            }








            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;
                if (dr["Type"].ConvertToString() == "OPD Bill")
                {

                    T_BillCash += dr["CashPayAmount"].ConvertToDouble();
                    T_BillCard += dr["CardPayAmount"].ConvertToDouble();
                    T_BillCheque += dr["ChequePayAmount"].ConvertToDouble();


                }

                if (dr["Type"].ConvertToString() == "OP Refund of Bill")
                {

                    T_BillReturnCash += dr["CashPayAmount"].ConvertToDouble();
                    T_BillReturnCard += dr["CardPayAmount"].ConvertToDouble();
                    T_BillReturnCheque += dr["ChequePayAmount"].ConvertToDouble();


                }


            }



            T_TotalCash = T_BillCash.ConvertToDouble() - T_BillReturnCash.ConvertToDouble();
            T_TotalCard = T_BillCard.ConvertToDouble() - T_BillReturnCard.ConvertToDouble();
            T_TotalCheque = T_BillCheque.ConvertToDouble() - T_BillReturnCheque.ConvertToDouble();


            TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble();

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalCashpay}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCardpay}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequepay}}", T_ChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNetpay}}", T_NETPayAmount.To2DecimalPlace());


            html = html.Replace("{{TotalCollection}}", TotalCollection.To2DecimalPlace());

            html = html.Replace("{{PrintStoreName}}", Bills.GetColValue("PrintStoreName"));
            html = html.Replace("{{StoreAddress}}", Bills.GetColValue("StoreAddress"));


            html = html.Replace("{{T_BillCash}}", T_BillCash.To2DecimalPlace());
            html = html.Replace("{{T_BillCard}}", T_TotalCard.To2DecimalPlace());
            html = html.Replace("{{T_BillCheque}}", T_BillCheque.To2DecimalPlace());

            html = html.Replace("{{T_BillReturnCash}}", T_BillReturnCash.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnCard}}", T_BillReturnCard.To2DecimalPlace());
            html = html.Replace("{{T_BillReturnCheque}}", T_BillReturnCheque.To2DecimalPlace());


            html = html.Replace("{{T_TotalCash}}", T_TotalCash.To2DecimalPlace());
            html = html.Replace("{{T_TotalCard}}", T_TotalCard.To2DecimalPlace());
            html = html.Replace("{{T_TotalCheque}}", T_TotalCheque.To2DecimalPlace());


            return html;
        }
    }
}
