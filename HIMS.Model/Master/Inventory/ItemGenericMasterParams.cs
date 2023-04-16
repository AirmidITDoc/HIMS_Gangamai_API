using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ItemGenericMasterParams
    {
        public InsertItemGenericMaster InsertItemGenericMaster { get; set; }
        public UpdateItemGenericMaster UpdateItemGenericMaster { get; set; }
    }

    public class InsertItemGenericMaster
    {
        public string ItemGenericName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }

    public class UpdateItemGenericMaster
    {
        public long ItemGenericNameId { get; set; }
        public string ItemGenericName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
