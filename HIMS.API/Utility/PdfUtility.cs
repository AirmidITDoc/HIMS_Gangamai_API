using HIMS.Data.Pharmacy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Wkhtmltopdf.NetCore;

using Aspose.Cells;
using Wkhtmltopdf.NetCore.Options;
using HIMS.Data.Master;
using HIMS.Model.Master;

namespace HIMS.API.Utility
{
    public class PdfUtility : IPdfUtility
    {
        public readonly I_Sales _Sales;
        public readonly IGeneratePdf _generatePdf;
        public readonly IConfiguration _configuration;
        public readonly I_Hospital _Hospital;
        public PdfUtility(I_Sales sales, IGeneratePdf generatePdf, IConfiguration configuration, I_Hospital i_Hospital)
        {
            _Sales = sales;
            _generatePdf = generatePdf;
            _configuration = configuration;
            _Hospital = i_Hospital;
        }
        public string GetHeader(string filePath, long HospitalId=0)
        {
            string htmlHeader = System.IO.File.ReadAllText(filePath);
            HospitalMaster objHospital = _Hospital.GetHospitalById(1);
            htmlHeader = htmlHeader.Replace("{{HospitalName}}", objHospital?.HospitalName ?? "");
            htmlHeader = htmlHeader.Replace("{{Address}}", objHospital?.HospitalAddress ?? "");
            htmlHeader = htmlHeader.Replace("{{City}}", objHospital?.City ?? "");
            htmlHeader = htmlHeader.Replace("{{Pin}}", objHospital?.Pin ?? "");
            htmlHeader = htmlHeader.Replace("{{Phone}}", objHospital?.Phone ?? "");
            htmlHeader = htmlHeader.Replace("{{Display}}", (objHospital?.HospitalId ?? 0) > 0 ? "visible" : "hidden");
            return htmlHeader.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));
        }
        public Tuple<byte[], string> GeneratePdfFromHtml(string html, string FolderName, string FileName = "", Wkhtmltopdf.NetCore.Options.Orientation PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait)
        {
            var options = new ConvertOptions
            {
                EnableForms = true,
                PageOrientation = PageOrientation
            };

            _generatePdf.SetConvertOptions(options);
            var pdf = _generatePdf.GetPDF(html);
            var pdfStream = new System.IO.MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            Byte[] bytes = pdfStream.ToArray();
            string DestinationPath = _Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists(DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy")))
                Directory.CreateDirectory(DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy"));
            string NewFileName = DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy") + "\\" + (string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName) + ".pdf";
            if (File.Exists(NewFileName))
                NewFileName = DestinationPath.Trim('\\') + "\\" + FolderName + "\\" + DateTime.Now.ToString("ddMMyyyy") + "\\" + FileName + "_" + (Guid.NewGuid()) + ".pdf";
            System.IO.File.WriteAllBytes(NewFileName, bytes);
            return new Tuple<byte[], string>(bytes, NewFileName);
        }



        public Tuple<byte[], string> CreateExel(string html, string FolderName, string FileName = "", Orientation PageOrientation = Orientation.Portrait)
        {
            // throw new NotImplementedException();
            // Instantiate a Workbook object that represents Excel file.
            var pdfStream = new System.IO.MemoryStream();
            Byte[] bytes = pdfStream.ToArray();
            Workbook wb = new Workbook();

            // When you create a new workbook, a default "Sheet1" is added to the workbook.
            Worksheet sheet = wb.Worksheets[0];

            // Access the "A1" cell in the sheet.
            Cell cell = sheet.Cells["A1"];

            // Input the "Hello World!" text into the "A1" cell.
            cell.PutValue("Hello World!");

            // Save the Excel as .xlsx file.
            wb.Save("Excel.xlsx", SaveFormat.Xlsx);
            String st = "ok";

            return new Tuple<byte[], string>(bytes, st); ;
        }
    }
}

