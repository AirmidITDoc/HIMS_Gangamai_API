using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class CurrencyMasterParams
    {
        public InsertCurrencyMaster InsertCurrencyMaster { get; set; }
        public UpdateCurrencyMaster UpdateCurrencyMaster { get; set; }
    }

    public class InsertCurrencyMaster 
    {
         public string CurrencyName { get; set; }
        public Boolean IsDeleted { get; set;}
        public long AddedBy { get; set; }
        
    }

    public class UpdateCurrencyMaster
    {
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
