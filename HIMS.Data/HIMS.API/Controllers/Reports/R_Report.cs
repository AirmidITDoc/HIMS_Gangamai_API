using HIMS.API.Utility;
using HIMS.Data.Opd;
using HIMS.Data.CommanReports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HIMS.Data.PharmacyReports;
using HIMS.Common.Extensions;
using System.Drawing.Printing;
using Wkhtmltopdf.NetCore.Options;
using HIMS.Common.Utility;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace HIMS.Data.CommanReports
{
    public class R_Report : I_Report
    {
        public readonly IPdfUtility _pdfUtility;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
        public R_Report( Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment,
            IPdfUtility pdfUtility)
        {
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        //public string GetReportSetByProc(ReportRequestModel model)
        //{
        //    var tuple = new Tuple<byte[], string>(null, string.Empty);
        //    switch (model.Mode)
        //    {
        //        #region :: RegistrationReport ::
        //        case "RegistrationReport":
        //            {
        //                model.RepoertName = "Registration List";
        //                string[] headerList = { "Sr.No", "UHID", "Patient Name", "Address", "City", "Pin Code", "Age", "Gender Name", "Mobile No" };
        //                string[] colList = { "RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };
        //                string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
        //                string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
        //                var html = GetHTMLView("rptListofRegistration", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
        //                //tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationReport", "RegistrationReport", Orientation.Portrait, PaperKind.A4);
        //                tuple = _pdfUtility.GeneratePdfFromHtml(html, "DepartmentWisecountSummury", "DepartmentWisecountSummury", Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
        //                break;
        //            }
        //    }
        //    string byteFile = Convert.ToBase64String(tuple.Item1);
        //    return byteFile;
        //}
        //private string GetHTMLView(string sp_Name, ReportRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "")
        //{
        //    Dictionary<string, string> fields = HIMS.API.Extensions.SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
        //    DatabaseHelper odal = new();
        //    int sp_Para = 0;
        //    SqlParameter[] para = new SqlParameter[fields.Count];
        //    foreach (var property in fields)
        //    {
        //        var param = new SqlParameter
        //        {
        //            ParameterName = "@" + property.Key,
        //            Value = property.Value.ToString()
        //        };

        //        para[sp_Para] = param;
        //        sp_Para++;
        //    }
        //    var dt = odal.FetchDataTableBySP(sp_Name, para);

        //    string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath);

        //    string html = File.ReadAllText(htmlFilePath);
        //    html = html.Replace("{{HospitalHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{RepoertName}}", model.RepoertName);

        //    DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
        //    DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

        //    StringBuilder HeaderItems = new("");
        //    StringBuilder items = new("");
        //    StringBuilder ItemsTotal = new("");
        //    double T_Count = 0;
        //    switch (model.Mode)
        //    {
        //        // Simple Report Format
        //        case "RegistrationReport":
        //            {
        //                HeaderItems.Append("<tr>");
        //                foreach (var hr in headerList)
        //                {
        //                    HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
        //                    HeaderItems.Append(hr.ConvertToString());
        //                    HeaderItems.Append("</th>");
        //                }
        //                HeaderItems.Append("</tr>");

        //                int k = 0;
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    k++;

        //                    items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
        //                    foreach (var colName in colList)
        //                    {
        //                        items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
        //                    }
        //                }
        //            }
        //            break;
        //    }

        //    html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;

        //}

    }
}





