using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


using Microsoft.Extensions.Primitives;

using System.IO;
using System.Net;

using System.Text.Json;

namespace HIMS.Data.Opd
{
   public class R_OpBilling :GenericRepository,I_OPbilling
    {
        public R_OpBilling(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(OPbillingparams OPbillingparams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            
            var outputId2 = new SqlParameter
            {
                 SqlDbType = SqlDbType.BigInt,
                 ParameterName = "@BillDetailId",
                 Value = 0,
                 Direction = ParameterDirection.Output
            };

            var VarChargeID = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ChargeID",
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


            var disc3 = OPbillingparams.InsertBillupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in OPbillingparams.ChargesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                disc5.Remove("ChargeID");
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("insert_OPAddCharges_1", disc5, VarChargeID);

                // Dill Detail Table Insert 
                Dictionary<string, Object> OPBillDet = new Dictionary<string, object>();
                OPBillDet.Add("BillNo", BillNo);
                OPBillDet.Add("ChargesID", ChargeID);
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", OPBillDet);

                if (a.IsPathology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("PathDate", a.ChargesDate);
                    PathParams.Add("PathTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("PathTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("IsSamplecollection", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", PathParams);
                }
                if (a.IsRadiology)
                {
                    Dictionary<string, Object> PathParams = new Dictionary<string, object>();

                    PathParams.Add("RadDate", a.ChargesDate);
                    PathParams.Add("RadTime", a.ChargesDate);
                    PathParams.Add("OPD_IPD_Type", a.OPD_IPD_Type);
                    PathParams.Add("OPD_IPD_Id", a.OPD_IPD_Id);
                    PathParams.Add("RadTestID", a.ServiceId);
                    PathParams.Add("AddedBy", a.AddedBy);
                    PathParams.Add("IsCancelled", 0);
                    PathParams.Add("ChargeID", ChargeID);
                    PathParams.Add("IsCompleted", 0);
                    PathParams.Add("IsPrinted", 0);
                    PathParams.Add("TestType", 0);

                    ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", PathParams);
                }
            }

            var disc7 = OPbillingparams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);
            
            
            // foreach (var a in OPbillingparams.InsertPathologyReportHeader)
            // {
            //     var disc1 = a.ToDictionary();
            //     //disc5["BillNo"] = BillNo;
            //     ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            // }

            // foreach (var a in OPbillingparams.InsertRadiologyReportHeader)
            // {
            //     var disc2 = a.ToDictionary();
            //    ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            // }



            // foreach (var a in OPbillingparams.OpBillDetailsInsert)
            //{
            //    var disc5 = a.ToDictionary();
            //    disc5["BillNo"] = BillNo;
            //    ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            // }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            //var disc6 = OPbillingparams.OPCalDiscAmountBill.ToDictionary();
            //disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            //ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

            //IPBillingParams.BillDetailsInsert.BillNo = (int)Convert.ToInt64(BillNo);



            _unitofWork.SaveChanges();
            return BillNo;
        }

        public string ViewOPBillDailyReportReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPDailyCollectionReport", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;

           
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            double T_NetAmount = 0, T_TotAmount=0, T_DiscAmount=0, T_PaidAmount=0,T_BalAmount=0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Number"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["TotalAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["ConcessionAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                T_TotAmount += dr["TotalAmt"].ConvertToDouble();
                T_DiscAmount += dr["ConcessionAmt"].ConvertToDouble();
                T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();
                T_BalAmount += dr["BalanceAmt"].ConvertToDouble();
                T_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
            }

           

            

            html = html.Replace("{{Items}}", items.ToString());


            html = html.Replace("{{T_TotAmount}}", T_TotAmount.To2DecimalPlace());
            html = html.Replace("{{T_DiscAmount}}", T_DiscAmount.To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{T_BalAmount}}", T_BalAmount.To2DecimalPlace());
            html = html.Replace("{{T_PaidAmount}}", T_PaidAmount.To2DecimalPlace());
            html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.To2DecimalPlace());
            html = html.Replace("{{T_ChequePayAmount}}", T_ChequePayAmount.To2DecimalPlace());



            html = html.Replace("{{FromDate}}", FromDate.ConvertToDateString());
            html = html.Replace("{{Todate}}", ToDate.ConvertToDateString());

            return html;
        }

        public String ViewOPBillReceipt(int BillNo, string htmlFilePath,string htmlHeaderFilePath)
        {
         

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptBillPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag=false;


            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmount"));
            html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString());
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{EmailId}}", Bills.GetColValue("EmailId"));
            html = html.Replace("{{Date}}", Bills.GetDateColValue("Date").ConvertToDateString());
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitDate").ConvertToDateString());
            html = html.Replace("{{PhoneNo}}", Bills.GetColValue("PhoneNo"));

            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["NetAmount"].ConvertToString()).Append("</td></tr>");

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            html = html.Replace("{{Items}}", items.ToString());


            html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmount"));
         
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));
            html = html.Replace("{{Price}}", Bills.GetColValue("Price"));
            html = html.Replace("{{TotalGst}}", Bills.GetColValue("TotalGst"));
            html = html.Replace("{{NetAmount}}", Bills.GetColValue("NetAmount"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("AddedByName").ConvertToString());

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "block" : "none");
            //html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "block" : "none");
            
            return html;

        }

        public string ViewOPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIP_OP_Comman_DailyCollectionReport", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{HeaderName}}", htmlHeader);
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

        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            //double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            //double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash;
        }

    }
}
