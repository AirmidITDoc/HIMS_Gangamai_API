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

        public DataTable GetDataForReport(int PathReportId, int OP_IP_Type)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@PathReportId", PathReportId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            return GetDataTableProc("m_rptPrintPathologyReportTemplate", para);
        }

        public bool Insert(PathologyTemplateResultParams PathologyTemplateResultParams)
        {
            var disc3 = PathologyTemplateResultParams.DeletePathologyReportTemplateDetails.ToDictionary();
            var PathReportId = disc3["PathReportId"];
            ExecNonQueryProcWithOutSaveChanges("m_Delete_T_PathologyReportTemplateDetails", disc3);

                
            var disc1 = PathologyTemplateResultParams.InsertPathologyReportTemplateDetails.ToDictionary();
            disc1["PathReportId"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("m_insert_PathologyReportTemplateDetails_1", disc1);

            //PathologyTemplateResultParams.UpdatePathologyReportHeader.PathReportID = PathologyTemplateResultParams.
            var disc = PathologyTemplateResultParams.UpdatePathTemplateReportHeader.ToDictionary();
            disc["PathReportID"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("m_update_T_PathologyReportHeader_1", disc);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewPathTemplateReceipt(int PathReportId, int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@PathReportId", PathReportId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("m_rptPrintPathologyReportTemplate", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                // html = html.Replace("{{KamalHeader}}", Bills.GetColValue("HeaderName"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("OP_IP_Number"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("bedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName").ConvertToString());
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName").ConvertToString());
            html = html.Replace("{{ComapanyName}}", Bills.GetColValue("ComapanyName"));

            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{Path_RefDoctorName}}", Bills.GetColValue("Path_RefDoctorName"));
            html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{Signature}}", Bills.GetColValue("Signature").ConvertToString());

            html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
            html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
            html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
            html = html.Replace("{{SampleCollection}}", Bills.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;

        }

        public string ViewPathTemplateReceipt(DataTable Bills, string htmlFilePath, string htmlHeader)
        {
            SqlParameter[] para = new SqlParameter[2];

            //para[0] = new SqlParameter("@PathReportId", PathReportId) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            //var Bills = GetDataTableProc("m_rptPrintPathologyReportTemplate", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            // html = html.Replace("{{KamalHeader}}", Bills.GetColValue("HeaderName"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;
            Boolean chkresonflag = false;

            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("OP_IP_Number"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("bedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));

            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName").ConvertToString());
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName").ConvertToString());
            html = html.Replace("{{ComapanyName}}", Bills.GetColValue("ComapanyName"));

            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{Path_RefDoctorName}}", Bills.GetColValue("Path_RefDoctorName"));
            html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{Signature}}", Bills.GetColValue("Signature").ConvertToString());

            html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
            html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
            html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
            html = html.Replace("{{SampleCollection}}", Bills.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }
    }
}

   