using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
   public class CustomerInformation_SwParams
    {
        public CustomerInformation_SwInsert CustomerInformation_SwInsert { get; set; }
        public CustomerInformation_SwUpdate CustomerInformation_SwUpdate { get; set; }
    }

    public class CustomerInformation_SwInsert
    {
        public DateTime TranDate { get; set; }
        public string HospitalName { get; set; }
        public string AddressName { get; set; }
        public string ContactName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public int OrderAmount { get; set; }
        public int AMCPercentage { get; set; }
        public string OrderCloserPerson { get; set; }
        public DateTime InstallDate { get; set; }
        public DateTime AMCDate { get; set; }
        public int AddedBy { get; set; }
       
    }

    public class CustomerInformation_SwUpdate
    {
        public int CustomerId { get; set; }
        public DateTime TranDate { get; set; }
        public string HospitalName { get; set; }
        public string AddressName { get; set; }
        public string ContactName { get; set; }
        public string MobileNo { get; set; }
        public string WhatsupMobileNo { get; set; }
        public string EmailId { get; set; }
        public int OrderAmount { get; set; }
        public int AMCPercentage { get; set; }
        public string OrderCloserPerson { get; set; }
        public DateTime InstallDate { get; set; }
        public DateTime AMCDate { get; set; }
        public int UpdatedBy { get; set; }

    }
}
