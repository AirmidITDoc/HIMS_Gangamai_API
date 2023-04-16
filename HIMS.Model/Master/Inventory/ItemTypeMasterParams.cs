using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ItemTypeMasterParams
    {
        public InsertItemTypeMaster InsertItemTypeMaster { get; set; }
        public UpdateItemTypeMaster UpdateItemTypeMaster { get; set; }
    }

    public class InsertItemTypeMaster
    {
        public string ItemTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }

    public class UpdateItemTypeMaster
    {
        public long ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
