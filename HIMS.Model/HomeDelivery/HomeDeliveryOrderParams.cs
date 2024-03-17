using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.HomeDelivery
{
    public class HomeDeliveryOrderParams
    {
        public HomeDeliveryOrderInsert HomeDeliveryOrderInsert { get; set; }
    }
    public class HomeDeliveryOrderInsert
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderTime { get; set; }
        public long CustomerID { get; set; }
        public string UploadDocument { get; set; }
        public long StoreId { get; set; }
    }
}
