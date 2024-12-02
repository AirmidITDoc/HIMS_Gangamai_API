using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPDDischargeSummary
    {
        public String Insert(IPDDischargeSummaryParams IPDDischargeSummaryParams);
        public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams);

        public String DischTemplateInsert(IPDDischargeSummaryParams IPDDischargeSummaryParams);
        public bool DischTemplateUpdate(IPDDischargeSummaryParams IPDDischargeSummaryParams);

        DataTable GetDataForReport(int AdmissionID);
        string ViewDischargeSummary(DataTable Bills, int AdmissionID, string htmlFilePath, string HeaderName);
        string ViewDischargeSummaryTemplate(DataTable Bills, int AdmissionID, string htmlFilePath, string HeaderName);


        
        string ViewDischargeSummaryold(int AdmissionID,string htmlFilePath, string HeaderName);
        

    }
}
