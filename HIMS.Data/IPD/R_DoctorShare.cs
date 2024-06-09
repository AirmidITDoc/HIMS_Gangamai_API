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
    public class R_DoctorShare : GenericRepository, I_DoctorShare
    {
        public R_DoctorShare(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public string Insert(Doctorshareparam Doctorshareparam)
        {
            // throw new NotImplementedException();

            var outputId1 = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@DocShareId",
                Value = 0,
                Direction = ParameterDirection.Output
            }; 

            var disc1 = Doctorshareparam.DoctorshareheaderInsert.ToDictionary();
            disc1.Remove("DocShareId");
            var Id = ExecNonQueryProcWithOutSaveChanges("insert_DoctorShareHeader_1", disc1,outputId1);

            var dic2 = Doctorshareparam.DoctorsharemasterUpdate.ToDictionary();
            dic2["DoctorId"] = Doctorshareparam.DoctorshareheaderInsert.DoctorId;
             var Id1 = ExecNonQueryProcWithOutSaveChanges("Insert_DoctorShareMaster_1", dic2);


            _unitofWork.SaveChanges();
            return Id;
        }

        public string ViewDeptdoctorshareReport(int DoctorId, DateTime FromDate, DateTime ToDate, string htmlFilePath, string htmlHeader)
        {
            // throw new NotImplementedException();
            SqlParameter[] para = new SqlParameter[3];
            para[0] = new SqlParameter("@DoctorId", DoctorId) { DbType = DbType.UInt64 };
            para[1] = new SqlParameter("@FromDate", FromDate) { DbType = DbType.DateTime };
            para[2] = new SqlParameter("@ToDate", ToDate) { DbType = DbType.DateTime };


            var Bills = GetDataTableProc("RptDoctorShareDetReport", para);
            string html = File.ReadAllText(htmlFilePath);// templates.Rows[0]["TempDesign"].ToString();


            html = html.Replace("{{NewHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            StringBuilder items = new StringBuilder("");
            int i = 0, j = 0;

            double TotalCollection = 0, Tot_ServiceAmt=0,G_ServiceAmount=0;
            string previousLabel = " ";

            foreach (DataRow dr in Bills.Rows)
            {
                i++; j++;
                if (i == 1)
                {
                    String Label;
                    Label = dr["GroupName"].ConvertToString();
                    items.Append("<tr style=\"font-size:20px;border: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"9\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                }

                if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                {
                    j = 1;


                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                     .Append(G_ServiceAmount.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");
                    G_ServiceAmount = 0;

                    items.Append("<tr style=\"font-size:20px;border-bottom: 1px;color:blue; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");
                }


                Tot_ServiceAmt += dr["Amount"].ConvertToDouble();

                previousLabel = dr["GroupName"].ConvertToString();



                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");
                //items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GroupName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ServiceName"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Percentage"].ConvertToString()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PerAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TDSPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TDSAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetPayableAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["StartDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["EndDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td></tr>");


                items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");


                if (Bills.Rows.Count > 0 && Bills.Rows.Count == i)
                {

                    items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                    .Append(Tot_ServiceAmt.To2DecimalPlace()).Append("</td><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"></td></tr>");

                }
            }



            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));





            return html;
        }
    }
}
