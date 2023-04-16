using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ItemCategoryMaster
    {
        public bool Save(ItemCategoryMasterParams itemCategoryMasterParams);
        public bool Update(ItemCategoryMasterParams itemCategoryMasterParams);
    }
}
