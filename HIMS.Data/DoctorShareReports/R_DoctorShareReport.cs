using HIMS.Common.Utility;using HIMS.Data.DoctorShareReports;using HIMS.Data.OPReports;using HIMS.Model.Opd;using System;using System.Collections.Generic;using System.Data;using System.Data.SqlClient;using System.IO;using System.Text;namespace HIMS.Data.Opd{    public class R_DoctorShareReport : GenericRepository, I_DoctorShareReport    {        private object finalVisitDate;        public R_DoctorShareReport(IUnitofWork unitofWork) : base(unitofWork)        {        }
        public string ViewDoctorShareReport(DateTime FromDate, DateTime ToDate, int DoctorId, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("Rtrv_DocShareAmt", para);


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
                    Label = dr["Lable"].ConvertToString();

                    Label = dr["AddChargeDrName"].ConvertToString();
                   
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }
                if (previousLabel != "" && previousLabel != dr["AddChargeDrName"].ConvertToString())
                {
                    j = 1;

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Amt </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                       .Append(NetAmount.ToString()).Append("</td></tr>");

                    NetAmount = 0;
                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["AddChargeDrName"].ConvertToString()).Append("</td></tr>");

                }

                NetAmount = NetAmount ;
                T_Count = T_Count;
                previousLabel = dr["AddChargeDrName"].ConvertToString();

                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["RegNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddChargeDrName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillNo"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BillDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CompanyName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td>");
               
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DocAmt"].ConvertToDouble()).Append("</td></tr>");

                NetAmount += dr["NetAmount"].ConvertToDouble();

                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Amt</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                         .Append(NetAmount.ToString()).Append("</td></tr>");


                }
                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                DocAmt += dr["DocAmt"].ConvertToDouble();
            }

            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString());
            html = html.Replace("{{DocAmt}}", DocAmt.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        public string ViewDoctorWiseSummaryReport(DateTime FromDate, DateTime ToDate, int DoctorId,string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();

            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[1] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.Int64 };
            var Bills = GetDataTableProc("Rtrv_DocShareAmt", para);



            string html = File.ReadAllText(htmlFilePath);

            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;
            double TotalAmt = 0, NetAmount = 0;

            foreach (DataRow dr in Bills.Rows)
            {
                i++;j++;

                items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["AddChargeDrName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td>");
               
                items.Append("<td style=\"text-align: right; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble()).Append("</td></tr>");
                TotalAmt += dr["TotalAmt"].ConvertToDouble();
                NetAmount += dr["NetAmount"].ConvertToDouble();
            }
            html = html.Replace("{{TotalAmt}}", TotalAmt.ToString());
            html = html.Replace("{{NetAmount}}", NetAmount.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
    }
}