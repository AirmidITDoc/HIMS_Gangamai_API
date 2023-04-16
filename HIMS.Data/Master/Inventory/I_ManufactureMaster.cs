using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ManufactureMaster
    {
        public bool Insert(ManufactureMasterParams manufMasterParams);
        public bool Update(ManufactureMasterParams manufMasterParams);
    }
}
