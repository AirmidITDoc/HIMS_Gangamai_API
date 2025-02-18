using HIMS.Common.Utility;using HIMS.Data.DoctorShareReports;using HIMS.Data.OPReports;using HIMS.Model.Opd;using System;using System.Collections.Generic;using System.Data;using System.Data.SqlClient;using System.IO;using System.Text;using System.Text.RegularExpressions;namespace HIMS.Data.Opd{    public class R_DoctorShareReport : GenericRepository, I_DoctorShareReport    {        private object finalVisitDate;        public R_DoctorShareReport(IUnitofWork unitofWork) : base(unitofWork)        {        }
        public string ViewDoctorShareReport(int Doctor_Id, int GroupId, DateTime From_Dt, DateTime To_Dt, int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            //SqlParameter[] para = new SqlParameter[5];
            //para[0] = new SqlParameter("@Doctor_Id", Doctor_Id) { DbType = DbType.Int64 };
            //para[1] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };
            //para[2] = new SqlParameter("@From_Dt", From_Dt) { DbType = DbType.DateTime };
            //para[3] = new SqlParameter("@To_Dt", To_Dt) { DbType = DbType.DateTime };
            //para[4] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            //var Bills = GetDataTableProc("Retrieve_DoctorShareList", para);


            //string html = File.ReadAllText(htmlFilePath);

            //html = html.Replace("{{NewHeader}}", htmlHeader);
            //html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            //StringBuilder items = new StringBuilder("");
            //int i = 0, j = 0;
            //double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0;

            //string previousLabel = "";



            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++; j++;


            //    if (i == 1)
            //    {
            //        String Label;
            //        Label = dr["PatientType"].ConvertToString();
                   

            //        Label = dr["DoctorName"].ConvertToString();
                   
            //        items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
            //    }
            //    if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
            //    {
            //        j = 1;

            //        items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
            //           .Append(NetAmount.ToString()).Append("</td></tr>");

            //        NetAmount = 0;
            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");

            //    }

            //    NetAmount = NetAmount ;
            //    T_Count = T_Count;
            //    previousLabel = dr["DoctorName"].ConvertToString();

            //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
            //    //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
               
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalBillAmount"].ConvertToDouble()).Append("</td></tr>");

            //    NetAmount += dr["NetAmount"].ConvertToDouble();

            //    if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
            //    {

            //        items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

            //             .Append(NetAmount.ToString()).Append("</td></tr>");


            //    }
            //    T_NetAmount += dr["NetAmount"].ConvertToDouble();
            //    DocAmt += dr["TotalBillAmount"].ConvertToDouble();
            //}

            //html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            //html = html.Replace("{{DocAmt}}", DocAmt.ToString());
            //html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", From_Dt.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", To_Dt.ToString("dd/MM/yy"));
            //return html;


            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@Doctor_Id", Doctor_Id) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@From_Dt", From_Dt) { DbType = DbType.DateTime };
            para[3] = new SqlParameter("@To_Dt", To_Dt) { DbType = DbType.DateTime };
            para[4] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_Rtrv_DoctorShareDetail", para);

            string html = File.ReadAllText(htmlFilePath);

            // Replacing placeholders in the HTML template
            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0, T_DocAmt = 0 , HospitalAmt = 0, T_HospitalAmt = 0;

            string previousPatientType = "";
            string previousDoctorName = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                string currentPatientType = dr["PatientType"].ConvertToString();
                string currentDoctorName = dr["DoctorName"].ConvertToString();

                // If the PatientType or DoctorName changes, insert a new section
                if (i == 1 || previousPatientType != currentPatientType || previousDoctorName != currentDoctorName)
                {
                    // If there's an existing group, close it and insert total
                    if (previousPatientType != "" && previousDoctorName != "")
                    {
                        items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(NetAmount.ToString("0.00")).Append("</td>");
                        items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                            .Append(DocAmt.ToString("0.00")).Append("</td>");
                        items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                            .Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
                    }

                    // Reset the net amount for the new group
                    NetAmount = 0;

                    // Add new group header with both PatientType and DoctorName
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                         .Append("<td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                         .Append("Patient Type: ").Append(currentPatientType).Append(" | Doctor: ").Append(currentDoctorName)
                         .Append("</td></tr>");
                }

                // Update previousPatientType and previousDoctorName
                previousPatientType = currentPatientType;
                previousDoctorName = currentDoctorName;

                // Append the row data for the current bill
                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                     .Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().ToString("0.00")).Append("</td>")
                      .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocAmt"].ConvertToDouble().ToString("0.00")).Append("</td>")
                      .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["HospitalAmt"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");
                     //.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalBillAmount"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");

                // Update NetAmount for the current group
                NetAmount += dr["NetAmount"].ConvertToDouble();
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                T_DocAmt += dr["DocAmt"].ConvertToDouble();
                T_HospitalAmt += dr["HospitalAmt"].ConvertToDouble();
                      DocAmt += dr["DocAmt"].ConvertToDouble();
                HospitalAmt += dr["HospitalAmt"].ConvertToDouble();




                // If it's the last row, add the total for this group
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(NetAmount.ToString("0.00")).Append("</td>");
                    items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(DocAmt.ToString("0.00")).Append("</td>");
                    items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
                }

              
               
            }

            // Replace the placeholders with actual totals and the generated items
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString("0.00"));
            html = html.Replace("{{T_DocAmt}}", T_DocAmt.ToString("0.00"));
            html = html.Replace("{{T_HospitalAmt}}", T_HospitalAmt.ToString("0.00"));
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", From_Dt.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", To_Dt.ToString("dd/MM/yy"));

            return html;
        

    }
        public string ViewDoctorWiseSummaryReport(int Doctor_Id, int GroupId, DateTime From_Dt, DateTime To_Dt, int OP_IP_Type, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@Doctor_Id", Doctor_Id) { DbType = DbType.Int64 };
            para[1] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };
            para[2] = new SqlParameter("@From_Dt", From_Dt) { DbType = DbType.DateTime };
            para[3] = new SqlParameter("@To_Dt", To_Dt) { DbType = DbType.DateTime };
            para[4] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };

            var Bills = GetDataTableProc("m_Rtrv_DoctorShareSummary", para);



            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            //StringBuilder items = new StringBuilder("");
            //int i = 0, j = 0;
            //double TotalAmt = 0, NetAmount = 0;

            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++;j++;

            //    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmittedDocName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocAmt"].ConvertToDouble()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["HospitalAmt"].ConvertToDouble()).Append("</td></tr>");
            //    //items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");
            //    //TotalAmt += dr["TotalAmt"].ConvertToDouble();
            //    NetAmount += dr["NetPayableAmt"].ConvertToDouble();
            //}
            //html = html.Replace("{{TotalAmt}}", TotalAmt.ToString());
            //html = html.Replace("{{NetAmount}}", NetAmount.ToString());
            //html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //return html;
            //StringBuilder items = new StringBuilder("");
            //int i = 0, j = 0;
            //double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0, HospitalAmt =0;

            //string previousPatientType = "";
            //string previousDoctorName = "";
            //string previousGroupName = "";

            //foreach (DataRow dr in Bills.Rows)
            //{
            //    i++; j++;

            //    string currentPatientType = dr["PatientType"].ConvertToString();
            //    string currentDoctorName = dr["DoctorName"].ConvertToString();
            //    string currentGroupName = dr["GroupName"].ConvertToString();

            //    // If the PatientType or DoctorName changes, insert a new section
            //    if (i == 1 || previousPatientType != currentPatientType || previousDoctorName != currentDoctorName || previousGroupName != currentGroupName)
            //    {
            //        // If there's an existing group, close it and insert total
            //        if (previousPatientType != "" && previousDoctorName != "" && previousGroupName != "")
            //        {
            //            items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Doctor Wise Net Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
            //                 .Append(NetAmount.ToString("0.00")).Append(DocAmt.ToString("0.00")).Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
            //        }

            //        // Reset the net amount for the new group
            //        NetAmount = 0;

            //        // Add new group header with both PatientType and DoctorName
            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
            //             .Append("<td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
            //             .Append("Patient Type: ").Append(currentPatientType).Append(currentDoctorName).Append(currentGroupName)
            //             .Append("</td></tr>");
            //    }

            //    // Update previousPatientType and previousDoctorName
            //    previousPatientType = currentPatientType;
            //    previousDoctorName = currentDoctorName;
            //    previousGroupName = currentGroupName;

            //    // Append the row data for the current bill
            //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
            //         .Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>")
            //            .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"></td>")
            //           .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"></td>")


            //         .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().ToString("0.00")).Append("</td>")
            //         .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocAmt"].ConvertToDouble().ToString("0.00")).Append("</td>")
            //         .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["HospitalAmt"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");

            //    // Update NetAmount for the current group
            //    NetAmount += dr["NetPayableAmt"].ConvertToDouble();

            //    // If it's the last row, add the total for this group
            //    if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
            //    {
            //        items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
            //             .Append(NetAmount.ToString("0.00")).Append(DocAmt.ToString("0.00")).Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
            //    }

            //    // Update the total amounts for all doctors and patients
            //    T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();
            //    DocAmt += dr["DocAmt"].ConvertToDouble();
            //    HospitalAmt += dr["HospitalAmt"].ConvertToDouble();
            //}

            //// Replace the placeholders with actual totals and the generated items
            //html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString("0.00"));
            //html = html.Replace("{{DocAmt}}", DocAmt.ToString("0.00"));
            //html = html.Replace("{{HospitalAmt}}", HospitalAmt.ToString("0.00"));
            //html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", From_Dt.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", To_Dt.ToString("dd/MM/yy"));
            //return html;

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0, HospitalAmt = 0, T_DocAmt = 0, T_HospitalAmt = 0;

            string previousPatientType = "";
            //string previousTariffName = "";
            string previousDoctorName = "";
            string previousGroupName = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                string currentPatientType = dr["PatientType"].ConvertToString();
                //string currentTariffName = dr["TariffName"].ConvertToString();
                string currentDoctorName = dr["DoctorName"].ConvertToString();
                string currentGroupName = dr["GroupName"].ConvertToString();

                // If the PatientType or DoctorName changes, insert a new section
                if (i == 1 || previousPatientType != currentPatientType ||previousDoctorName != currentDoctorName || previousGroupName != currentGroupName)
                {
                    // If there's an existing group, close it and insert total
                    if (previousPatientType != "" &&  previousDoctorName != "" && previousGroupName != "")
                    {
                        //items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\">Doctor Wise Net Amount</td><td style=\"border-right:1px solid #000;font-size:20px;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                        //     .Append(NetAmount.ToString("0.00")).Append(DocAmt.ToString("0.00")).Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
                        items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(NetAmount.ToString("0.00")).Append("</td>");
                        items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                            .Append(DocAmt.ToString("0.00")).Append("</td>");
                        items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                            .Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");

                    }

                    // Reset the net amount for the new group
                    NetAmount = 0;

                    // Add new group header with both PatientType and DoctorName on separate lines
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                         .Append("<td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                         .Append("Patient Type: ").Append(currentPatientType)
                         .Append("</td></tr>")
                         .Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                         .Append("<td colspan=\"6\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                         .Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ").Append(currentDoctorName).Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ").Append(currentGroupName)
                         .Append("</td></tr>");
                        


                    // Add space between group header and data rows
                    items.Append("<tr><td colspan='6' style='border: 1px solid #d4c3c3; height: 10px;'></td></tr>"); // Empty row for spacing
                }

                // Update previousPatientType and previousDoctorName
                previousPatientType = currentPatientType;
                //previousTariffName = currentTariffName;
                previousDoctorName = currentDoctorName;
                previousGroupName = currentGroupName;

                

               items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                    .Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>")
                    .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"></td>")
                    .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"></td>")
                    .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().ToString("0.00")).Append("</td>")
                    .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocAmt"].ConvertToDouble().ToString("0.00")).Append("</td>")
                    .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["HospitalAmt"].ConvertToDouble().ToString("0.00")).Append("</td></tr>");

                // Update NetAmount for the current group
                NetAmount += dr["NetAmount"].ConvertToDouble();
                DocAmt += dr["DocAmt"].ConvertToDouble();
                HospitalAmt += dr["HospitalAmt"].ConvertToDouble();

                // If it's the last row, add the total for this group
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                         .Append(NetAmount.ToString("0.00")).Append("</td>");
                    items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(DocAmt.ToString("0.00")).Append("</td>");
                    items.Append("<td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;font-size:20px;vertical-align:middle\">")
                        .Append(HospitalAmt.ToString("0.00")).Append("</td></tr>");
                }

                // Update the total amounts for all doctors and patients
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                T_DocAmt += dr["DocAmt"].ConvertToDouble();
                T_HospitalAmt += dr["HospitalAmt"].ConvertToDouble();
            }

            // Replace the placeholders with actual totals and the generated items
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString("0.00"));
            html = html.Replace("{{T_DocAmt}}", T_DocAmt.ToString("0.00"));
            html = html.Replace("{{T_HospitalAmt}}", T_HospitalAmt.ToString("0.00"));
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", From_Dt.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", To_Dt.ToString("dd/MM/yy"));

            return html;

        }

        public string ViewConDoctorShareDetails(DateTime FromDate, DateTime ToDate, int DoctorId, int OPD_IPD_Type, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[4];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
           
            para[3] = new SqlParameter("@OPD_IPD_Type", OPD_IPD_Type) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("IP_DocShare_Cal_Sparsh", para);



            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, NetPayableAmt = 0, T_NetAmount = 0, DocAmt = 0;

            //string previousLabel = "";



            //foreach (DataRow dr in Bills.Rows)
            //{

            //    i++; j++;


            //    if (i == 1)
            //    {
            //        String Label;
            //        Label = dr["Lable1"].ConvertToString();

            //        Label = dr["AdmittedDocName"].ConvertToString();

            //        items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
            //    }
            //    if (previousLabel != "" && previousLabel != dr["AdmittedDocName"].ConvertToString())
            //    {
            //        j = 1;

            //        items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
            //           .Append(NetPayableAmt.ToString()).Append("</td></tr>");

            //        NetPayableAmt = 0;
            //        items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AdmittedDocName"].ConvertToString()).Append("</td></tr>");

            //    }

            //    NetPayableAmt = 0;
            //    T_Count = T_Count;
            //    previousLabel = dr["AdmittedDocName"].ConvertToString();

            //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
            //    //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
            //    items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td>");

            //    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");



            //    if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
            //    {

            //        items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

            //             .Append(NetPayableAmt.ToString()).Append("</td></tr>");


            //    }
            //    T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();
                
            //}

            //html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            //html = html.Replace("{{DocAmt}}", DocAmt.ToString());
            //html = html.Replace("{{Items}}", items.ToString());
            //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            //return html;




            string previousPatientType = "";
            string previousDoctorName = "";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;

                string currentPatientType = dr["Lable1"].ConvertToString();
                string currentDoctorName = dr["AdmittedDocName"].ConvertToString();

                // If the PatientType or DoctorName changes, insert a new section
                if (i == 1 || previousPatientType != currentPatientType || previousDoctorName != currentDoctorName)
                {
                    // If there's an existing group, close it and insert total
                    if (previousPatientType != "" && previousDoctorName != "")
                    {
                        items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                             .Append(NetPayableAmt.ToString("0.00")).Append("</td></tr>");
                    }


                    // Reset the net amount for the new group
                    NetPayableAmt = 0;

                    // Add new group header with both PatientType and DoctorName
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                         .Append("<td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">")
                         .Append("Patient Type: ").Append(currentPatientType).Append(" | Doctor: ").Append(currentDoctorName)
                         .Append("</td></tr>");
                }

                // Update previousPatientType and previousDoctorName
                previousPatientType = currentPatientType;
                previousDoctorName = currentDoctorName;

                // Append the row data for the current bill
                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                     .Append("<td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>")
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AdmissionDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>")
                      .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DischargeDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>")
                       
                     .Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>")
                            
                     .Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetPayableAmt"].ConvertToDouble().ToString("0.00")).Append("</td>")
                     .Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");

                // Update NetAmount for the current group
                NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

                // If it's the last row, add the total for this group
                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {
                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                         .Append(NetPayableAmt.ToString("0.00")).Append("</td></tr>");
                }

                // Update the total amounts for all doctors and patients
                T_NetAmount += dr["NetPayableAmt"].ConvertToDouble();
               
            }

            // Replace the placeholders with actual totals and the generated items
            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString("0.00"));
            html = html.Replace("{{DocAmt}}", DocAmt.ToString("0.00"));
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

            return html;


        }
        public string ViewDoctorShareListWithCharges(DateTime FromDate, DateTime Todate, int Doctor_Id, int GroupId,int OP_IP_Type ,string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@Todate", Todate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@Doctor_Id", Doctor_Id) { DbType = DbType.Int64 };
            para[3] = new SqlParameter("@GroupId", GroupId) { DbType = DbType.Int64 };
            para[4] = new SqlParameter("@OP_IP_Type", OP_IP_Type) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("rptAddChargeDoctorWiselist", para);


            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double T_Count = 0, NetAmount = 0, T_NetAmount = 0, DocAmt = 0;

            string previousLabel = "";



            foreach (DataRow dr in Bills.Rows)

            {

                i++; j++;


                if (i == 1)
                {
                    String Label;
                    Label = dr["label"].ConvertToString();

                    Label = dr["AddChargesDoctorName"].ConvertToString();

                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AddChargesDoctorName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetAmount.ToString()).Append("</td></tr>");

                    NetAmount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AddChargesDoctorName"].ConvertToString()).Append("</td></tr>");

                }

                NetAmount = NetAmount;
                T_Count = T_Count;
                previousLabel = dr["AddChargesDoctorName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddChargesDoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PBillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                //items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");

                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");

                NetAmount += dr["NetAmount"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='8' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetAmount.ToString()).Append("</td></tr>");


                }
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                DocAmt += dr["TotalAmt"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{DocAmt}}", DocAmt.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{Todate}}", Todate.ToString("dd/MM/yy"));
            return html;

        }
    }
}