using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Opd
{
   public class IPDischargeParams
    {
        public InsertIPDDischarg InsertIPDDischarg { get; set; }
        public UpdateAdmission UpdateAdmission { get; set; }

         public UpdateIPDDischarg UpdateIPDDischarg { get; set; }

        //  public InsertIPSMSTemplete InsertIPSMSTemplete { get; set; }
    }

    public class InsertIPDDischarg
    {
        public long DischargeId { get; set; }
        public long AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }
     
        public int DischargeTypeId { get; set; }
        public int DischargedDocId { get; set; }
        public int DischargedRMOID { get; set; }

        public String Modeofdischarge { get; set; }
        public int AddedBy { get; set; }

       
    }


    public class UpdateIPDDischarg
    {

       // public long AdmissionId { get; set; }
        public long DischargeId { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }

        public int DischargeTypeId { get; set; }
        public int DischargedDocId { get; set; }
        public int DischargedRMOID { get; set; }

        public String Modeofdischarge { get; set; }
     
        public int UpdatedBy { get; set; }

    }

    public class UpdateAdmission
    {
       
        public int AdmissionID { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }
        public int IsDischarged { get; set; }
        
    }

    public class UpdateDischargeSummary
    { 
        public int DischargesummaryId { get; set; }
      //  public int AdmissionId { get; set; }
        public int DischargeId { get; set; }
        public String History { get; set; }

        public String Diagnosis { get; set; }
        public String Investigation { get; set; }
        public String ClinicalFinding { get; set; }
        public String OpertiveNotes { get; set; }
        public String TreatmentGiven { get; set; }
        public String TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime Followupdate { get; set; }
        public String Remark { get; set; }
        public DateTime OPDate { get; set; }

        public DateTime OPTime { get; set; }
        public int DischargeDoctor1 { get; set; }
        public int DischargeDoctor2 { get; set; }
        public int DischargeDoctor3 { get; set; }
        public DateTime DischargeSummaryTime { get; set; }
        public String DoctorAssistantName { get; set; }
    }

    public class InsertIPSMSTemplete
    {
        public String VstCode { get; set; }
        public int MsgId { get; set; }

        public int PatientType { get; set; }
    }
}