﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_InsertIPDraft
    {
        public String Insert(InsertIPDraftParams InsertIPDraftParams);

        string ViewIPDraftBillClassWise(int AdmissionID, string htmlFilePath, string HeaderName);
        string ViewIPDraftBillReceiptNew(int AdmissionID, string htmlFilePath, string HeaderName);
    }
}
