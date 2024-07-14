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
            ExecNonQueryProcWithOutSaveChanges("m_insert_MLCInfo_1", dic);

                     
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(MLCInfoParams MLCInfoParams)
        {
            var disc1 = MLCInfoParams.UpdateMLCInfo.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_MLCInfo_1", disc1);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewMlcReport(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_MLCCasePaperPrint", para);
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

            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{ReportingDate}}", Bills.GetColValue("ReportingTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));

           
            html = html.Replace("{{MLCNo}}", Bills.GetColValue("MLCNo").ConvertToString());
           
            html = html.Replace("{{AuthorityName}}", Bills.GetColValue("AuthorityName").ConvertToString());
            html = html.Replace("{{BuckleNo}}", Bills.GetColValue("BuckleNo").ConvertToString());
            html = html.Replace("{{PoliceStation}}", Bills.GetColValue("PoliceStation").ConvertToString());
          
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
            html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
            html = html.Replace("{{DetailGiven}}", Bills.GetColValue("DetailGiven"));


            html = html.Replace("{{chkRemarkflag}}", Bills.GetColValue("Remark") !=null ? "table-row " : "none");

            html = html.Replace("{{chkgivenflag}}", Bills.GetColValue("DetailGiven").ConvertToString() != "" ? "table -row " : "none");

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
