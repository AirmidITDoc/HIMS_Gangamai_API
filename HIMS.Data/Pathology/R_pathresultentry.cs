using HIMS.Common.Utility;
using HIMS.Model.Opd;
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


            foreach (var a2 in pathresultentryparam.Deletepathreportheader)
            {
                var disc1 = a2.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Delete_T_PathologyReportDetails", disc1);
            }

            foreach (var a1 in pathresultentryparam.Insertpathreportdetail)
            {
               
                    var disc3 = a1.ToDictionary();
                    ExecNonQueryProcWithOutSaveChanges("m_insert_PathRrptDet_1", disc3);
                
            }
            foreach (var a3 in pathresultentryparam.Updatepathreportheader)
            {
                var disc3 = a3.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_update_T_PathologyReportHeader_1", disc3);
            }




            _unitofWork.SaveChanges();
            return true;
        }

        public bool PrintInsert(pathresultentryparam pathresultentryparam)
        {
            foreach (var a7 in pathresultentryparam.PrintInsert)
            {
                var vParam = a7.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_truncate_Temp_PathReportId", vParam);
            }
            foreach (var a6 in pathresultentryparam.PrintInsert)
            {
              var disc9 = a6.ToDictionary();
              ExecNonQueryProcWithOutSaveChanges("m_Insert_Temp_PathReportId", disc9);
            }
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Rollback(pathresultentryparam pathresultentryparam)
        {
           
                var Roll = pathresultentryparam.RollbackReport.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_RollBack_TestForResult", Roll);
            
            _unitofWork.SaveChanges();
            return true;
        }
        public DataTable GetDataForReport(int OP_IP_Type)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            return GetDataTableProc("m_rptPathologyReportPrintMultiple", para);
        }
        public string ViewPathTestMultipleReport(DataTable Bills, string htmlFilePath, string htmlHeader)
        {
            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{NewHeader}}", htmlHeader);
            StringBuilder items = new StringBuilder("");
            Boolean chkresonflag = false;
            string chkflag ="";
            int Suggflag = 0;
            int i = 0, j = 0,k=0,testlength=0,m;
            String Label = "", Suggchk="";
            string previousLabel = "",previoussubLabel = "";
            foreach (DataRow dr in Bills.Rows)
            {

                i++;

               

                if (i == 1)
                {
                    Label = dr["PrintTestName"].ConvertToString();
                    Suggchk = dr["PrintTestName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;padding-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:20px;vertical-align:middle;\">").Append(dr["CategoryName"].ConvertToString()).Append("</td></tr>");
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;padding-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:middle;\">").Append(Label).Append("</td></tr>");
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;padding-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;vertical-align:middle;\">").Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["PrintTestName"].ConvertToString())
                {
                    Suggchk = dr["PrintTestName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:middle\">").Append(dr["PrintTestName"].ConvertToString()).Append("</td></tr>");

                }

                //for (j = 0; j < Bills.Rows.Count; j++)
                //{
                //    if (Suggchk == previousLabel)
                //        testlength++;
                //}


                if (previousLabel != dr["PrintTestName"].ConvertToString() || previousLabel == "")
                    Suggflag = 1;
                else
                    Suggflag = 0;



                previousLabel = dr["PrintTestName"].ConvertToString();

                if (Suggflag == 1 || previousLabel != dr["PrintTestName"].ConvertToString())
                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;padding-left:10px;vertical-align:middle\">").Append("Interpretation Remark").Append(dr["SuggestionNote"].ConvertToString()).Append("</td></tr>");


                if (dr["ResultValue"].ConvertToString() != "")
                {
                    
                    k++;

                    if (k == 1 && dr["SubTestName"].ConvertToString() != "")
                    {
                        previoussubLabel = dr["SubTestName"].ConvertToString();
                     
                        items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                    }
                    if (previoussubLabel != "" && previoussubLabel != dr["SubTestName"].ConvertToString())
                    {
                        items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                    }



                    if (dr["IsBoldFlag"].ConvertToString() == "B")
                        items.Append("<tr style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding-bottom:5px;height: 20px;text-align:left;font-size:18px;font-weight:bold;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
                    else
                        items.Append("<tr  style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;padding-right:10px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");



                    if (dr["NormalRange"].ConvertToString() != " -   ") {
                        if (dr["ParaBoldFlag"].ConvertToString() == "B")
                            items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td>");
                        else
                            items.Append("<td style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                        items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");
                    }
                    else if(dr["NormalRange"].ConvertToString() == " -   ")
                    {
                        if (dr["ParaBoldFlag"].ConvertToString() == "B")
                            items.Append("<td colspan=\"2\"   style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td></tr>");
                        else
                            items.Append("<td colspan=\"2\" style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td></tr>");
                    }
                    
                    if (dr["MethodName"].ConvertToString() != "")
                    {
                        items.Append("<tr style=\"line-height: 15px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding-bottom:5px;height:15px;text-align:left;\">").Append(dr["MethodName"].ConvertToString()).Append("</td>");
                        items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:center;font-size:22px;font-weight:bold;\">").Append("</td>");
                        items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:center;font-size:22px;font-weight:bold;\">").Append("</td></tr>");
                    }


                    previoussubLabel = dr["SubTestName"].ConvertToString();

                    
                }

                //if (i== testlength)
                //    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;padding-left:10px;vertical-align:middle\">").Append("Interpretation Remark").Append(dr["SuggestionNote"].ConvertToString()).Append("</td></tr>");




            }


            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
            html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

            html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy "));
            html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy "));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            html = html.Replace("{{Adm_Visit_Time}}", Bills.GetColValue("Adm_Visit_Time"));
            //html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
            html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


            html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
            html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

            html = html.Replace("{{IPDNo}}", Bills.GetColValue("OP_IP_Number"));
            html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
            html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
            html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));


            html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
            html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
            html = html.Replace("{{DoctorName}}", Bills.GetColValue("ConsultantDocName"));
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
            html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
            html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
            html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
            html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
            html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
            html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
            html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));
         //   html = html.Replace("{{SuggestionNote}}", Bills.GetColValue("SuggestionNote"));
            html = html.Replace("{{BedName}}", Bills.GetColValue("BedName").ConvertToString());
            html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName").ConvertToString());
            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
            html = html.Replace("{{chkSuggestionNote}}", Bills.GetColValue("SuggestionNote").ConvertToString() != "" ? "table-row" : "none");

            //html = html.Replace("{{Signature}}", Bills.GetColValue("Signature"));


            
            return html;
        }



        //public string ViewPathTestMultipleReport(int OP_IP_Type, string htmlFilePath, string htmlHeader)
        //{
        //    //throw new NotImplementedException();

        //    SqlParameter[] para = new SqlParameter[1];

        //    para[0] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

        //    var Bills = GetDataTableProc("m_rptPathologyReportPrintMultiple", para);
        //    string html = File.ReadAllText(htmlFilePath);

        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{NewHeader}}", htmlHeader);
        //    StringBuilder items = new StringBuilder("");
        //    Boolean chkresonflag = false;
        //    string chkflag = "";

        //    int i = 0, j = 0, k = 0, l, m;
        //    String Label = "", Sugg = "";
        //    string previousLabel = "";

        //    foreach (DataRow dr in Bills.Rows)
        //    {
        //        i++;
        //        if (i == 1 || Label != previousLabel)
        //        {
        //            j = 1;
        //            Label = dr["PrintTestName"].ConvertToString();
        //            items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(Label).Append("</td></tr>");
        //        }
        //        previousLabel = dr["PrintTestName"].ConvertToString();

        //        if (Label == previousLabel)
        //        {
        //            i++;
        //            Label1 = dr["SubTestName"].ConvertToString();


        //            if (Label1 == "DIFFERENTIAL COUNT" && k == 0)
        //            {
        //                k = 1;
        //                items.Append("<tr style=\"font-size:18px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label1).Append("</td></tr>");

        //            }
        //            //if (Label1 == "CHEMICAL EXAMINATION" && l == 0)
        //            //{
        //            //    l = 1;
        //            //    items.Append("<tr style=\"font-size:18px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label1).Append("</td></tr>");

        //            //}

        //            //if (Label1 == "MICROSCOPIC EXAMINATION" && m == 0)
        //            //{
        //            //    m = 1;
        //            //    items.Append("<tr style=\"font-size:18px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label1).Append("</td></tr>");

        //            //}



        //            if (dr["ResultValue"].ConvertToString() != "")
        //            {
        //                if (dr["PrintParameterName"].ConvertToString() == "HAEMOGLOBIN" || dr["PrintParameterName"].ConvertToString() == "TOTAL WBC COUNT" || dr["PrintParameterName"].ConvertToString() == "DIFFERENTIAL COUNT")
        //                    items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding-bottom: 10px;height: 20px;text-align:left;font-size:18px;font-weight:bold;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
        //                else
        //                    items.Append("<tr  style=\"font-size: 14px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");

        //                items.Append("<td style=\"vertical-align: top;padding-bottom: 10px;height: 20px;text-align:center;font-size:18px;\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
        //                items.Append("<td style=\"vertical-align: top;padding-bottom: 10px;height: 20px;text-align:center;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");

        //            }

        //            j++;
        //        }
        //    }


        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
        //    html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
        //    html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
        //    html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));

        //    html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("ConsultantDocName"));
        //    html = html.Replace("{{PathTime}}", Bills.GetColValue("PathTime").ConvertToDateString());
        //    html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
        //    html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
        //    html = html.Replace("{{Adm_Visit_Time}}", Bills.GetColValue("Adm_Visit_Time"));
        //    //html = html.Replace("{{PathTemplateDetailsResult}}", Bills.GetColValue("PathTemplateDetailsResult").ConvertToString());
        //    html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));


        //    html = html.Replace("{{AdmissionTime}}", Bills.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //    html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

        //    html = html.Replace("{{IPDNo}}", Bills.GetColValue("IPDNo"));
        //    html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
        //    html = html.Replace("{{AdvanceAmount}}", Bills.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
        //    html = html.Replace("{{Phone}}", Bills.GetColValue("Phone"));


        //    html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
        //    html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
        //    html = html.Replace("{{DoctorName}}", Bills.GetColValue("DoctorName"));
        //    html = html.Replace("{{RoomName}}", Bills.GetColValue("RoomName"));
        //    html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
        //    html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
        //    html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
        //    html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
        //    html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));
        //    html = html.Replace("{{Path_DoctorName}}", Bills.GetColValue("Path_DoctorName"));
        //    html = html.Replace("{{Education}}", Bills.GetColValue("Education"));
        //    html = html.Replace("{{MahRegNo}}", Bills.GetColValue("MahRegNo"));



        //    //html = html.Replace("{{chkpaidflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

        //    html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
        //    //html = html.Replace("{{chkresonflag}}", Bills.GetColValue("reason").ConvertToString() != null ? "block" : "none");
        //    return html;
        //}

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
           //Boolean chkresonflag = false, chkSuggestionNote=false;


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


          
            html = html.Replace("{{chkSuggestionNote}}", Bills.GetColValue("SuggestionNote").ConvertToString() != "" ? "table-row" : "none");


            html = html.Replace("{{PathResultDr1}}", Bills.GetColValue("PathResultDr1"));
          
            return html;
        }
    }
}
