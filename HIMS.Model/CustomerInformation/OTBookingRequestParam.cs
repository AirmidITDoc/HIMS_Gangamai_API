using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class OTBookingRequestParam
    {
        public SaveOTBookingRequestParam SaveOTBookingRequestParam { get; set; }
        public UpdateOTBookingRequestParam UpdateOTBookingRequestParam { get; set; }
        public CancelOTBookingRequestParam CancelOTBookingRequestParam { get; set; }
    }

    public class SaveOTBookingRequestParam
    {
        public long OTBookingId { get; set; }
        public DateTime OTBookingDate { get; set; }
        public DateTime OTBookingTime { get; set; }
        public long OP_IP_Id { get; set; }
        public long OP_IP_Type { get; set; }
        public long SurgeryType { get; set; }
        public long DepartmentId { get; set; }
        public long CategoryId { get; set; }
        public long SiteDescId { get; set; }
        public long SurgeonId { get; set; }
        public long SurgeryId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDateTime { get; set; }

       

    }
    public class UpdateOTBookingRequestParam
    {
        public long OTBookingId { get; set; }
        public DateTime OTBookingDate { get; set; }
        public DateTime OTBookingTime { get; set; }
        public long OP_IP_Id { get; set; }
        public long OP_IP_Type { get; set; }
        public long SurgeryType { get; set; }
        public long DepartmentId { get; set; }
        public long CategoryId { get; set; }
        public long SiteDescId { get; set; }
        public long SurgeonId { get; set; }
        public long SurgeryId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDateTime { get; set; }
    }
    public class CancelOTBookingRequestParam
    {
        public long OTBookingId { get; set; }
       
        public long IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDateTime { get; set; }
    }
}

    