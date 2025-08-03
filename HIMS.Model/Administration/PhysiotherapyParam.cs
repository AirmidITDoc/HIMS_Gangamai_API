using HIMS.Model.CustomerInformation;
using HIMS.Model.Opd;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace HIMS.Model.Administration
{
    public class PhysiotherapyParam
    {
        public InsertPhysiotherapyHeader InsertPhysiotherapyHeader { get; set; }
        public UpdatePhysiotherapyDetails UpdatePhysiotherapyDetails { get; set; }
        //public List<InsertPhysiotherapyDetails InsertPhysiotherapyDetails { get; set; }
        //public List<UpdatePhysiotherapyDetails UpdatePhysiotherapyDetails { get; set; }


    }


    public class InsertPhysiotherapyHeader
    {
        public int PhysioId { get; set; }
        public DateTime PhysioDate { get; set; }
        public DateTime PhysioTime { get; set; }
        public int VisitiId { get; set; }
        public int RegId { get; set; }
        public DateTime Startdate { get; set; }
        public int Interval { get; set; }
        public int NoSession { get; set; }
        public DateTime EndDate { get; set; }
        public long CreatedBy { get; set; }


    }





    public class UpdatePhysiotherapyDetails
    {
        public int PhysioDetId { get; set; }
        public int PhysioId { get; set; }
        public DateTime SessionStartdate { get; set; }
        public int Interval { get; set; }

        public int NoSession { get; set; }
        public DateTime SessionendDate { get; set; }
        public bool IsCompleted { get; set; }
        public String Comments { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
