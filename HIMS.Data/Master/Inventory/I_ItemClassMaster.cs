using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ItemClassMaster
    {
        public bool Insert(ItemClassMasterParams itemClassMasterParams);
        public bool Update(ItemClassMasterParams itemClassMasterParams);
    }
}
