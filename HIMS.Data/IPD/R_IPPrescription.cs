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
    public class R_IPPrescription:GenericRepository,I_IPPrescription
    {
        public R_IPPrescription(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

        public bool Insert(IPPrescriptionParams IPPrescriptionParams)
        {
            //var OP_IP_ID = IPPrescriptionParams.DeleteIP_Prescription.ToDictionary();
            //ExecNonQueryProcWithOutSaveChanges("Delete_T_IP_Prescription", OP_IP_ID);

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@MedicalRecoredId",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = IPPrescriptionParams.InsertIP_MedicalRecord.ToDictionary();
            disc1.Remove("MedicalRecoredId");
            var MedicalRecoredId = ExecNonQueryProcWithOutSaveChanges("insert_T_IPMedicalRecord_1", disc1, outputId1);

            foreach (var a in IPPrescriptionParams.InsertIP_Prescription)
            {
                var disc2 = a.ToDictionary();
                disc2["IPMedID"] = MedicalRecoredId;
                ExecNonQueryProcWithOutSaveChanges("insert_IPPrescription_1", disc2);
            }

                      

            _unitofWork.SaveChanges();
            return true;
        }

        public string ViewIPPrescriptionDetailReceipt(int AdmissionID, string htmlFilePath, string htmlHeader)
        {
            //throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPPrescriptionDetails", para);
         
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

                   
                    j++;
                }


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            return html;
        }

        public string ViewIPPrescriptionReceipt(int OP_IP_ID, int PatientType,  string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[2];

                para[0] = new SqlParameter("@OP_IP_ID", OP_IP_ID) { DbType = DbType.Int64 };
                  para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
                  var Bills = GetDataTableProc("rptIPDPrecriptionPrint", para);
            
            string html = File.ReadAllText(htmlFilePath);
                //string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
                html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                html = html.Replace("{{NewHeader}}", htmlHeader);
                StringBuilder items = new StringBuilder("");
                int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;vertical-align:middle;text-align: center;font-size:18px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
                html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
                html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
                html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{IPMedID}}", Bills.GetColValue("IPMedID"));
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
                html = html.Replace("{{PDate}}", Bills.GetColValue("PDate").ConvertToDateString());

                html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
                html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));


            return html;

            }

        public string ViewIPPrescriptionSummHourlyReceipt(int OP_IP_ID, int PatientType, string htmlFilePath, string htmlHeaderFilePath)
        {
            //  throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            //para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPPrescriptionDetails", para);
            htmlHeaderFilePath = "F:\\AirmidHIMS\\HIMS_Gangamai_API\\HIMS.API\\wwwroot\\PdfTemplates\\HospitalHeader.html";


            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(htmlHeaderFilePath);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");


                    j++;
                }


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            return html;
        }

        public string ViewIPPrescriptionSummReceipt(int AdmissionID, string htmlFilePath, string HeaderName)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[1];

            para[0] = new SqlParameter("@AdmissionID", AdmissionID) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptIPPrescriptionDetails", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            string previousLabel = "";
            String Label = "";


            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                if (i == 1 || Label != previousLabel)
                {
                    j = 1;
                    Label = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                previousLabel = dr["RoundVisitTime"].ConvertToDateString("MM/dd/yyyy");

                if (Label == previousLabel)
                {

                    i++;
                    items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(j).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["RoundVisitDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["RoundVisitTime"].ConvertToDateString("hh:mm:tt")).Append("</td></tr>");


                    j++;
                }


            }

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("AdmissionTime").ConvertToDateString());

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));

            return html;
        }

        public string ViewOPPrescriptionReceipt(int VisitId, int PatientType, string htmlFilePath, string HeaderName)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[2];

            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@PatientType", PatientType) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptOPDPrecriptionPrint", para);
            string html = File.ReadAllText(htmlFilePath);
            string htmlHeader = File.ReadAllText(HeaderName);// templates.Rows[0]["TempDesign"].ToString();
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{HeaderName}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;

                items.Append("<tr><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;font-size:18px;text-align:center;\">").Append(i).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;vertical-align:middle;text-align: center;font-size:18px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

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
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));

            return html;
        }
    }
    }

