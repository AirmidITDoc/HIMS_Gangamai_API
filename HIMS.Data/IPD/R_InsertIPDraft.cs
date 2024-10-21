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
    public class R_InsertIPDraft :GenericRepository, I_InsertIPDraft
    {
        public R_InsertIPDraft(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

      
        public String Insert(InsertIPDraftParams InsertIPDraftParams)
        {
            //throw new NotImplementedException();

            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DRBNo",
                Value = 0,
                Direction = ParameterDirection.Output
            };


            var disc = InsertIPDraftParams.IPIntremdraftbillInsert.ToDictionary();
            disc.Remove("DRBNo");
            var DRNo = ExecNonQueryProcWithOutSaveChanges("insert_DRBill_1", disc, outputId);


            foreach (var a in InsertIPDraftParams.InterimBillDetailsInsert)
            {
                var disc1 = a.ToDictionary();
                disc1["DRNo"] = DRNo;
                ExecNonQueryProcWithOutSaveChanges("insert_T_DRBillDet_1", disc1);
            }


            _unitofWork.SaveChanges();
            return DRNo;
        }
        //187857
        public string ViewIPDraftBillReceipt(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetAmount = 0;
            string previousLabel = "";
            //String Label = "";
            String finalLabel = "";
            int rowlength = 0;

            var Bills = GetDataTableProc("m_rptIPD_DraftBillSummary_Print", para);
            rowlength = Bills.Rows.Count;
            double Tot_AfterAdvused = 0,Tot_Wothoutdedu=0, Tot_Balamt = 0, Tot_Advamt = 0, Tot_Advusedamt = 0, T_TotalAmount=0, F_TotalAmount=0, balafteradvuseAmount=0, BalancewdudcAmt=0,AdminChares = 0, TotalNetPayAmt = 0;

           
            
            double T_TotAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;
                    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                   .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                    T_TotalAmount = 0;

                    //items.Append("<tr style=\"font-size:18px;border-bottom: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                }


                T_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                F_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                //ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();
               

                previousLabel = dr["GroupName"].ConvertToString();


                items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");


                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceAmount"].ConvertToDouble();
                if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
                {
                    balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
                }
                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt).ConvertToDouble();
                }
                


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;font-weight:bold'><td colspan='5' style=\"font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #000;border-collapse: collapse;padding:3px;border-bottom:1px solid #000;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                    items.Append("<tr style='border:1px solid black;'><td colspan='5' style=\"font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Total</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                    .Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                  
                }



            }
         
            

            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));

            
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ToString());
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName").ToString());
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear").ToString());

            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName").ToString());

            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName").ToString());
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode").ToString());

                html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmt").ConvertToDouble().ToString("0.00"));

                html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
               html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));
                html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().ToString("0.00"));
                 html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
                html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
                html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
                html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
                html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
                string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                  html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ToString());
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ToString());
            
                html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));


            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row" : "none");

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", balafteradvuseAmount.ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");


            return html;
            }



        public string ViewIPDraftBillReceiptNew(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetAmount = 0;
            string previousLabel = "";
            //String Label = "";
            String finalLabel = "";
            int rowlength = 0;           

            var Bills = GetDataTableProc("m_rptIPD_DraftBillClassWise_Print", para);
            rowlength = Bills.Rows.Count;
            double Tot_AfterAdvused = 0, Tot_Wothoutdedu = 0, Tot_Balamt = 0, Tot_Advamt = 0, Tot_Advusedamt = 0, T_TotalAmount = 0, F_TotalAmount = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0, AdminChares = 0, TotalNetPayAmt = 0;



            double T_TotAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {

                i++; j++;


                //if (i == 1)
                //{
                //    String Label;
                //    Label = dr["GroupName"].ConvertToString();
                //    items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(Label).Append("</td></tr>");
                //}
                //if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                //{
                //    j = 1;
                //    items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                //   .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    T_TotalAmount = 0;

                //    //items.Append("<tr style=\"font-size:18px;border-bottom: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #cccccc;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                //}


                //T_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                //F_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                ////ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


                //previousLabel = dr["GroupName"].ConvertToString();


                items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");


                TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                Tot_Advamt = dr["AdvanceAmount"].ConvertToDouble();
                if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
                {
                    balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
                }
                if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                {
                    BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt).ConvertToDouble();
                }



                //if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                //{

                //    items.Append("<tr style='border:1px solid black;font-weight:bold'><td colspan='6' style=\"font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #000;border-collapse: collapse;padding:3px;border-bottom:1px solid #000;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                //        .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                //    items.Append("<tr style='border:1px solid black;'><td colspan='7' style=\"font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Total</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                //    .Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");


                //}



            }



            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));


            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo").ToString());
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo").ToString());

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName").ToString());
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear").ToString());
            html = html.Replace("{{ClassName}}", Bills.GetColValue("GenderName").ToString());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName").ToString());

            html = html.Replace("{{AdmissionDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName").ToString());
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDoctorName"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("AdmittedDoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DischargeDate}}", Bills.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode").ToString());

            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmt").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{AdvanceUsedAmount}}", Bills.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceBalAmount}}", Bills.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{TaxAmount}}", Bills.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ConcessionAmount}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{T_NetAmount}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{Qty}}", Bills.GetColValue("Qty"));
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            string finalamt = conversion(Bills.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
            html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ToString());
            html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ToString());

            html = html.Replace("{{AdvanceRefundAmount}}", Bills.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));

            html = html.Replace("{{AddedBy}}", Bills.GetColValue("AddedBy"));


            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row" : "none");

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkAdvflag}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalflag}}", balafteradvuseAmount.ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkadminflag}}", Bills.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");


            return html;
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

            var Content = beforefloating + ' ' + " RUPEES" + ' '  + " only";

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

    }
}
