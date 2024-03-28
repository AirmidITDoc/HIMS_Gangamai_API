using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.HomeDelivery
{
    public class HomeDeliveryLoginParams
    {
        public HomeDeliveryLoginCreate HomeDeliveryLoginCreate { get; set; }
    }

    public class HomeDeliveryLoginCreate
    {
        public long CustomerId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public long CustomerPinCode { get; set; }
        public string CustomerPasscode { get; set; }
    }

    public class HomeDeliveryProfileUpdate
    {
        public long CustomerId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public long CustomerPinCode { get; set; }
        public string CustomerPasscode { get; set; }
    }
}
