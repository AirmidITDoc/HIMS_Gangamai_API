using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
  public class IPAdvaceEditparam
    {
        public List<UpdateAdvancedet> UpdateAdvancedet { get; set; }

        public List<UpdatePayAmountAdvance> UpdatePayAmountAdvance { get; set; }
    }


    public class UpdateAdvancedet
    {
        public float AdvDetId { get; set; }
        public long AdvAmount { get; set; }
        public long AdvUsed { get; set; }

    }


    public class UpdatePayAmountAdvance
    {
        public float PaymentID { get; set; }
        public int CashPayAmount { get; set; }

    }
}
