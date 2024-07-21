using HIMS.Common.Utility;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Pathology
{
    public class R_pathresultentry :GenericRepository,I_pathresultentry
    {
        public R_pathresultentry(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(pathresultentryparam pathresultentryparam)
        {
            // throw new NotImplementedException();
            var disc1 = pathresultentryparam.Deletepathreportheader.ToDictionary();
            var PathReportId = disc1["PathReportID"];
            ExecNonQueryProcWithOutSaveChanges("Delete_T_PathologyReportDetails", disc1);
//for
            
          
            foreach (var a in pathresultentryparam.Insertpathreportdetail)
            {

                var disc2 = a.ToDictionary();
                disc2["PathReportId"] = (int)Convert.ToInt64(PathReportId);
                ExecNonQueryProcWithOutSaveChanges("insert_PathRrptDet_1", disc2);
            }


            var disc3 = pathresultentryparam.Updatepathreportheader.ToDictionary();
            disc3["PathReportID"] = (int)Convert.ToInt64(PathReportId);
            ExecNonQueryProcWithOutSaveChanges("update_T_PathologyReportHeader_1", disc3);
            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewPathTestMultipleReport(int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("rptPathologyReportPrintMultiple", para);
            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            Boolean chkresonflag = false;
            string chkflag ="";

            int i = 0, j = 0;
            String Label = "";
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["TestName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["TestName"].ConvertToString();

                if (Label == previousLabel)
                {
                    i++;


                    if (dr["ResultValue"].ConvertToString() != "")
                    {
                        if(dr["PrintParameterName"].ConvertToString() == "HAEMOGLOBIN" || dr["PrintParameterName"].ConvertToString() == "TOTAL WBC COUNT" || dr["PrintParameterName"].ConvertToString() == "DIFFERENTIAL COUNT")
                            items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;font-weight:bold;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
                        else
                        items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");

                        // items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                        items.Append("<td style=\"vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                        items.Append("<td style=\"vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");

                    }
                


                    //items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");

                    //// items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");
                  
                    j++;
                }
            }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            html = html.Replace("{{Adm_Visit_Time}}", Bills.GetColValue("Adm_Visit_Time"));
            //html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));


            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));



            //html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }

        public string ViewPathTestReport(int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            
            var Bills = GetDataTableProc("rptPathologyReportPrintMultiple", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
           Boolean chkresonflag = false;


            int i = 0,j=0;
            String Label ="";
            string previousLabel = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["TestName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["TestName"].ConvertToString();

                if (Label == previousLabel)
                {
                    i++;
                    items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(j).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["OP_IP_Number"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["PathTime"].ConvertToDateString("dd / MM / yyyy hh: mm tt")).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ReportTime"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(dr["Path_ConsultantDocname"].ConvertToString()).Append("</td></tr>");
                    //items.Append("<td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;\">").Append(dr["TotalAmount"].ConvertToString()).Append("</td></tr>");
                    j++;
                }
                }
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            html = html.Replace("{{Adm_Visit_Time}}", Bills.GetColValue("Adm_Visit_Time"));
            //html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));

            
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
            

          

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }
    }
}
