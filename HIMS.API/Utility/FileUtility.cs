using HIMS.Data.Pharmacy;
using HIMS.Model.IPD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace HIMS.API.Utility
{
    public class FileUtility : IFileUtility
    {
        public readonly I_Sales _Sales;
        public readonly IConfiguration _configuration;
        public FileUtility(I_Sales sales, IConfiguration configuration)
        {
            _Sales = sales;
            _configuration = configuration;
        }
        public async Task<string> UploadDocument(IFormFile objFile, string Folder, string FileName)
        {
            var DestinationPath = _Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists(DestinationPath.Trim('\\') + "\\" + Folder))
                Directory.CreateDirectory(DestinationPath.Trim('\\') + "\\" + Folder);
            string FilePath = Path.Combine(DestinationPath.Trim('\\'), Folder.Trim('\\'));
            string NewFileName = Path.Combine(FilePath, (string.IsNullOrWhiteSpace(FileName) ? Guid.NewGuid().ToString() : FileName) + System.IO.Path.GetExtension(objFile.FileName));
            if (File.Exists(NewFileName))
                NewFileName = Path.Combine(FilePath, FileName + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(objFile.FileName));
            using (Stream fileStream = new FileStream(NewFileName, FileMode.Create))
            {
                await objFile.CopyToAsync(fileStream);
            }
            return NewFileName;
        }

        public async Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath)
        {
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return new Tuple<MemoryStream, string, string>(memoryStream, GetMimeType(filePath), Path.GetFileName(filePath));
        }
        public async Task<string> GetBase64(string filePath)
        {
            if (!File.Exists(filePath))
                return "";
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            return Convert.ToBase64String(imageArray);
        }

        private string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


    }
}
