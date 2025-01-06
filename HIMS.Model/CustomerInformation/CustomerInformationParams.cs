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
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobileNO { get; set; }
        public string CustomerPinCode { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonMobileNo { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime AMCDate { get; set; }
        public long CreatedBy { get; set; }
        public long IsActive { get; set; }
        public long CustomerId { get; set; }
    }
    public class CustomerInformationUpdate
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { set; get; }
        public string CustomerMobileNO { set; get; }
        public string CustomerPinCode { set; get; }
        public string ContactPersonName { set; get; }
        public string ContactPersonMobileNo { set; get; }
        public DateTime InstallationDate { set; get; }
        public DateTime AMCDate { set; get; }
        public long CreatedBy { set; get; }
        public DateTime CreatedDatetime { set; get; }
        public long ModifiedBy { set; get; }
        public DateTime Modifieddatetime { set; get; }
    }
}

    