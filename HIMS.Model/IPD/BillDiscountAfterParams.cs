using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class BillDiscountAfterParams
    {
        public BillDiscountAfterUpdate BillDiscountAfterUpdate { get; set; }
    }

    public class BillDiscountAfterUpdate
    {
        public int BillNo { get; set; }
        public float NetPayableAmt { get; set; }
        public float ConcessionAmt { get; set; }
        public float CompDiscAmt { get; set; }
        public float BalanceAmt { get; set; }
        public int ConcessionReasonId { get; set; }

    }
}
