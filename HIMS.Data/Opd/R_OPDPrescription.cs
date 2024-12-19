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

            // delete previous data from prescription table
            var vVisitId = OPDPrescriptionParams.delete_OPPrescription.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_delete_OPPrescription_1", vVisitId);

            // add prescription table
            foreach (var a in OPDPrescriptionParams.InsertOPDPrescription)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_insert_OPPrescription_1", disc1);
            }
            // update follow 
            var disc5 = OPDPrescriptionParams.Update_VisitFollowupDate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_Update_VisitFollowupDate", disc5);

            // Add Test Request  
            foreach (var a in OPDPrescriptionParams.OPRequestList)
            {
                var vOPRequestList = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Insert_T_OPRequestList", vOPRequestList);
            }
            // Add Casepaper Master (Complaint,Diagnosis,Examination)
            foreach (var a in OPDPrescriptionParams.OPCasepaperDignosisMaster)
            {
                var vOPCasepaperMaster = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Insert_OPCasepaperDignosisMaster", vOPCasepaperMaster);
            }
            _unitofWork.SaveChanges();

            return true;
        }

        public DataTable GetDataForReport(int VisitId)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@VisitId", VisitId) { DbType = DbType.Int64 };
            return GetDataTableProc("m_rptOPDPrecriptionPrint", para);
        }

        public string ViewOPPrescriptionReceipt(DataTable Bills,  string htmlFilePath, string htmlHeader)
        {
           
            string html = File.ReadAllText(htmlFilePath);
            
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            int i = 0,j=0;
            string previousLabel = "";
            String Label = "",Label1 = "",Label2 = "";

            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++;

            //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\"><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

            //}





            foreach (DataRow dr in Bills.Rows)
            {
                i++;
                //if (i == 1 || Label != previousLabel)
                //{
                //    j = 1;
                //    Label = dr["DrugName"].ConvertToString();
                //    Label1 = dr["GenericName"].ConvertToString();
                //    Label2 = dr["OldClassName"].ConvertToString();

                //    items.Append("<tr style=\"font-size:22px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"2\" style=\";padding:6px;height:10px;text-align:left;font-weight:bold;vertical-align:middle;padding-left:60px;\">").Append(Label2).Append("-----").Append(Label).Append("</td><td  style=\"padding:6px;height:10px;text-align:left;vertical-align:middle\">").Append(Label1).Append("</td></tr>");
                //}
                //previousLabel = dr["DrugName"].ConvertToString();

                //if (Label == previousLabel)
                //{

                //    i++;
                    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:20px;\"><td style=\"vertical-align: top;padding: 6px;;height: 20px;text-align:left;font-size:20px;padding-left:7px;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                    items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["Instruction"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"vertical-align:middle;padding: 6px;height:10px;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

                    if (dr["ItemGenericName"].ConvertToString() != null)
                    {
                        items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;\">").Append("Composition :").Append(dr["ItemGenericName"].ConvertToString()).Append("</td></tr>");

                    }
                   
                        items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px;border-bottom: 1px solid #d4c3c3;padding-bottom:20px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;padding-bottom:20px;\">").Append("Timing :").Append(dr["DoseNameInEnglish"].ConvertToString()).Append(dr["DoseNameInMarathi"].ConvertToString()).Append("</td></tr>");


                //    j++;
                //}


            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{ReferDrName}}", Bills.GetColValue("ReferDrName"));
            
            html = html.Replace("{{OPDNo}}", Bills.GetColValue("OPDNo"));
            html = html.Replace("{{PDate}}", Bills.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
            html = html.Replace("{{VisitTime}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
            html = html.Replace("{{FollowupDate}}", Bills.GetColValue("FollowupDate").ConvertToDateString("dd/MM/yyyy"));

            
            html = html.Replace("{{PrecriptionId}}", Bills.GetColValue("PrecriptionId"));
            
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
            html = html.Replace("{{WardName}}", Bills.GetColValue("WardName"));
            html = html.Replace("{{PreparedBy}}", Bills.GetColValue("PreparedBy"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{Address}}", Bills.GetColValue("Address"));
            html = html.Replace("{{ChiefComplaint}}", Bills.GetColValue("ChiefComplaint"));
            html = html.Replace("{{Diagnosis}}", Bills.GetColValue("Diagnosis"));
            html = html.Replace("{{Examination}}", Bills.GetColValue("Examination"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{Pulse}}", Bills.GetColValue("Pulse"));
           html = html.Replace("{{Height}}", Bills.GetColValue("Height"));
            html = html.Replace("{{Weight}}", Bills.GetColValue("PWeight"));
            html = html.Replace("{{Temp}}", Bills.GetColValue("Temp"));
            html = html.Replace("{{BP}}", Bills.GetColValue("BP"));
            html = html.Replace("{{BSL}}", Bills.GetColValue("BSL"));
            html = html.Replace("{{BMI}}", Bills.GetColValue("BMI"));
            html = html.Replace("{{SpO2}}", Bills.GetColValue("SpO2"));

            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("DoctorName"));
            html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
            html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
            html = html.Replace("{{Advice}}", Bills.GetColValue("Advice"));

            html = html.Replace("{{chkBPflag}}", Bills.GetColValue("BP").ConvertToString() !="" ? "visible" : "none");
            html = html.Replace("{{chkPulseflag}}", Bills.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkHeightflag}}", Bills.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkWeightflag}}", Bills.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkTempflag}}", Bills.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBSLflag}}", Bills.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkBMIflag}}", Bills.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
            html = html.Replace("{{chkSpO2flag}}", Bills.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");

            html = html.Replace("{{chkEdu}}", Bills.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
            html = html.Replace("{{chkRegNo}}", Bills.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
           
            html = html.Replace("{{chkSignature}}", Bills.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");


            return html;
        }
    }
}
