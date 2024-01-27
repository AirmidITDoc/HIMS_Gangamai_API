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

        public string ViewOPPaymentReceipt(int PaymentId, string htmlFilePath, string HeaderName)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];
            Boolean chkchequeflag = false, chkNeftflag = true, chkcardflag = true, chkpaytmflag = true, chkcashflag = false;

            para[0] = new SqlParameter("@PaymentId", PaymentId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDPaymentReceiptPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            
            html = html.Replace("{{chkchequeflag}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkcashflag}}", Bills.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkcardflag}}", Bills.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkNeftflag}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "block" : "none");
            html = html.Replace("{{chkpaytmflag}}", Bills.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "block" : "none");
            
            html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{RegId}}", Bills.GetColValue("RegId"));
            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount"));

            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount"));
            html = html.Replace("{{ChequeDate}}", Bills.GetColValue("ChequeDate").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{ChequeNo}}", Bills.GetColValue("ChequeNo"));

            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount"));
            html = html.Replace("{{CardDate}}", Bills.GetColValue("CardDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{CardNo}}", Bills.GetColValue("CardNo"));
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount"));
            html = html.Replace("{{NEFTNo}}", Bills.GetColValue("NEFTNo"));
            html = html.Replace("{{NEFTBankMaster}}", Bills.GetColValue("NEFTBankMaster"));
            html = html.Replace("{{PayTMAmount}}", Bills.GetColValue("PayTMAmount"));
            html = html.Replace("{{PayTMTranNo}}", Bills.GetColValue("PayTMTranNo"));
            html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount"));
            html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{ReceiptNo}}", Bills.GetColValue("ReceiptNo"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
            html = html.Replace("{{PaymentDate}}", Bills.GetColValue("PaymentDate").ConvertToDateString("dd/MM/yyyy"));



            return html;
        }

        /* public bool Update(PaymentParams PaymentParams)
         {
             throw new NotImplementedException();
         }

         public bool Save(PaymentParams PaymentParams)
         {
             throw new NotImplementedException();
         }*/
    }
}
