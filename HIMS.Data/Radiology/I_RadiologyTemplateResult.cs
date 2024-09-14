using HIMS.Model.IPD;
using HIMS.Model.Radiology;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HIMS.Data.Radiology
{
    public interface I_RadiologyTemplateResult
    {
        public bool Update(RadiologyTemplateResultParams RadiologyTemplateResultParams);
      //  string ViewRadiologyTemplateReceipt(int RadReportId, int OP_IP_Type, string htmlFilePath, string HeaderName);

        DataTable GetDataForReport(int RadReportId, int OP_IP_Type);
        string ViewRadiologyTemplateReceipt(DataTable Bills, string htmlFilePath, string HeaderName);

    }
}
