using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class OTBookingDetailParams
    {
        public OTTableBookingDetailInsert OTTableBookingDetailInsert { get; set; }
        public OTTableBookingDetailUpdate OTTableBookingDetailUpdate { get; set; }

    }

    public class OTTableBookingDetailInsert{


        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
       
        public int OP_IP_ID { get; set; }
        public int OP_IP_Type { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public int Duration { get; set; }
        public int OTTableID { get; set; }

      
        public int SurgeonId { get; set; }
       public int SurgeonId1 { get; set; }
   
        public int AnestheticsDr { get; set; }
        public int AnestheticsDr1 { get; set; }
        public String Surgeryname { get; set; }
        public int ProcedureId { get; set; }

        public String AnesthType { get; set; }

        public bool UnBooking { get; set; }
        public String Instruction { get; set; }
       
        public int IsAddedBy { get; set; }
        public int OTBookingID { get; set; }
   
}

    public class OTTableBookingDetailUpdate
    {


        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }

        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public int Duration { get; set; }
        public int OTTableID { get; set; }


        public int SurgeonId { get; set; }
        public int SurgeonId1 { get; set; }

        public int AnestheticsDr { get; set; }
        public int AnestheticsDr1 { get; set; }
        public String Surgeryname { get; set; }
        public int ProcedureId { get; set; }

        public String AnesthType { get; set; }

        public bool UnBooking { get; set; }
        public String Instruction { get; set; }

        public int IsUpdatedBy { get; set; }
        public int OTBookingID { get; set; }

    }

}
