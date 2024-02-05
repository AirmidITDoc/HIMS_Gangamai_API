using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class Doctorshareparam
    {
        public DoctorshareheaderInsert DoctorshareheaderInsert { get; set; }
        public DoctorsharemasterUpdate DoctorsharemasterUpdate { get; set; }
    }



    public class  DoctorsharemasterUpdate
    {
        public int DoctorId { get; set; }

        public int ServiceId { get; set; }
        public int servicePercentage { get; set; }
        public int DocShrType { get; set; }

        public string DocShrTypeS { get; set; }
        public int ServiceAmount { get; set; }

        public int ClassId { get; set; }

        public int ShrTypeSerOrGrp { get; set; }

        public int Op_IP_Type { get; set; }

    }


    public class DoctorshareheaderInsert
    {

        public DateTime GDate { get; set; }

        public DateTime GTime { get; set; }
        public int DoctorId { get; set; }

        public int GroupId { get; set; }
        public int TotalAmount { get; set; }
        public int Percentage { get; set; }
        public int PerAmount { get; set; }

        public int TDSPercentage { get; set; }

        public int TDSAmount { get; set; }

        public int NetPayableAmt { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int Op_Ip_Type { get; set; }

        public int DocShareId { get; set; }


    }
}
