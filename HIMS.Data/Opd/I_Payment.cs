using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public interface I_Payment
    {
        bool Update(PaymentParams PaymentParams);
        bool Save(PaymentParams PaymentParams);
    }
}
