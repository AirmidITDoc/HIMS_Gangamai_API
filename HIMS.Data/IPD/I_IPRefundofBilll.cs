﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPRefundofBilll
    {
        public String Insert(IPRefundofBilllparams IPRefundofBilllparams);

        string ViewIPRefundofBillReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath);

        
    }
}
