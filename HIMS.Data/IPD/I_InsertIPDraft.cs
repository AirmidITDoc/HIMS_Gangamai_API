using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_InsertIPDraft
    {
        public String Insert(InsertIPDraftParams InsertIPDraftParams);

        string ViewIPDraftBillReceipt(int AdmissionID, string htmlFilePath, string HeaderName);
    }
}
