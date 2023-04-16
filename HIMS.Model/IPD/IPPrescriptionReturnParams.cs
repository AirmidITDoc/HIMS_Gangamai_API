using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class IPPrescriptionReturnParams
    {
        public IPPrescriptionReturnH IPPrescriptionReturnH { get; set; }
        public IPPrescriptionReturnD IPPrescriptionReturnD { get; set; }

    }

      public class IPPrescriptionReturnH
    {

        public DateTime PresDate { get; set; }
        public DateTime PresTime { get; set; }
        public int ToStoreId { get; set; }
        public int OP_IP_Id { get; set; }
        public int OP_IP_Type { get; set; }
        
        public int Addedby { get; set; }
        public int Isdeleted { get; set; }
        public Boolean Isclosed { get; set; }

        public int PresReId { get; set; }
    }


    public class IPPrescriptionReturnD
    {
        
       // public int PresDetailsId { get; set; }
        public int PresReId { get; set; }
        public DateTime BatchExpDate { get; set; }
        public int ItemId { get; set; }
        public String BatchNo { get; set; }
        public long Qty { get; set; }
      
      
    }
}

