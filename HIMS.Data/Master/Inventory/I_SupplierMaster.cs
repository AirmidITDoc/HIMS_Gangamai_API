using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_SupplierMaster
    {
        public bool Insert(SupplierMasterParams supplierMasterParams);
        public bool Update(SupplierMasterParams supplierMasterParams);

    }
}
