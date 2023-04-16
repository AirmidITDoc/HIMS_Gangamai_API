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
        public String CashCounterName { get; set; }
        public String Prefix { get; set; }

        public String BillNo { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class CashCounterMasterUpdate
    {

        public int CashCounterID { get; set; }
        public String CashCounterName { get; set; }
        public String Prefix { get; set; }
        public String BillNo { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

