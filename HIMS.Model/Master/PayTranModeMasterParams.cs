using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model
{
   public class PayTranModeMasterParams
    {
        public PayTranModeMasterInsert PayTranModeMasterInsert { get; set; }
        public PayTranModeMasterUpdate PayTranModeMasterUpdate { get; set; }
    }

    public class PayTranModeMasterInsert
    {

        public string PayTranModeName { get; set; }
        public bool IsDeleted { get; set; }

        public long AddedBy { get; set; }

    }
    public class PayTranModeMasterUpdate
    {
        public int PayTranId { get; set; }
        public string PayTranModeName { get; set; }
        public bool IsDeleted { get; set; }
       
        public long UpdatedBy { get; set; }


    }
}

