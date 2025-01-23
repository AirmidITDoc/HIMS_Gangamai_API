using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;
using static HIMS.Model.CustomerInformation.UpdateOTBookingParam;

namespace HIMS.Model.CustomerInformation
{
    public class OTBookingParam
    {
        public SaveOTBookingParam SaveOTBookingParam { get; set; }
        public UpdateOTBookingParam UpdateOTBookingParam { get; set; }
        public CancelOTBookingParam CancelOTBookingParam { get; set; }
    }

    public class SaveOTBookingParam
    {
        public long OTBookingID { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public long Duration { get; set; }
        public long OTTableID { get; set; }
        public long SurgeonId { get; set; }
        public long SurgeonId1 { get; set; }
        public long AnestheticsDr { get; set; }
        public long AnestheticsDr1 { get; set; }
        public string Surgeryname { get; set; }
        public long ProcedureId { get; set; }
        public long AnesthType { get; set; }
        public long UnBooking { get; set; }
        public string Instruction { get; set; }
        public long OTTypeID { get; set; }
        public long OTRequestId { get; set; }
        public long CreatedBy { get; set; }
        
    }
    public class UpdateOTBookingParam
    {
        public long OTBookingID { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public long Duration { get; set; }
        public long OTTableID { get; set; }
        public long SurgeonId { get; set; }
        public long SurgeonId1 { get; set; }
        public long AnestheticsDr { get; set; }
        public long AnestheticsDr1 { get; set; }
        public string Surgeryname { get; set; }
        public long ProcedureId { get; set; }
        public long AnesthType { get; set; }
        public long UnBooking { get; set; }
        public string Instruction { get; set; }
        public long OTTypeID { get; set; }
        public long OTRequestId { get; set; }
        public long ModifiedBy { get; set; }
        

        public class CancelOTBookingParam
        {
            public long OTBookingID { get; set; }

            public long IsCancelled { get; set; }
            public long IsCancelledBy { get; set; }
            
        }
    }
}

    