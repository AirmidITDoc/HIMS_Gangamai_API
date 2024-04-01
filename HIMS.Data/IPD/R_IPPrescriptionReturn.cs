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
   public class R_IPPrescriptionReturn:GenericRepository,I_IPPrescriptionReturn
    {
        public R_IPPrescriptionReturn(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Insert(IPPrescriptionReturnParams IPPrescriptionReturnParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@PresReId",
                Value = 0,
                Direction = ParameterDirection.Output
            };
            

            var disc2 = IPPrescriptionReturnParams.IPPrescriptionReturnH.ToDictionary();
            disc2.Remove("PresReId");
            var PresReID = ExecNonQueryProcWithOutSaveChanges("Insert_T_IPPrescriptionReturnH_1", disc2, outputId);

            

            foreach (var a in IPPrescriptionReturnParams.IPPrescriptionReturnD)
            {
                //IPPrescriptionReturnParams.IPPrescriptionReturnD.PresReId = Convert.ToInt32(PresReID);
                var disc = a.ToDictionary();
                disc["PresReId"] = Convert.ToInt32(PresReID);
                ExecNonQueryProcWithOutSaveChanges("Insert_T_IPPrescriptionReturnD_1", disc);
            }
            //IPPrescriptionReturnParams.IPPrescriptionReturnD.PresReId=Convert.ToInt32( PresReID);
            //var disc1 = IPPrescriptionReturnParams.IPPrescriptionReturnD.ToDictionary();
           // disc1.Remove("PresReId");
            //ExecNonQueryProcWithOutSaveChanges("Insert_T_IPPrescriptionReturnD_1", disc1);

          
            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewIPPrescriptionReturnfromwardReceipt(DateTime FromDate, DateTime ToDate, int Reg_No, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];

            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@Reg_No", Reg_No) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            var Bills = GetDataTableProc("Rtrv_IPPrescriptionReturnListFromWard", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;vertical-align:middle;text-align: center;font-size:18px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("PDate").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));


            return html;
        }

        public string ViewIPPrescriptionReturnReceipt(int PresReId, string htmlFilePath, string htmlHeaderFilePath)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@PresReId", PresReId) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPPrescriptionReturnListPrint", para);
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";

            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;vertical-align:middle;text-align: center;font-size:18px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{PresTime}}", Bills.GetColValue("PresTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));


            return html;
        }
    }
}
