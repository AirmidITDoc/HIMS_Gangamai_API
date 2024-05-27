using HIMS.Common.Utility;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_Payment :GenericRepository,I_Payment
    {
        public R_Payment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(PaymentParams PaymentParams)
        {
            var disc1 = PaymentParams.PaymentUpdate.ToDictionary();
            // ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BankMaster", disc1);
            
           ExecNonQueryProcWithOutSaveChanges("ps_Update_Payment_Advance", disc1);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PaymentParams PaymentParams)
        {
            // throw new NotImplementedException();
            var disc = PaymentParams.PaymentInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_Payment_New_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public string ViewOPPaymentReceipt(int PaymentId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            Boolean chkchequeflag = false, chkNeftflag = true, chkcardflag = true, chkpaytmflag = true, chkcashflag = false, chkremarkflag=false;
            //rptIPDPaymentReceiptPrint
            para[0] = new SqlParameter("@PaymentId", PaymentId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPDPaymentReceiptPrint", para);
           
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            
            html = html.Replace("{{chkchequeflag}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkcashflag}}", Bills.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkcardflag}}", Bills.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkNeftflag}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkpaytmflag}}", Bills.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkremarkflag}}", Bills.GetColValue("Remark").ConvertToString() != null ? "block" : "none");


            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegId}}", Bills.GetColValue("RegId"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());

            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequeDate}}", Bills.GetColValue("ChequeDate").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{ChequeNo}}", Bills.GetColValue("ChequeNo"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CardDate}}", Bills.GetColValue("CardDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{CardNo}}", Bills.GetColValue("CardNo"));
            html = html.Replace("{{CardBankName}}", Bills.GetColValue("CardBankName"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTNo}}", Bills.GetColValue("NEFTNo"));
            html = html.Replace("{{NEFTBankMaster}}", Bills.GetColValue("NEFTBankMaster"));
            html = html.Replace("{{PayTMAmount}}", Bills.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMTranNo}}", Bills.GetColValue("PayTMTranNo"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{ReceiptNo}}", Bills.GetColValue("ReceiptNo"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy HH:mm tt"));


            html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());

            string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

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

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + afterfloating + ' ' + " PAISE only";

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
