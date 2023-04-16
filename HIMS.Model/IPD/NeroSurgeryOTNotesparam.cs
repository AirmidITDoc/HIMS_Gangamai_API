using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class NeroSurgeryOTNotesparam
    {
        public NeroSurgeryOTNotesInsert NeroSurgeryOTNotesInsert { get; set; }

        public NeroSurgeryOTNotesUpdate NeroSurgeryOTNotesUpdate { get; set; }
    }

    public class NeroSurgeryOTNotesInsert
    {


        public int AdmissionID { get; set; }
        public int OP_IP_Type { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public DateTime OTDate { get; set; }
        public DateTime OTTime { get; set; }
        public String SurgeryName { get; set; }
        public int SurgeonID { get; set; }
        public int SurgeonID1 { get; set; }
        public int SurgeonID2 { get; set; }

        public int AnesthetishID { get; set; }


        public int AnesthetishID1 { get; set; }

        public int AnesthetishID2 { get; set; }
        public String AnesthetishType { get; set; }

        public String Position { get; set; }

        public String BloodLoss { get; set; }

        public String Findings { get; set; }

        public String Surgery { get; set; }

        public String AdviceonDischarge { get; set; }

        public String SurgeryType { get; set; }

        public int Addedby { get; set; }


        public int OTNeroId { get; set; }


    }

    public class NeroSurgeryOTNotesUpdate{

        public int OTNeroId { get; set; }
      //  public int OP_IP_Type { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public DateTime OTDate { get; set; }
        public DateTime OTTime { get; set; }
        public String SurgeryName { get; set; }
        public int SurgeonID { get; set; }
        public int SurgeonID1 { get; set; }
        public int SurgeonID2 { get; set; }

        public int AnesthetishID { get; set; }


        public int AnesthetishID1 { get; set; }

        public int AnesthetishID2 { get; set; }
        public String AnesthetishType { get; set; }

        public String Position { get; set; }

        public String BloodLoss { get; set; }

        public String Findings { get; set; }

        public String Surgery { get; set; }

        public String AdviceonDischarge { get; set; }

        public String SurgeryType { get; set; }

        public int UpdatedBy { get; set; }


        
    }


}

