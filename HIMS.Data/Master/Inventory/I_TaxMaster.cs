using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_TaxMaster
    {
        public bool Insert(TaxMasterParams taxMasterParams);
        public bool Update(TaxMasterParams taxMasterParams);
    }
}
