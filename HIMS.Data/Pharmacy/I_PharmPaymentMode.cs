using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
  public  interface I_PharmPaymentMode
    {
        public bool UpdatePaymentMode(PharmPaymentMode PharmPaymentMode);
    }
}
