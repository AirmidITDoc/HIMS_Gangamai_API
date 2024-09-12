using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace HIMS.Data.Radiology
{
    public class R_RadiologyTemplateResult:GenericRepository,I_RadiologyTemplateResult
    {
        public R_RadiologyTemplateResult(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public DataTable GetDataForReport(int RadReportId, int OP_IP_Type)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@RadReportId", RadReportId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            return GetDataTableProc("rptRadiologyReportPrint", para);
        }

        public bool Update(RadiologyTemplateResultParams RadiologyTemplateResultParams)
        {

            var RadReportId = RadiologyTemplateResultParams.RadiologyReportHeaderUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_T_RadiologyReportHeader_1", RadReportId);


            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewRadiologyTemplateReceipt(DataTable Bills, string htmlFilePath, string htmlHeader)
        {
            //  throw new NotImplementedException();

            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantName}}", Bills.GetColValue("ConsultantName"));
          //  html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString("dd/MM/yy | hh:mm tt"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{RadiologyDocName}}", Bills.GetColValue("RadiologyDocName"));
            html = html.Replace("{{ResultEntry}}", Bills.GetColValue("ResultEntry").ConvertToString());
            html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));
            html = html.Replace("{{SuggestionNotes}}", Bills.GetColValue("SuggestionNotes"));



           // String v = Bills.GetColValue("ResultEntry").Con);
            //String v1= Bills.GetColValue("ResultEntry")

            //html = html.Replace("{{ResultEntry}}", v);




            //{ { PathTemplateDetailsResult || innerHTML} }
            //{ { PathTemplateDetailsResult} }.innerHTML

            html = html.Replace("{{RadiologyDocName}}", Bills.GetColValue("RadiologyDocName"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }

       
    }
}
