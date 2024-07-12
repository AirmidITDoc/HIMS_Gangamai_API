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
   public class R_CompanyInformation:GenericRepository,I_CompanyInformation
    {
        public R_CompanyInformation(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(CompanyInformationparam CompanyInformationparam)
        {
            //throw new NotImplementedException();


            var disc1 = CompanyInformationparam.CompanyUpdate.ToDictionary();
            var Id=ExecNonQueryProcWithOutSaveChanges("update_CompanyInfo", disc1);


            _unitofWork.SaveChanges();
            return (true);

        }

        public string ViewCompanyInformationReceipt(int AdmissionId, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionId", AdmissionId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rpt_CompanyInformationPrint", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;


            //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
            //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

            html = html.Replace("{{reason}}", Bills.GetColValue("reason"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceNo}}", Bills.GetColValue("AdvanceNo"));
            html = html.Replace("{{Addedby}}", Bills.GetColValue("Addedby"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));

            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{H_TotalAmt}}", Bills.GetColValue("H_TotalAmt").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{C_FinalBillAmt}}", Bills.GetColValue("C_FinalBillAmt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{AprovAmount}}", Bills.GetColValue("AprovAmount"));



            html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{ChequeNo}}", Bills.GetColValue("ChequeNo").ConvertToString());
            html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{CardBankName}}", Bills.GetColValue("CardBankName").ConvertToString());
            html = html.Replace("{{CardNo}}", Bills.GetColValue("CardNo").ConvertToString());
            html = html.Replace("{{BankName}}", Bills.GetColValue("BankName").ConvertToString());
            html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("PatientType").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{NEFTNo}}", Bills.GetColValue("NEFTNo").ConvertToString());
            html = html.Replace("{{NEFTBankMaster}}", Bills.GetColValue("NEFTBankMaster").ConvertToString());
            html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{PayTMTranNo}}", Bills.GetColValue("PayTMTranNo").ConvertToString());
            html = html.Replace("{{PayTMDate}}", Bills.GetColValue("PayTMDate").ConvertToDouble().To2DecimalPlace());




            html = html.Replace("{{chkcashflag}}", Bills.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkchequeflag}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkcardflag}}", Bills.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
            html = html.Replace("{{chkneftflag}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{chkpaytmflag}}", Bills.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");


            //string finalamt = conversion(Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
            //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


            html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }
    }
}
