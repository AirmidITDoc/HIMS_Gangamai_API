using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public interface I_Sw_Bill_info
    {
        bool Save(Sw_Bill_infoParams Sw_Bill_infoParams);
       bool Update(Sw_Bill_infoParams Sw_Bill_infoParams);
    }
}
