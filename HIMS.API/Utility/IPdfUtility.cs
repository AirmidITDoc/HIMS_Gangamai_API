﻿using HIMS.Data.Pharmacy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Wkhtmltopdf.NetCore;

namespace HIMS.API.Utility
{
    public interface IPdfUtility
    {
        Tuple<byte[], string> GeneratePdfFromHtml(string html, string FolderName,string FileName="", Wkhtmltopdf.NetCore.Options.Orientation PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

        Tuple<byte[], string> CreateExel(string html, string FolderName, string FileName = "", Wkhtmltopdf.NetCore.Options.Orientation PageOrientation = Wkhtmltopdf.NetCore.Options.Orientation.Portrait);
        //bool CreateExel();
        string GetHeader(string filePath,long HospitalId=0);
        string GetStoreHeader(string filePath, long StoreId = 0);
        string GetHeader(int Id, int Type = 1);
        string GetTemplateHeader(int Id);
    }
}

