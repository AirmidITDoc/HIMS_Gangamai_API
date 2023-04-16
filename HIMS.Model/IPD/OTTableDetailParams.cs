using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class OTTableDetailParams
    {
        public OTTableDetailInsert OTTableDetailInsert { get; set; }

        public OTTableDetailUpdate OTTableDetailUpdate { get; set; }
    }

    public class OTTableDetailInsert
    {
        public DateTime TranDate { get; set; }
        public int OPD_IPD_ID { get; set; }
        public int OTHeaderNo { get; set; }
        public String TheaterName { get; set; }


        public int SurgeonName { get; set; }
        public int AnaName { get; set; }
        public DateTime OpDate { get; set; }
        public DateTime OPTime { get; set; }

        public String OperationNotes { get; set; }
        public int AnaType { get; set; }
        public int ProcedureType { get; set; }
        public String BirthRegNo { get; set; }

        public int PaediatricionName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthTime { get; set; }
        public bool IsBirthTimechk { get; set; }
        public int SurgeonAmt { get; set; }
        public int AnaAmt { get; set; }
        public int PadAmt { get; set; }

        public int ProcedueAmt { get; set; }
    }


    public class OTTableDetailUpdate{


        public int OTDetailID { get; set; }
        public DateTime TranDate { get; set; }
        public int OPD_IPD_ID { get; set; }
        public int OTHeaderNo { get; set; }
        public String TheaterName { get; set; }


        public int SurgeonName { get; set; }
        public int AnaName { get; set; }
        public DateTime OpDate { get; set; }
        public DateTime OPTime { get; set; }

        public String OperationNotes { get; set; }
        public int AnaType { get; set; }
        public int ProcedureType { get; set; }
        public String BirthRegNo { get; set; }

        public int PaediatricionName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthTime { get; set; }
        public bool IsBirthTimechk { get; set; }
        public int SurgeonAmt { get; set; }
        public int AnaAmt { get; set; }
        public int PadAmt { get; set; }

        public int ProcedueAmt { get; set; }

    }

}
