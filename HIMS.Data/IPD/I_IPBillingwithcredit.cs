﻿using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
  public  interface I_IPBillingwithcredit
    {
        public String Insert(IPBillingwithcreditparams IPBillingwithcreditparams);
        public String IPBillingCreditCashCounter(IPBillingwithcreditparams IPBillingwithcreditparams);
    }
}
