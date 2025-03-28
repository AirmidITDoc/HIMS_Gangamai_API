﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPAdvance
    {

        public bool Cancel(AdvanceParamCancelPram AdvanceParamCancelPram);
        public String Insert(IPAdvanceParams IPAdvanceParams );
        string ViewAdvanceReceipt(int AdvanceNo, string htmlFilePath, string HeaderName);
        string ViewAdvanceSummaryReceipt(int AdmissionID, string htmlFilePath, string HeaderName);

    }
}
