using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
  public class OTRequestparam
    {
        public OTTableRequestInsert OTTableRequestInsert { get; set; }
        public OTTableRequestUpdate OTTableRequestUpdate { get; set; }
    }

    public class OTTableRequestInsert
    {


        public DateTime OTbookingDate { get; set; }
        public DateTime OTbookingTime { get; set; }

        public int OP_IP_ID { get; set; }
        public int OP_IP_Type { get; set; }

        public int SurgeonId { get; set; }
        public int SurgeryId { get; set; }

        public String SurgeryType { get; set; }
        public int DepartmentId { get; set; }

        public int CategoryId { get; set; }

        public int AddedBy { get; set; }
        public DateTime AddedDateTime { get; set; }
        public bool IsCancelled { get; set; }
     
        public int SiteDescId { get; set; }

       

    }

    public class OTTableRequestUpdate
    {


        public DateTime OTbookingDate { get; set; }
        public DateTime OTbookingTime { get; set; }

        public int OP_IP_ID { get; set; }
        public int OP_IP_Type { get; set; }

        public int SurgeonId { get; set; }
        public int SurgeryId { get; set; }

        public String SurgeryType { get; set; }
        public int DepartmentId { get; set; }

        public int CategoryId { get; set; }

        public int AddedBy { get; set; }
        public DateTime AddedDateTime { get; set; }
        public bool IsCancelled { get; set; }

        public int SiteDescId { get; set; }

    }

}



