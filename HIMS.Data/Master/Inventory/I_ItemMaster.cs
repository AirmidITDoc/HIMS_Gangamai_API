using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ItemMaster
    {
        public bool Insert(ItemMasterParams itemMasterParams);
        public bool Update(ItemMasterParams itemMasterParams);

    }
}
