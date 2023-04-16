using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
    public class SubGroupMasterParams
    {
        public SubGroupMasterInsert SubGroupMasterInsert { get; set; }
        public SubGroupMasterUpdate SubGroupMasterUpdate { get; set; }

    }

    public class SubGroupMasterInsert
    {
        public int GroupId { get; set; }
        public String SubGroupName { get; set; }
    
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class SubGroupMasterUpdate
    {

        public int SubGroupID { get; set; }
        public int GroupId { get; set; }
        public String SubGroupName { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

