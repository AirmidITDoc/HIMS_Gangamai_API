using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Pathology;
using Microsoft.Extensions.Primitives;
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

        public string ViewPathTemplateReceipt(DataTable Bills, string htmlFilePath, string htmlHeader)
        {
           
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            
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
           
            html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
            html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
            html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
            html = html.Replace("{{SampleCollection}}", Bills.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }


        //public string ViewPathTemplateReceipt1(int PathReportId, int OP_IP_Type, string htmlFilePath, string htmlHeader)
        //{
        //    // throw new NotImplementedException();

        //    SqlParameter[] para = new SqlParameter[2];

        //    para[0] = new SqlParameter("@PathReportId", PathReportId) { DbType = DbType.Int64 };
        //    para[1] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
        //    var Bills = GetDataTableProc("m_rptPrintPathologyReportTemplate", para);
        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //        // html = html.Replace("{{KamalHeader}}", Bills.GetColValue("HeaderName"));
        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    StringBuilder items = new StringBuilder("");
        //    int i = 0;
        //    Boolean chkresonflag = false;

        //    html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
        //    html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
        //    html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
        //    html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));
        //    html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));

        //    html = html.Replace("{{IPDNo}}", Bills.GetColValue("OP_IP_Number"));
        //    html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
        //    html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
        //    html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
        //    html = html.Replace("{{DoctorName}}", Bills.GetColValue("ConsultantDocName"));
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{BedName}}", Bills.GetColValue("bedName"));
        //    html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
        //    html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
        //    html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
        //    html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));

        //    html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

        //    html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
        //    html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
        //    html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));

        //    html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName").ConvertToString());
        //    html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName").ConvertToString());
        //    html = html.Replace("{{ComapanyName}}", Bills.GetColValue("ComapanyName"));

        //    html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
        //    html = html.Replace("{{Path_RefDoctorName}}", Bills.GetColValue("Path_RefDoctorName"));
        //    html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
        //  //  html = html.Replace("{{Signature}}", Bills.GetColValue("Signature").ConvertToString());

        //    html = html.Replace("{{PrintTestName}}", Bills.GetColValue("PrintTestName"));
        //    html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
        //    html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
        //    html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
        //    html = html.Replace("{{SampleCollection}}", Bills.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

        //    html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
        //    //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
        //    return html;

        //}
        public string ViewPathologyReport(int AdmissionID, string htmlFilePath, string htmlHeader)
        {


            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("RptPathologyTestDetailForDischargeSummary", para);

            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0;

            string previousPatientType = "";
            string previousDoctorName = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

             
                string currentPatientType = dr["PathDate"].ConvertToDateString("dd/MM/yyyy");
                string currentDoctorName = dr["TestName"].ConvertToString();

                // If the PatientType or DoctorName changes, insert a new section
                if (i == 1 || previousPatientType != currentPatientType || previousDoctorName != currentDoctorName)
                {


                    // Add new group header with both PatientType and DoctorName
                    items.Append("<tr style=\"font-weight:bold;font-size:21px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                         .Append("<td  style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                         .Append(currentPatientType)
                        .Append("<td colspan='13' style=\"border:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(currentDoctorName)
                       
                         .Append("</td></tr>");
                }

                // Update previousPatientType and previousDoctorName
                previousPatientType = currentPatientType;
                previousDoctorName = currentDoctorName;
                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:18px;\"><td style=\"vertical-align: top;padding: 6px;;height: 20px;text-align:left;font-size:20px;padding-left:7px;\">").Append("</td>");

                //items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PathDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                //items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ParameterName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["UnitName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");

                items.Append("<td style=\"padding: 10px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:5px;\">").Append(dr["ParameterName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"padding: 10px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["UnitName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"padding: 10px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-right:5px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");


            }

            html = html.Replace("{{Items}}", items.ToString());
          
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            //   html = html.Replace("{{AdmId}}", Bills.GetColValue("AdmId"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{PathDate}}", Bills.GetColValue("PathDate").ConvertToDateString("dd/MM/yyyy"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString("hh:mm tt"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));

            return html;
        }


    }
}

   