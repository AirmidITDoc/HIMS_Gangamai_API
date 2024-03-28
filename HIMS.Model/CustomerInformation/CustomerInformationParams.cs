using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class CustomerInformationParams
    {
        public CustomerInformationInsert CustomerInformationInsert { get; set; }
        public CustomerInformationUpdate CustomerInformationUpdate { get; set; }
    }

    public class CustomerInformationInsert
    {
        public long CustomerId { get; set; }
        public  string CustomerName { get; set; } 
        public string CustomerAddress { get; set; }
        public string CustomerMobileNO { get; set; }
        public string CustomerPinCode { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonMobileNo { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime AMCDate { get; set; }
        public long  CreatedBy { get; set; }
    }
    public class CustomerInformationUpdate
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobileNO { get; set; }
        public string CustomerPinCode { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonMobileNo { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime AMCDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime Modifieddatetime { get; set; }
    }

}


