using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CustomerInvoiceRaiseParam
    {
        public CustomerInvoiceRaiseInsert customerInvoiceRaiseInsert { get; set; }
        public CustomerInvoiceRaiseUpdate customerInvoiceRaiseUpdate { get; set; }

    }
    public class CustomerInvoiceRaiseInsert
    {
        public string InvNumber { get; set; }
        public DateTime InvDate { get; set; }
        public long CustomerId { get; set; }
        public long Amount { get; set; }
        public long CreatedBy { get; set; }
        public long InvoiceRaisedId { get; set; }
    }
}
public class CustomerInvoiceRaiseUpdate 
{
    public string InvNumber { get; set; }
    public DateTime InvDate { get; set; }
    public long CustomerId { get; set; }
    public long Amount { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public long ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
public long InvoiceRaisedId { get; set; }
}

