using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class CustomerParams
    {
        public CustomerInsert CustomerInsert { get; set; }
        public CustomerUpdate CustomerUpdate { get; set; }
    }
    public class CustomerInsert
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public long Amount { get; set; }
        public long AddedBy { get; set; }

        public long ProjectId { get; set; }

    }

    public class CustomerUpdate
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public long Amount { get; set; }
        public long UpdatedBy { get; set; }

        public long ProjectId { get; set; }
    }

}
