using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_CompanyInformation
    {
        public bool Update(CompanyInformationparam CompanyInformationparam);
        string ViewCompanyInformationReceipt(int AdmissionId, string htmlFilePath, string HeaderName);

    }
}
