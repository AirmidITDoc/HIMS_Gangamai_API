using HIMS.Common.Utility;
using HIMS.Model.Opd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Opd
{
    public class R_OPDPrescription :GenericRepository,I_OPDPrescription
    {
        public R_OPDPrescription(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }


        public bool Insert(OPDPrescriptionParams OPDPrescriptionParams)
        {
            foreach (var a in OPDPrescriptionParams.InsertOPDPrescription)
            {
                var disc1 = a.ToDictionary();
                //var dic1 = OPDPrescriptionParams.InsertOPDPrescription.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("insert_Prescription_1", disc1);

            }


            _unitofWork.SaveChanges();

            return true;
        }


        public string ViewOPPrescriptionReceipt(int VisitId, int PatientType, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPDPrecriptionPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\"><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;text-align:center;vertical-align:middle;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{VisitTime}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PrecriptionId}}", Bills.GetColValue("PrecriptionId"));
            
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));

            return html;
        }
    }
}
