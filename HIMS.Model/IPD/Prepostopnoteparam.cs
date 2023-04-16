using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
    public class Prepostopnoteparam
    {
        public InsertPrepostopnote InsertPrepostopnote { get; set; }
        public UpdatePrepostopnote UpdatePrepostopnote { get; set; }

    }


    public class InsertPrepostopnote
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

        public String Assistant { get; set; }
        public int AnesthetishID { get; set; }


        public int AnesthetishID1 { get; set; }

        public int AnesthetishID2 { get; set; }
        public String AnesthetishType { get; set; }

        public String Incision { get; set; }
        public String OperativeDiagnosis { get; set; }

        public String OperativeFindings { get; set; }

        public String OperativeProcedure { get; set; }
        public String ExtraProPerformed { get; set; }
        public String ClosureTechnique { get; set; }

        public String PostOpertiveInstru { get; set; }

        public String DetSpecimenForLab { get; set; }
        public int Addedby { get; set; }

        public String SurgeryType { get; set; }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
       
        public int OTId { get; set; }


    }
    public class UpdatePrepostopnote
    {


      //  public int AdmissionID { get; set; }
      //  public int OP_IP_Type { get; set; }
        public DateTime TranDate { get; set; }

        public DateTime TranTime { get; set; }

        public DateTime OTDate { get; set; }

        public DateTime OTTime { get; set; }
        public String SurgeryName { get; set; }
        public int SurgeonID { get; set; }
        public int SurgeonID1 { get; set; }

        public String Assistant { get; set; }
        public int AnesthetishID { get; set; }


        public int AnesthetishID1 { get; set; }

        public int AnesthetishID2 { get; set; }
        public String AnesthetishType { get; set; }

        public String Incision { get; set; }
        public String OperativeDiagnosis { get; set; }

        public String OperativeFindings { get; set; }

        public String OperativeProcedure { get; set; }
        public String ExtraProPerformed { get; set; }
        public String ClosureTechnique { get; set; }

        public String PostOpertiveInstru { get; set; }

        public String DetSpecimenForLab { get; set; }
        public int Updatedby { get; set; }

        public String SurgeryType { get; set; }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        public int OTId { get; set; }


    }
}