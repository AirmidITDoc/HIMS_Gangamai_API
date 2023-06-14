using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
   public class CashCounterMasterParams
    {
        public CashCounterMasterInsert CashCounterMasterInsert { get; set; }
        public CashCounterMasterUpdate CashCounterMasterUpdate { get; set; }

    }

    public class CashCounterMasterInsert
    {
       
        public String CashCounter { get; set; }
        public String Prefix { get; set; }

        public String BillNo { get; set; }
      
        public Boolean IsActive { get; set; }
    }

    public class CashCounterMasterUpdate
    {

        public int CashCounterId { get; set; }
        public String CashCounter { get; set; }
       
        public Boolean IsActive { get; set; }
        
    }
}

