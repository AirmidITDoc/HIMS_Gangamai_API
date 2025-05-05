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
            disc1.Remove("CashCounterId");
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_1", disc1, outputId1);

            foreach (var a in IPBillingParams.BillDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", disc);
            }

            IPBillingParams.Cal_DiscAmount_IPBill.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingParams.Cal_DiscAmount_IPBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingParams.AdmissionIPBillingUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_AdmissionforIPBilling", AdmissionID);

            var disc7 = IPBillingParams.IPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc7);


             IPBillingParams.IPBillBalAmount.BillNo = (int)Convert.ToInt64(BillNo);
             var disc2 = IPBillingParams.IPBillBalAmount.ToDictionary();
             ExecNonQueryProcWithOutSaveChanges("m_update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingParams.IPAdvanceDetailUpdate)
            {
                var disc = a.ToDictionary();
              //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("m_update_AdvanceDetail_1", disc);
            }

                      
            var disc4 = IPBillingParams.IPAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;

        }

        public String InsertCashCounter(IPBillingParams IPBillingParams)
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
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_CashCounter_1", disc1, outputId1);

            foreach (var a in IPBillingParams.BillDetailsInsert)
            {
                var disc = a.ToDictionary();
                disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", disc);
            }

            IPBillingParams.Cal_DiscAmount_IPBill.BillNo = (int)Convert.ToInt64(BillNo);
            var disc3 = IPBillingParams.Cal_DiscAmount_IPBill.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Cal_DiscAmount_OPBill", disc3);

            var AdmissionID = IPBillingParams.AdmissionIPBillingUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_T_AdmissionforIPBilling", AdmissionID);

            var disc7 = IPBillingParams.IPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            //disc7.Remove("PaymentId");
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc7);


            IPBillingParams.IPBillBalAmount.BillNo = (int)Convert.ToInt64(BillNo);
            var disc2 = IPBillingParams.IPBillBalAmount.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_BillBalAmount_1", disc2);

            foreach (var a in IPBillingParams.IPAdvanceDetailUpdate)
            {
                var disc = a.ToDictionary();
                //  disc["BillNo"] = BillNo;
                ExecNonQueryProcWithOutSaveChanges("m_update_AdvanceDetail_1", disc);
            }


            var disc4 = IPBillingParams.IPAdvanceHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_AdvanceHeader_1", disc4);




            _unitofWork.SaveChanges();
            return BillNo;

        }
        public bool BillDiscountAfterUpdate(BillDiscountAfterParams billDiscountAfterParams)
        {
            var disc1 = billDiscountAfterParams.BillDiscountAfterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_BillDiscountAfter_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }
          public bool PhBillDiscountAfterUpdate(PhBillDiscountAfter PhBillDiscountAfter)
        {
            var disc1 = PhBillDiscountAfter.PhBillDiscountAfterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_PhBillDiscountAfter", disc1);

            _unitofWork.SaveChanges();
            return true;
        }


            public string ViewIPBillDatewiseReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBillWithDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
           
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHospital}}", htmlHeader);
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

            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));


            html = html.Replace("{{chkcommflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "block" : "table-row");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "table-row");
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
                    items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }


            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            string finalamt = conversion(T_NetAmount.ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            return html;

        }

        public string ViewIPBillReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {
            //m_rptIPDFinalBill
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPD_FINAL_BILL_GROUPWISE", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string previousLabel = "";
            string deptLabel = "";
            String FinalLabel = "";
            double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;



            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++; j++;

            //    if (i == 1)
            //    {
            //        String Label2;
            //        Label2 = dr["ClassName"].ConvertToString();
            //        items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

            //    }

            //    if (i == 1 && Bills.Rows.Count != i)
            //    {

            //        String Label;
            //        Label = dr["GroupName"].ConvertToString();
            //        items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
            //    }
            //    if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
            //    {
            //        j = 1;
            //        items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")

            //       .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
            //        T_TotalAmount = 0;

            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

            //    }

            //    if (deptLabel != "" && deptLabel != dr["ClassName"].ConvertToString())
            //    {

            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td></tr>");

            //    }

            //    T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
            //    F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
            //    ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


            //    previousLabel = dr["GroupName"].ConvertToString();
            //    deptLabel = dr["ClassName"].ConvertToString();

            //    items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

            //    if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
            //    {

            //        //items.Append("<tr style='font-weight:bold'><td colspan='5' style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Grand Total Amount</td><td style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle\">")


            //           items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
            //        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
            //        //items.Append("<tr style='border:1px solid black;'><td colspan='5' style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
            //        //.Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

            //    }

            //    TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
            //    Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
            //    Tot_paidamt = dr["PaidAmount"].ConvertToDouble();
            //    //if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
            //    //{
            //    //    balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
            //    //}
            //    if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
            //    {
            //        BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
            //    }

            //}


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;

              
                    if (i == 1)
                    {

                        String Label2;
                        Label2 = dr["GroupName"].ConvertToString();
                        items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                    }

                  
                    if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                    {
                        j = 1;
                        items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                      .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                        T_TotalAmount = 0;

                        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                    }
                 

                T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


                previousLabel = dr["GroupName"].ConvertToString();
            

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                  items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                 .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                 

                }

                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                Tot_paidamt = dr["PaidAmount"].ConvertToDouble();
            
                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                }

            }



            html = html.Replace("{{Items}}", items.ToString());
      
             html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
            
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
           // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{CompDiscAmt}}", Bills.GetColValue("CompDiscAmt"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}",AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkcomdiscflag}}", Bills.GetColValue("CompDiscAmt").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");

           
            return html;

    }
        //public string ViewIPBillReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        //{

        //    SqlParameter[] para = new SqlParameter[1];

        //    para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
        //    var Bills = GetDataTableProc("m_rptIPDFinalBill", para);
        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    StringBuilder items = new StringBuilder("");
        //    int i = 0, j = 0;
        //    String[] GroupName;
        //    object GroupName1 = "";
        //    Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
        //    double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

        //    string previousLabel = "";

        //    String FinalLabel = "";
        //    double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;



        //    foreach (DataRow dr in Bills.Rows)
        //    {

        //        i++; j++;


        //        if (i == 1)
        //        {
        //            String Label;
        //            Label = dr["GroupName"].ConvertToString();
        //            items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
        //        }
        //        if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
        //        {
        //            j = 1;
        //            items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")

        //           .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
        //            T_TotalAmount = 0;

        //            items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

        //        }


        //        T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
        //        F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
        //        ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


        //        previousLabel = dr["GroupName"].ConvertToString();

        //        items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
        //        items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

        //        if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
        //        {

        //            //items.Append("<tr style='font-weight:bold'><td colspan='5' style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Grand Total Amount</td><td style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle\">")


        //            items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
        //         .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
        //            //items.Append("<tr style='border:1px solid black;'><td colspan='5' style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
        //            //.Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

        //        }

        //        TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
        //        Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
        //        Tot_paidamt = dr["PaidAmount"].ConvertToDouble();
        //        //if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
        //        //{
        //        //    balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
        //        //}
        //        if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
        //        {
        //            BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
        //        }

        //    }


        //    html = html.Replace("{{Items}}", items.ToString());

        //    html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

        //    string finalamt = conversion(Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
        //    html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


        //    html = html.Replace("{{BillNo}}", Bills.GetColValue("PBillNo"));
        //    html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

        //    html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
        //    html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
        //    html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
        //    html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
        //    html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
        //    html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
        //    html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


        //    html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //    html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

        //    html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

        //    html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //    html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //    html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
        //    html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

        //    html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));

        //    html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


        //    html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

        //    html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

        //    html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
        //    html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
        //    html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
        //    html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
        //    html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

        //    html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
        //    html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
        //    html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


        //    html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
        //    html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

        //    html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

        //    html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

        //    html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
        //    html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
        //    html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
        //    html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

        //    html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


        //    return html;

        //}

        public string ViewIPBillWardwiseReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDFinalBillWithDateWise", para);
            string html = File.ReadAllText(htmlFilePath);
           
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHospital}}", htmlHeader);
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
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));


            html = html.Replace("{{chkcommflag}}", Bills.GetColValue("ConcessionAmount").ConvertToDouble() > 0 ? "block" : "table-row");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "block" : "table-row");
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["RoomName"].ConvertToString();
                    items.Append("<tr style=\"font-size:15px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoomName"].ConvertToString();

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:15px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                    T_NetAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                    j++;
                }


            }
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            string finalamt = conversion(T_NetAmount.ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

            return html;
        }

           
        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            double finalTotal = dt.Compute("SUM(" + ColName + ")", "lbl='OP Bill'").ConvertToDouble();
            //double finalTotal = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
            return cash;
        }


        public string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            var afterfloating = ConvertNumbertoWords(Convert.ToInt64(j));

            // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + "only";

            return Content;
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
           {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
           {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public string ViewIPBillReceiptclasswise(int BillNo, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            //m_rptIPDFinalBill_classwise
                para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPD_FINAL_BILL_CLASSWISE", para);
            string html = File.ReadAllText(htmlFilePath);
           
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            String[] GroupName;
            object GroupName1="";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag=false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string GroupLable = "";
            string ClassLable = "";
            String FinalLabel = "";
            double T_TotAmount = 0, T_TotalAmt = 0, ChargesTotalamt = 0, T_TotalAmount=0, F_TotalAmount=0.0,AdminChares=0, Tot_paidamt=0;
            double Tot=0;
            int length = Bills.Rows.Count-1;

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                //if (i == 1)
                //{
                //    String Label2;
                //    Label2 = dr["ClassName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                //}

                //if (i == 1)
                //{
                //    String Label;
                //    Label = dr["GroupName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                //}

                //if (ClassLable != "" && ClassLable != dr["ClassName"].ConvertToString())
                //{

                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td></tr>");
                    
                //    if(Bills.Rows.Count == i)
                //    {
                //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //    }
                //}


                //if ((GroupLable != "" && GroupLable != dr["GroupName"].ConvertToString() && Bills.Rows.Count != i))
                //{
                //    j = 1;
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //  .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    T_TotalAmount = 0;

                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //}



                //T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                //F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                //ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:20px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");


                //if (i == length && GroupLable != dr["GroupName"].ConvertToString())
                //{

                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    Tot = T_TotalAmount;

                //}
                //if (Bills.Rows.Count > 0 && Bills.Rows.Count == i && i != length + 1)
                //{
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                //}

                //if (i == length + 1)
                //{

                //    T_TotalAmount = T_TotalAmount - Tot;
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                //}


                ClassLable = dr["ClassName"].ConvertToString();
                GroupLable = dr["GroupName"].ConvertToString();
                T_TotalAmt = dr["ChargesTotalAmt"].ConvertToDouble();
                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                Tot_paidamt = dr["PaidAmount"].ConvertToDouble();
              
                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                }
                
            }


            html = html.Replace("{{Items}}", items.ToString());
      
             html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{T_TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
            
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
           // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));

            html = html.Replace("{{chktotalflag}}", Bills.GetColValue("ChargesTotalAmt").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}",AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");

           
            return html;

        }


        public string ViewIPBillReceiptclassServicewise(int BillNo, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            //m_rptIPDFinalBill_classwise
            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPD_FINAL_BILL_CLASSWISE", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string GroupLable = "";
            string ClassLable = "";
            String FinalLabel = "";
            double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;
            double Tot = 0;
            int length = Bills.Rows.Count - 1;

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label2;
                    Label2 = dr["ClassName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                }

                //if (i == 1)
                //{
                //    String Label;
                //    Label = dr["GroupName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                //}

                if (ClassLable != "" && ClassLable != dr["ClassName"].ConvertToString())
                {

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td></tr>");

                    if (Bills.Rows.Count == i)
                    {
                        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                    }
                }


                //if ((GroupLable != "" && GroupLable != dr["GroupName"].ConvertToString() && Bills.Rows.Count != i))
                //{
                //    j = 1;
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //  .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    T_TotalAmount = 0;

                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //}



                T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                //if (i == length && GroupLable != dr["GroupName"].ConvertToString())
                //{

                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    Tot = T_TotalAmount;

                //}
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i && i != length + 1)
                {
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }

                if (i == length + 1)
                {

                    T_TotalAmount = T_TotalAmount - Tot;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }


                ClassLable = dr["ClassName"].ConvertToString();
                GroupLable = dr["GroupName"].ConvertToString();

                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                }

            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            return html;

        }

        public string ViewIPFinalBillReceiptNew(int BillNo, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            //m_rptIPDFinalBill_classwise
            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptIPD_FINAL_BILL_CLASSWISE", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string GroupLable = "";
            string ClassLable = "";
            String FinalLabel = "";
            double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;
            double Tot = 0;
            int length = Bills.Rows.Count - 1;

            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                //if (i == 1)
                //{
                //    String Label2;
                //    Label2 = dr["ClassName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                //}

                //if (i == 1)
                //{
                //    String Label;
                //    Label = dr["GroupName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                //}

                //if (ClassLable != "" && ClassLable != dr["ClassName"].ConvertToString())
                //{

                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td></tr>");

                //    if (Bills.Rows.Count == i)
                //    {
                //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //    }
                //}


                //if ((GroupLable != "" && GroupLable != dr["GroupName"].ConvertToString() && Bills.Rows.Count != i))
                //{
                //    j = 1;
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //  .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    T_TotalAmount = 0;

                //    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //}



                T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                //if (i == length && GroupLable != dr["GroupName"].ConvertToString())
                //{

                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    Tot = T_TotalAmount;

                //}
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i && i != length + 1)
                {
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }

                if (i == length + 1)
                {

                    T_TotalAmount = T_TotalAmount - Tot;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }


                ClassLable = dr["ClassName"].ConvertToString();
                GroupLable = dr["GroupName"].ConvertToString();

                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                }

            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            return html;

        }


        public string ViewIPCompanyFinalBillWithSR(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            //m_rptIPDFinalBill_classwise
            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPComBlPrtSummary_WithAdd_SR", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string GroupLable = "";
            string ClassLable = "";
            String FinalLabel = "";
            double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, NetPayableAmt = 0, Tot_paidamt = 0;
            double Tot = 0;
            int length = Bills.Rows.Count - 1;
            string previousLabel = "";
            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["DepartmentName"].ConvertToString();


                T_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                F_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                //if (i == length && GroupLable != dr["GroupName"].ConvertToString())
                //{

                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    Tot = T_TotalAmount;

                //}
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i && i != length + 1)
                {
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }

                if (i == length + 1)
                {

                    T_TotalAmount = T_TotalAmount - Tot;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }


                ClassLable = dr["ClassName"].ConvertToString();
                GroupLable = dr["GroupName"].ConvertToString();

                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
          
         

                //if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                //{
                //    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                //}

            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{BillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{SubTPACompanyName}}", Bills.GetColValue("SubTPACompanyName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));

            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{CompBillDate}}", Bills.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{NetPayableAmt}}", NetPayableAmt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            return html;

        }

        public string ViewIPCompanyFinalBill(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            //m_rptIPDFinalBill_classwise
            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPComBlPrtSummary_WithAdd", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            String[] GroupName;
            object GroupName1 = "";
            Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
            double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

            string GroupLable = "";
            string ClassLable = "";
            String FinalLabel = "";
            double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, NetPayableAmt = 0, Tot_paidamt = 0;
            double Tot = 0;
            int length = Bills.Rows.Count - 1;
            string previousLabel = "";
            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["DepartmentName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                    NetPayableAmt = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                }

                //Dcount = Dcount + 1;
                //T_Count = T_Count + 1;
                previousLabel = dr["DepartmentName"].ConvertToString();


                T_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                F_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

                items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                //if (i == length && GroupLable != dr["GroupName"].ConvertToString())
                //{

                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                //     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    Tot = T_TotalAmount;

                //}
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i && i != length + 1)
                {
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                     .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }

                if (i == length + 1)
                {

                    T_TotalAmount = T_TotalAmount - Tot;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                }


                //ClassLable = dr["ClassName"].ConvertToString();
                //GroupLable = dr["GroupName"].ConvertToString();

                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                //Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                //Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                }

            }


            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            string finalamt = conversion(Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{BillNo}}", Bills.GetColValue("PBillNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));


            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));

            html = html.Replace("{{SubTPACompanyName}}", Bills.GetColValue("SubTPACompanyName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{AdmittedDoctorName}}", Bills.GetColValue("AdmittedDoctorName"));

            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{CompBillDate}}", Bills.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
            // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{TotalAdvanceAmount}}", Bills.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{NetPayableAmt}}", NetPayableAmt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{UseName}}", Bills.GetColValue("UseName"));
            html = html.Replace("{{TDSAmount}}", Bills.GetColValue("TDSAmount"));
            html = html.Replace("{{AffilAmount}}", Bills.GetColValue("AffilAmount"));


            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chktdsflag}}", Bills.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkWrfAmountflag}}", Bills.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            return html;

        }

    }
}

