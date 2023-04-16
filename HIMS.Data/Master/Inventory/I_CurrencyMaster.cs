using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_CurrencyMaster
    {
        public bool Insert(CurrencyMasterParams currencyMasterParams);
        public bool Update(CurrencyMasterParams currencyMasterParams);
    }
}
