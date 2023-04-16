using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public  class OTNotesTemplateparam
    {
        public OTNoteTemplateInsert OTNoteTemplateInsert { get; set; }
        public OTNoteTemplateUpdate OTNoteTemplateUpdate { get; set; }
    }

    public class OTNoteTemplateInsert
    {


        public String OTTemplateName { get; set; }
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
        public int OTReservationId { get; set; }

        public String BloodLoss { get; set; }



        public int SurgeryID { get; set; }
        public String SorubNurse { get; set; }

        public String Histopathology { get; set; }

        public String BostOPOrders { get; set; }
        public int AnestTypeId { get; set; }
        public int SiteDescID { get; set; }

        public String ComplicationMode { get; set; }


        public int ServiceId { get; set; }
        public int ProcedureId { get; set; }

        public int OTNoteTempId { get; set; }


    }

    public class OTNoteTemplateUpdate
    {

        public int OTGenSurId { get; set; }
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
        public int UpdatedBy { get; set; }

        public String SurgeryType { get; set; }

        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    //    public int OTReservationId { get; set; }

        public String BloodLoss { get; set; }

        public int SurgeryID { get; set; }
        public String SorubNurse { get; set; }

        public String Histopathology { get; set; }

        public String BostOPOrders { get; set; }
        public String OTTemplateName { get; set; }

        public int AnestTypeId { get; set; }
        public int SiteDescID { get; set; }

        public String ComplicationMode { get; set; }


        public int ServiceId { get; set; }
        public int ProcedureId { get; set; }

       

    }

}


