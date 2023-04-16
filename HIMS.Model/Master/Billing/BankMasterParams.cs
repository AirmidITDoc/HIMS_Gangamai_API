using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
   public class BankMasterParams
    {
        public BankMasterInsert BankMasterInsert { get; set; }
        public BankMasterUpdate BankMasterUpdate { get; set; }

    }

    public class BankMasterInsert
    {
        public String BankName { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }

    }

    public class BankMasterUpdate
    {

        public int BankID { get; set; }
        public String BankName { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

