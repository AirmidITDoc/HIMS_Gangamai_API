using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pathology
{
    public class R_PathologyTemplateResult:GenericRepository,I_PathologyTemplateResult
    {
        public R_PathologyTemplateResult(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(PathologyTemplateResultParams PathologyTemplateResultParams)
        {
            var disc3 = PathologyTemplateResultParams.DeletePathologyReportTemplateDetails.ToDictionary();
            var PathReportId = disc3["PathReportId"];
            ExecNonQueryProcWithOutSaveChanges("Delete_T_PathologyReportTemplateDetails", disc3);

            
            foreach (var a in PathologyTemplateResultParams.InsertPathologyReportTemplateDetails)
            {
                
                var disc1 = a.ToDictionary();
                disc1["PathReportId"] = (int)Convert.ToInt64(PathReportId);
                ExecNonQueryProcWithOutSaveChanges("insert_PathologyReportTemplateDetails_1", disc1);
            }

            //PathologyTemplateResultParams.UpdatePathologyReportHeader.PathReportID = PathologyTemplateResultParams.
            var disc = PathologyTemplateResultParams.UpdatePathTemplateReportHeader.ToDictionary();
            disc["PathReportID"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("update_T_PathologyReportHeader_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewPathTemplateReceipt(int PathReportId, int OP_IP_Type, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@PathReportId", PathReportId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptPrintPathologyReportTemplate", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{Path_RefDoctorName}}", Bills.GetColValue("Path_RefDoctorName"));
            html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));

            //String v = Bills.GetColValue("PathTemplateDetailsResult").innerHTML;
            //{ { PathTemplateDetailsResult || innerHTML} }
            //{ { PathTemplateDetailsResult} }.innerHTML

html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;

        }
    }
}

   