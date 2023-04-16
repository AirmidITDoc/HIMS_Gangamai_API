using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class CustomerEnquiryParams
    {
        public CustomerEnquiryInsert CustomerEnquiryInsert { get; set; }
        public CustomerEnquiryUpdate CustomerEnquiryUpdate { get; set; }
    }

    public class CustomerEnquiryInsert
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public long BudgetAmount { get; set; }
        public long AddedBy { get; set; }
    }

    public class CustomerEnquiryUpdate
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public long BudgetAmount { get; set; }
        public long UpdatedBy { get; set; }
    }
}
