using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ItemClassMasterParams
    {
        public InsertItemClassMaster InsertItemClassMaster { get; set; }
        public UpdateItemClassMaster UpdateItemClassMaster { get; set; }
    }

    public class InsertItemClassMaster
    {
        public string ItemClassName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }

    public class UpdateItemClassMaster
    {
        public long ItemClassId { get; set; }
        public string ItemClassName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
