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


        public string ViewDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, int DoctorID, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("RptDoctorWisePatientCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0, Ipcount = 0, OPcount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Ipcount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Opcount"].ConvertToString()).Append("</td></tr>");





                if (dr["PatientType"].ConvertToString() == "IP Patient")
                {
                    Ipcount++;
                }
                else if (dr["PatientType"].ConvertToString() == "OP Patient")
                {
                    OPcount++;
                }

            }

            html = html.Replace("{{Ipcount}}", Ipcount.ToString());
            html = html.Replace("{{Opcount}}", OPcount.ToString());




            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewReferenceDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader, SqlDbType DoctorID)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.DateTime };
            var Bills = GetDataTableProc("RptReferenceDoctorWisePatientCount", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_Count = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OP Patient"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Lbl"].ConvertToString()).Append("</td></tr>");

                T_Count += dr["Lbl"].ConvertToDouble();
            }


            html = html.Replace("{{T_Count}}", T_Count.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string ViewConcessionReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader, SqlDbType DoctorID, SqlDbType OP_IP_Type)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int16 };
            para[3] = new SqlParameter("@DoctorID", DoctorID) { DbType = DbType.Int16 };
            var Bills = GetDataTableProc("rptIP_OP_ConcessionReport", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalAdvanceUseAmount = 0, T_TotalCashPayAmount = 0, T_TotalChequePayAmount = 0, T_TotalCardPayAmount = 0, T_TotalNEFTPayAmount = 0, T_TotalPayTMPayAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Visit_Adm_Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvanceUseAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscComments"].ConvertToString()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["ConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();
                T_TotalAdvanceUseAmount += dr["AdvanceUseAmount"].ConvertToDouble();
                T_TotalCashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                T_TotalChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                T_TotalCardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                T_TotalNEFTPayAmount += dr["NEFTPayAmount"].ConvertToDouble();
                T_TotalPayTMPayAmount += dr["PayTMAmount"].ConvertToDouble();
                
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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalNetPayableAmt = 0, T_TotalPaidAmount = 0, T_TotalBalanceAmt = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalNetPayableAmt += dr["TotalNetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmount += dr["TotalPaidAmount"].ConvertToDouble();
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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalNetAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientType"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");

                T_TotalNetAmount += dr["TotalNetAmount"].ConvertToDouble();

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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalPaidAmt = 0, T_TotalBalanceAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaidAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");


                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["TotalConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["TotalNetPayableAmt"].ConvertToDouble();
                T_TotalPaidAmt += dr["TotalPaidAmt"].ConvertToDouble();
                T_TotalBalanceAmt = T_TotalNetPayableAmt.ConvertToDouble() - T_TotalTotalAmt.ConvertToDouble();
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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalCashPayAmount = 0, T_TotalCardPayAmount = 0, T_TotalNEFTPayAmount = 0, T_TotalPayTMAmount = 0, T_TotalRefundAmount = 0;



            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PayTMAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefundAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionReason"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td></tr>");

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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDocName"].ConvertToString()).Append("</td></tr>");




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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalOPCollection = 0, T_TotalIPCollection = 0, T_TotalAdvCollection = 0, T_TotalOPBillRefund = 0, T_TotalIPBillRefund = 0, T_TotalIPAdvRefund = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PaymentDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvCollection"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OPBillRefund"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPBillRefund"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPAdvRefund"].ConvertToDouble()).Append("</td></tr>");


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

            html = html.Replace("{{T_TotalOPCollection}}", T_TotalOPCollection.To2DecimalPlace());
            html = html.Replace("{{T_TotalIPCollection}}", T_TotalIPCollection.To2DecimalPlace());
            html = html.Replace("{{T_TotalAdvCollection}}", T_TotalAdvCollection.To2DecimalPlace());
            html = html.Replace("{{T_TotalOPBillRefund}}", T_TotalOPBillRefund.To2DecimalPlace());
            html = html.Replace("{{T_TotalIPBillRefund}}", T_TotalIPBillRefund.To2DecimalPlace());
            html = html.Replace("{{T_TotalIPAdvRefund}}", T_TotalIPAdvRefund.To2DecimalPlace());

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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalTDSAmount = 0, T_TotalCashPayAmount = 0, T_TotalChequePayAmount = 0, T_TotalAdvanceUsedAmount = 0, T_TotalNEFTPayAmount = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TDSAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NEFTPayAmount"].ConvertToDouble()).Append("</td></tr>");



                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
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

            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalTDSAmount}}", T_TotalTDSAmount.To2DecimalPlace());
            html = html.Replace("{{TotalCashPayAmount}}", T_TotalCashPayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalChequePayAmount}}", T_TotalChequePayAmount.To2DecimalPlace());
            html = html.Replace("{{TotalAdvanceUsedAmount}}", T_TotalAdvanceUsedAmount.To2DecimalPlace());
            html = html.Replace("{{TotalNEFTPayAmount}}", T_TotalNEFTPayAmount.To2DecimalPlace());
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

            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_TotalTotalAmt = 0, T_TotalConcessionAmt = 0, T_TotalNetPayableAmt = 0, T_TotalBalanceAmt = 0;


            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td></tr>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td></tr>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td></tr>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td></tr>");

                T_TotalTotalAmt += dr["TotalAmt"].ConvertToDouble();
                T_TotalConcessionAmt += dr["TotalConcessionAmt"].ConvertToDouble();
                T_TotalNetPayableAmt += dr["TotalNetPayableAmt"].ConvertToDouble();
                T_TotalBalanceAmt = T_TotalNetPayableAmt.ConvertToDouble() - T_TotalTotalAmt.ConvertToDouble();
                


            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));


            html = html.Replace("{{TotalTotalAmt}}", T_TotalTotalAmt.To2DecimalPlace());
            html = html.Replace("{{TotalConcessionAmt}}", T_TotalConcessionAmt.To2DecimalPlace());
            html = html.Replace("{{TotalNetPayableAmt}}", T_TotalNetPayableAmt.To2DecimalPlace());
            html = html.Replace("{{TotalBalanceAmt}}", T_TotalBalanceAmt.To2DecimalPlace());
            return html;

        }

        public string ViewReferenceDoctorWisePatientCountReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }

        public string ViewConcessionReport(DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            throw new NotImplementedException();
        }
    }  
}
    


        
       
