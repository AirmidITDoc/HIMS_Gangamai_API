using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ItemTypeMaster
    {
        public bool Save(ItemTypeMasterParams itemTypeMasterParams);
        public bool Update(ItemTypeMasterParams itemTypeMasterParams);
    }
}
