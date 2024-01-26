using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPLabrequestChange
    {
        public bool Insert(IPLabrequestChangeParam IPLabrequestChangeParam);
        string ViewLabRequest(int RequestId,string htmlFilePath, string HeaderName);
    }
}
