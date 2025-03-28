﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPInterimBill
    {
        public String Insert(IPInterimBillParams IPInterimBillParams);
        public String InsertCashCounter(IPInterimBillParams IPInterimBillParams);
        string ViewIPInterimBillReceipt(int BillNo, string htmlFilePath,string HeaderName);

    }
}
