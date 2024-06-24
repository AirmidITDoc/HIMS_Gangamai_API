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
   public class R_MLCInfo :GenericRepository,I_MLCInfo
    {
        public R_MLCInfo(IUnitofWork unitofWork) : base(unitofWork)

        {
            //transaction and connection
        }


        public bool Insert(MLCInfoParams MLCInfoParams)
        {
            //throw new NotImplementedException();

            var dic = MLCInfoParams.InsertMLCInfo.ToDictionary();
         // dic.Remove("MLCId");
            ExecNonQueryProcWithOutSaveChanges("insert_MLCInfo_1", dic);

                     
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(MLCInfoParams MLCInfoParams)
        {
            var disc1 = MLCInfoParams.UpdateMLCInfo.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewMlcReport(int AdvanceDetailID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdvanceDetailID", AdvanceDetailID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPDAdvancePrint", para);
            string html = File.ReadAllText(htmlFilePath);


            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
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

            html = html.Replace("{{Age}}", Bills.GetColValue("Age"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));


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
            html = html.Replace("{{PayTMAmount}}", Bills.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
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
            //throw new NotImplementedException();
        }
    }
}
