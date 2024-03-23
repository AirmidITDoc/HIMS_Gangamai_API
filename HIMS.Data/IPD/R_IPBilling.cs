using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPBilling:GenericRepository,I_IPBilling
    {
        public R_IPBilling(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(IPBillingParams IPBillingParams)
        {

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@BillNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            
            var disc1 = IPBillingParams.InsertBillUpdateBillNo.ToDictionary();
            disc1.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc1, outputId1);

            foreach (var a in IPBillingParams.BillDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc);
            }

            IPBillingParams.Cal_DiscAmount_IPBill.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingParams.Cal_DiscAmount_IPBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingParams.AdmissionIPBillingUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_AdmissionforIPBilling", AdmissionID);

            var disc7 = IPBillingParams.IPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);


             IPBillingParams.IPBillBalAmount.BillNo = (int)Convert.ToInt64(BillNo);
             var disc2 = IPBillingParams.IPBillBalAmount.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingParams.IPAdvanceDetailUpdate)
            {
                var disc = a.ToDictionary();
              //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("update_AdvanceDetail_1", disc);
            }

                      
            var disc4 = IPBillingParams.IPAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;



           /* var outputId1 = new SqlParameter
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

            var outputId3 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PaymentId ",
                Value = 0,
                Direction = ParameterDirection.Output
            };



            foreach (var a in IPBillingParams.InsertPathologyReportHeader)
            {
                var disc1 = a.ToDictionary();
                //disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportHeader_1", disc1);
            }

            foreach (var a in IPBillingParams.InsertRadiologyReportHeader)
            {
                var disc2 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_RadiologyReportHeader_1", disc2);
            }

            var disc3 = IPBillingParams.InsertBillupdatewithbillno.ToDictionary();
            disc3.Remove("BillNo");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("insert_Bill_UpdateWithBillNo_1_New", disc3, outputId1);

            foreach (var a in IPBillingParams.OpBillDetailsInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("insert_BillDetails_1", disc5);
            }

            // var disc4 = OPbillingparams.OPoctorShareGroupAdmChargeDoc.ToDictionary();
            // disc4["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc4.Remove("BillNo");
            //new  ExecNonQueryProcWithOutSaveChanges("ps_OP_Doctor_Share_Group_Adm_ChargeDoc_1", disc4);


            var disc6 = IPBillingParams.OPCalDiscAmountBill.ToDictionary();
            disc6["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("Cal_DiscAmount_OPBill", disc6);

            //IPBillingParams.BillDetailsInsert.BillNo = (int)Convert.ToInt64(BillNo);
            var disc7 = IPBillingParams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("insert_Payment_1", disc7);

            _unitofWork.SaveChanges();
            return true;*/
        }

      
        public string ViewIPBillDatewiseReceipt(int BillNo, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBillWithDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            
            object SelectedDate = "";
            Boolean chkcommflag = false, chkpaidflag = false;
            double T_NetAmount = 0;
            int j = Bills.Rows.Count;



            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetDateColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));

            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));

            html = html.Replace("{{chkcommflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "none");
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["BillDate"].ConvertToDateString("MM/dd/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["BillDate"].ConvertToDateString("MM/dd/yyyy");

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }


            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            return html;

        }

        public string ViewIPBillReceipt(int BillNo, string htmlFilePath,string htmlHeaderFilePath)
    {
      


            SqlParameter[] para = new SqlParameter[1];
        
            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBill", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            String[] GroupName;
            object GroupName1="";
            Boolean chkcommflag = false, chkpaidflag = false;
            double T_NetAmount = 0;
            int j = Bills.Rows.Count;

           

            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetDateColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));

            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));

            html = html.Replace("{{chkcommflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "none");
            string previousLabel = "";
            String Label ="";


            foreach (DataRow dr in Bills.Rows)
            {
                i++; 
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["GroupName"].ConvertToString();
                
                if (Label == previousLabel)
                {
                   
                    i++;
                    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }

               
            }
        html = html.Replace("{{Items}}", items.ToString());
      
        html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


        return html;

    }

        public string ViewIPBillWardwiseReceipt(int BillNo, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBillWithDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            object SelectedDate = "";
            Boolean chkcommflag = false, chkpaidflag = false;
            double T_NetAmount = 0;
            int j = Bills.Rows.Count;



            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetDateColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetDateColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetDateColValue("GenderName"));
            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));

            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));

            html = html.Replace("{{chkcommflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "none");
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["RoomName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoomName"].ConvertToString();

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;font-size:15px;text-align:center;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }


            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            return html;
        }

        public string ViewIPDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            

            var Bills = GetDataTableProc("rptIPDailyCollectionReport", para);

            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);

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
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"9\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["Type"].ConvertToString())
                {
                    j = 1;
                    

                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                       .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td></tr>");
                    G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
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
              
                items.Append("<tr style=\"font-size:15px;font-family: sans-serif;fborder-bottom: 1px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(i).Append("</td>");
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

                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='4' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
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



            TotalCollection = T_FinalCashPayAmount.ConvertToDouble() + T_FinalCardPayAmount.ConvertToDouble() + T_FinalChequePayAmount.ConvertToDouble() ;

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


        public string ViewCommanDailyCollectionReceipt(DateTime FromDate, DateTime ToDate, int AddedById, int DoctorId, string htmlFilePath, string htmlHeaderFilePath)
        {
            SqlParameter[] para = new SqlParameter[4];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@AddedById", AddedById) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptIP_OP_Comman_DailyCollectionReport", para);

            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");
           
            int i = 0, j = 0;
            string previousLabel = "";
            double TotalCollection = 0;
            double G_BillAmount = 0, G_CashPayAmount = 0, G_CardPayAmount = 0, G_ChequePayAmount = 0, G_NEFTPayAmount = 0, G_PayTMAmount = 0, G_AdvanceUsedAmount = 0, G_BalanceAmount = 0;
            double T_BillAmount = 0 ,T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0, T_NEFTPayAmount = 0, T_PayTMAmount = 0, T_AdvanceUsedAmount = 0, T_BalanceAmount = 0;


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
                      .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\"></td>")
                        .Append("</td></tr>");
                    G_BillAmount = 0; G_CashPayAmount = 0; G_CardPayAmount = 0; G_ChequePayAmount = 0; G_NEFTPayAmount = 0; G_PayTMAmount = 0; G_AdvanceUsedAmount = 0; G_BalanceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["Type"].ConvertToString()).Append("</td></tr>");
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
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BillAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;margin-left: 5px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;text-align:center;padding:3px;height:10px;text-align:center\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["BalanceAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AddedByName"].ConvertToString()).Append("</td></tr>");

                
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:red;background-color:#fdfed3'><td colspan='5' style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_BillAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_CashPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_ChequePayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_CardPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_NEFTPayAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_PayTMAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                        .Append(G_AdvanceUsedAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;border-left:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                         .Append(G_BalanceAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\"></td>")
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

        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            //double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            //double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash;
        }
    }
}
