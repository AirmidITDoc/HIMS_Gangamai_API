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
   public class R_IPLabrequestChange :GenericRepository,I_IPLabrequestChange
    {
        public R_IPLabrequestChange(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(IPLabrequestChangeParam IPLabrequestChangeParam)
        {
           // throw new NotImplementedException();
            var dic = IPLabrequestChangeParam.InsertIPRequestLabcharges.ToDictionary();
            dic.Remove("RequestId");
            ExecNonQueryProcWithOutSaveChanges("Insert_LabRequest_Charges_1", dic);

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewLabRequest(int RequestId, string htmlFilePath, string HeaderName)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@RequestId", RequestId) { DbType = DbType.Int64 };
            
            var Bills = GetDataTableProc("rptLabRequestList", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align:border-bottom:1px solid #000; top;padding: 0;height: 20px;font-size:18px;text-align:center;\">").Append(i).Append("</td>");
              items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AdmittedDocName}}", Bills.GetColValue("AdmittedDocName"));

            html = html.Replace("{{OPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{ReqDate}}", Bills.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));

            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            


            return html;
        }
    }
}
