using HIMS.Model.Master;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public interface I_Customer
    {
        bool Update( CustomerParams CustomerParams);
        bool Save(CustomerParams CustomerParams);
    }
}
