using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
  public  class GroupMasterParams
    {
        public GroupMasterInsert GroupMasterInsert { get; set; }
        public GroupMasterUpdate GroupMasterUpdate { get; set; }

    }

    public class GroupMasterInsert
    {
        public String GroupName { get; set; }
        public Boolean Isconsolidated { get; set; }
        public Boolean IsConsolidatedDR { get; set; }
        public int PrintSeqNo { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }

    }

    public class GroupMasterUpdate
    {
        public int GroupID { get; set; }
        public String GroupName { get; set; }
        public Boolean Isconsolidated { get; set; }
        public Boolean IsConsolidatedDR { get; set; }
        public int PrintSeqNo { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}
