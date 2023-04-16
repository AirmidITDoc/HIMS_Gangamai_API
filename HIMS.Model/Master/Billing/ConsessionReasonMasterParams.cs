using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
   public class ConsessionReasonMasterParams
    {
        public ConsessionReasonMasterInsert ConsessionReasonMasterInsert { get; set; }
        public ConsessionReasonMasterUpdate ConsessionReasonMasterUpdate { get; set; }

    }

    public class ConsessionReasonMasterInsert
    {
        public String ConcessionReason { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }

    }

    public class ConsessionReasonMasterUpdate
    {

        public int ConcessionId { get; set; }
        public String ConcessionReason { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

