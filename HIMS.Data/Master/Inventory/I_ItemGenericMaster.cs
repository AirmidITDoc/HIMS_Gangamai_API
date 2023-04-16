using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ItemGenericMaster
    {
        public bool Insert(ItemGenericMasterParams itemGenericMasterParams);
        public bool Update(ItemGenericMasterParams itemGenericMasterParams);
    }
}
