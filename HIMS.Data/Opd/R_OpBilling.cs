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
    public class R_OpBilling : GenericRepository, I_OPbilling
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
            var BillNo = ExecNonQueryProcWithOutSaveChanges("m_insert_Bill_1", disc3, outputId1);

            foreach (var a in OPbillingparams.ChargesDetailInsert)
            {
                var disc5 = a.ToDictionary();
                disc5["BillNo"] = BillNo;
                disc5.Remove("ChargeID");
                var ChargeID = ExecNonQueryProcWithOutSaveChanges("m_insert_OPAddCharges_1", disc5, VarChargeID);

                // Dill Detail Table Insert 
                Dictionary<string, Object> OPBillDet = new Dictionary<string, object>();
                OPBillDet.Add("BillNo", BillNo);
                OPBillDet.Add("ChargesID", ChargeID);
                ExecNonQueryProcWithOutSaveChanges("m_insert_BillDetails_1", OPBillDet);

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

                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportHeader_1", PathParams);
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

                    ExecNonQueryProcWithOutSaveChanges("m_insert_RadiologyReportHeader_1", PathParams);
                }
            }

            var disc7 = OPbillingparams.OPInsertPayment.ToDictionary();
            disc7["BillNo"] = (int)Convert.ToInt64(BillNo);
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc7);

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
            html = html.Replace("{{NewHeader}}", htmlHeader);

            Boolean chkpaidflag = false, chkbalflag = false, chkremarkflag = false;


            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_NetAmount = 0, T_TotAmount = 0, T_DiscAmount = 0, T_PaidAmount = 0, T_BalAmount = 0, T_CashPayAmount = 0, T_CardPayAmount = 0, T_ChequePayAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Number"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaymentTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmt"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BalanceAmt"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["AdvanceUsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

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





        public String ViewOPBillReceipt(int BillNo, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@BillNo", BillNo) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptBillPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy H:mm"));
            html = html.Replace("{{PayMode}}", Bills.GetColValue("PayMode"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{ExtMobileNo}}", Bills.GetColValue("ExtMobileNo"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{Date}}", Bills.GetDateColValue("Date").ConvertToDateString());
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{PhoneNo}}", Bills.GetColValue("PhoneNo"));

            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));

            StringBuilder items = new StringBuilder("");
            int i = 0;
            double T_NetAmount = 0;
            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                T_NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            T_NetAmount = Math.Round(T_NetAmount);

            html = html.Replace("{{Items}}", items.ToString());


            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalBillAmount}}", Bills.GetColValue("TotalBillAmount").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{BalanceAmt}}", Bills.GetColValue("BalanceAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Price}}", Bills.GetColValue("Price").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{TotalGst}}", Bills.GetColValue("TotalGst").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{UserName}}", Bills.GetColValue("AddedByName").ConvertToString());
            html = html.Replace("{{HospitalName}}", Bills.GetColValue("HospitalName").ConvertToString());
            html = html.Replace("{{DiscComments}}", Bills.GetColValue("DiscComments").ConvertToString());

            html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
          
            html = html.Replace("{{chkbalflag}}", Bills.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");
           
            html = html.Replace("{{chkdiscflag}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

            //html = html.Replace("{{chkRefdrflag}}", Bills.GetColValue("chkRefdrflag").ConvertToDouble() != ' ' ? "table-row " : "none");
            //html = html.Replace("{{chkCompanyflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() != ' ' ? "table-row " : "none");


            string finalamt = conversion(T_NetAmount.ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            return html;

        }


        public double GetSum(DataTable dt, string ColName)
        {
            double cash = 0;
            //double Return = dt.Compute("SUM(" + ColName + ")", "Label='Sales Return'").ConvertToDouble();
            //double cash = dt.Compute("SUM(" + ColName + ")", "Label<>'Sales Return'").ConvertToDouble();
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

            //var Content = beforefloating + ' ' + " RUPEES" + ' ' + afterfloating + ' ' + " PAISE only";
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
    }
}
