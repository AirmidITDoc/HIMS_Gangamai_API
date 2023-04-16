using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
  public interface I_CustomerInfo_PayAdv_Sw 
    {
        bool Update(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams);
        bool Save(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams);
    }


}
