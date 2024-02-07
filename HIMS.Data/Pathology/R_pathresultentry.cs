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

        public string ViewPathTestReport(int OP_IP_Type, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            
            var Bills = GetDataTableProc("rptPathologyReportPrintMultiple", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
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
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["TestName"].ConvertToString();

                if (Label == previousLabel)
                {
                    i++;
                    items.Append("<tr><td style=\"border: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;font-size:18px;\">").Append(j).Append("</td>");
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

            //String v = Bills.GetColValue("PathTemplateDetailsResult").innerHTML;
            //{ { PathTemplateDetailsResult || innerHTML} }
            //{ { PathTemplateDetailsResult} }.innerHTML

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
            return html;
        }
    }
}
