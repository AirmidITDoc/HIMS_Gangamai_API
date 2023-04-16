using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_StoreMaster
    {
        public bool Insert(StoreMasterParams storeMasterParams);
        public bool Update(StoreMasterParams storeMasterParams);
    }
}
