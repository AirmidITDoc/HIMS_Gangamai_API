using HIMS.Data.Pharmacy;
using HIMS.Model.IPD;
using Microsoft.AspNetCore.Http;
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
        public async Task<string> UploadDocument(IFormFile objFile, string Folder)
        {
            var DestinationPath = _Sales.GetFilePath();
            if (string.IsNullOrWhiteSpace(DestinationPath))
                DestinationPath = _configuration.GetValue<string>("StorageBasePath");
            if (!Directory.Exists(DestinationPath))
                Directory.CreateDirectory(DestinationPath);
            if (!Directory.Exists(DestinationPath.Trim('\\') + "\\" + Folder))
                Directory.CreateDirectory(DestinationPath.Trim('\\') + "\\" + Folder);
            string FilePath = DestinationPath.Trim('\\') + "\\" + Folder.Trim('\\') + "\\";
            string FileName = FilePath + Guid.NewGuid() + System.IO.Path.GetExtension(objFile.FileName);
            using FileStream fileStream = System.IO.File.Create(FileName);
            await objFile.CopyToAsync(fileStream);
            fileStream.Flush();
            return FileName;
        }

    }
}
