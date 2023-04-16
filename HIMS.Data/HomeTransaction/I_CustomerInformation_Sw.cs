using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public interface I_CustomerInformation_Sw
    {
        bool Update(CustomerInformation_SwParams CustomerInformation_SwParams);
        bool Save(CustomerInformation_SwParams CustomerInformation_SwParams);
    }
}
