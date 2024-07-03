using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{ 
    public class R_OPRefundBill : GenericRepository, I_OPRefundBill
    {
        public R_OPRefundBill(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public String Insert(OPRefundBillParams OPRefundBillParams)
        {
            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@RefundId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = OPRefundBillParams.InsertRefund.ToDictionary();
            disc1.Remove("RefundId");
            var RefundId = ExecNonQueryProcWithOutSaveChanges("m_insert_Refund_1", disc1, outputId1);


            foreach (var a in OPRefundBillParams.InsertOPRefundDetails)
            {
                var disc2 = a.ToDictionary();
                disc2["RefundID"] = RefundId;
                disc2["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("m_insert_T_RefundDetails_1", disc2);
            }

            foreach (var a in OPRefundBillParams.Update_AddCharges_RefundAmount)
            {

                var disc3 = a.ToDictionary();
                disc3["RefundAmount"] = OPRefundBillParams.InsertRefund.RefundAmount;
                ExecNonQueryProcWithOutSaveChanges("m_Update_AddCharges_RefundAmt", disc3);
            }

            var disc5 = OPRefundBillParams.InsertOPPayment.ToDictionary();
            disc5["RefundId"] = RefundId;
            disc5["BillNo"] = OPRefundBillParams.InsertRefund.BillId;
            disc5["AdvanceId"] = OPRefundBillParams.InsertRefund.AdvanceId;
            ExecNonQueryProcWithOutSaveChanges("m_insert_Payment_1", disc5);

            _unitofWork.SaveChanges();      
            return RefundId;
        }

        public string ViewOPRefundofBillReceipt(int RefundId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RefundId", RefundId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPRefundofBillPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            //htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;



            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RefundNo}}", Bills.GetColValue("RefundNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));

            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissinDate}}", Bills.GetColValue("AdmissinDate"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{RefundAmount}}", Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{MobileNo}}", Bills.GetColValue("PhoneNo"));

            html = html.Replace("{{RefundDate}}", Bills.GetColValue("RefundDate").ConvertToDateString());
            html = html.Replace("{{RefundTime}}", Bills.GetColValue("RefundTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            html = html.Replace("{{PBillNo}}", Bills.GetColValue("PBillNo").ConvertToString());
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
            html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());


            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{Date}}", Bills.GetDateColValue("Date").ConvertToDateString());
            html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));


            string finalamt = conversion(Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
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

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + "Only";

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
