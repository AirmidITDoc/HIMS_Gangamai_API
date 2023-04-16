using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class CustomerInfo_PayAdv_SwParams
    {
        public CustomerInfo_PayAdv_SwInsert CustomerInfo_PayAdv_SwInsert { get; set; }
        public CustomerInfo_PayAdv_SwUpdate CustomerInfo_PayAdv_SwUpdate { get; set; }
    }

    public class CustomerInfo_PayAdv_SwInsert
    {
        public DateTime PayDate { get; set; }
        public DateTime PayTime { get; set; }
        public int CustomerID { get; set; }
        public int OrderAmount { get; set; }
        public int Amount { get; set; }
        public int BankId { get; set; }
        public string TranMode { get; set; }
        public string Comments { get; set; }
        public int AddedBy { get; set; }
        public int TranModeId { get; set; }

        public int BillId { get; set; }

    }

    public class CustomerInfo_PayAdv_SwUpdate
    {
      
        public int CustPayId { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime PayTime { get; set; }
        public int CustomerID { get; set; }
        public int OrderAmount { get; set; }
        public int Amount { get; set; }
        public int BankId { get; set; }
        public string TranMode { get; set; }
        public string Comments { get; set; }
        public int UpdatedBy { get; set; }
        public int TranModeId { get; set; }

        public int BillId { get; set; }
    }
    


}
