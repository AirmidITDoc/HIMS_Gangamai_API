using HIMS.Data.Pharmacy;
using HIMS.Model.IPD;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace HIMS.API.Utility
{
    public interface IFileUtility
    {
        Task<string> UploadDocument(IFormFile objFile, string Folder);
        Task<Tuple<MemoryStream, string, string>> DownloadFile(string filePath);
    }
}

