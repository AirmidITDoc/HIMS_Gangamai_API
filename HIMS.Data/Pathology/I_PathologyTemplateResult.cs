using HIMS.Model.IPD;
using HIMS.Model.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pathology
{
    public interface I_PathologyTemplateResult
    {
        public bool Insert(PathologyTemplateResultParams PathologyTemplateResultParams);

        string ViewPathTemplateReceipt(int PathReportId,int OP_IP_Type, string htmlFilePath, string HeaderName);
    }
}
