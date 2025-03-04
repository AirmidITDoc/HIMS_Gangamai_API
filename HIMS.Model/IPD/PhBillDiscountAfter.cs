using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class PhBillDiscountAfter
    {
        public PhBillDiscountAfterUpdate PhBillDiscountAfterUpdate { get; set; }
    }

    public class PhBillDiscountAfterUpdate
    {

      
        public int SalesId { get; set; }
        public float NetPayableAmt { get; set; }
        public float DiscAmount { get; set; }
      
        public float BalanceAmt { get; set; }
      
        public int ConcessionReasonId { get; set; }
     

    }
}
