using HIMS.Data.Pharmacy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Wkhtmltopdf.NetCore;

namespace HIMS.API.Utility
{
    public class PdfUtility : IPdfUtility
    {
        public readonly I_Sales _Sales;
        public readonly IGeneratePdf _generatePdf;
        public PdfUtility(I_Sales sales, IGeneratePdf generatePdf)
        {
            _Sales = sales;
            _generatePdf = generatePdf;
        }
        public Tuple<byte[], string> GeneratePdfFromHtml(string html)
        {
            var options = new ConvertOptions
            {
                EnableForms = true
            };

            _generatePdf.SetConvertOptions(options);
            var pdf = _generatePdf.GetPDF(html);
            var pdfStream = new System.IO.MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            Byte[] bytes = pdfStream.ToArray();
            string DestinationPath = _Sales.GetFilePath();
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists(DestinationPath.Trim('\\') + "\\PharmaBill\\" + DateTime.Now.ToString("ddMMyyyy")))
                Directory.CreateDirectory(DestinationPath.Trim('\\') + "\\PharmaBill\\" + DateTime.Now.ToString("ddMMyyyy"));
            string FileName = DestinationPath.Trim('\\') + "\\PharmaBill\\" + DateTime.Now.ToString("ddMMyyyy") + "\\" + Guid.NewGuid() + ".pdf";
            System.IO.File.WriteAllBytes(FileName, bytes);
            return new Tuple<byte[], string>(bytes, FileName);
        }

    }
}
