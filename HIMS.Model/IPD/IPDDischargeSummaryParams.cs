using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class IPDDischargeSummaryParams
    {
        public InsertIPDDischargSummary InsertIPDDischargSummary { get; set; }
        public UpdateIPDDischargSummary UpdateIPDDischargSummary { get; set; }
        public IPSMSInsert IPSMSInsert { get; set; }
      //  public IPSMSInsert IPSMSInsert { get; set; }
        public UpdateAdmisionDischargeSummary UpdateAdmisionDischargeSummary { get; set; }


    }


    public class InsertIPDDischargSummary
    {
            public int DischargesummaryId { get; set; }
         public long AdmissionId { get; set; }
          public long DischargeId { get; set; }
          public String History { get; set; }
          public String Diagnosis { get; set; }

          public String Investigation { get; set; }
          public String ClinicalFinding { get; set; }
          public String OpertiveNotes { get; set; }
          public String TreatmentGiven { get; set; }
          public String TreatmentAdvisedAfterDischarge { get; set; }
          public DateTime Followupdate { get; set; }
          public String Remark { get; set; }
          public DateTime DischargeSummaryDate { get; set; }

          public DateTime OPDate { get; set; }
          public DateTime OPTime { get; set; }
          public int DischargeDoctor1 { get; set; }
          public int DischargeDoctor2 { get; set; }
          public int DischargeDoctor3 { get; set; }
          public DateTime DischargeSummaryTime { get; set; }
          public String DoctorAssistantName { get; set; }

        /*   public String ClaimNumber { get; set; }

          public String PreOthNumber { get; set; }
          public int AddedBy { get; set; }
          public DateTime AddedByDate { get; set; }
          public String SurgeryProcDone { get; set; }

          public String ICD10CODE { get; set; }
          public String ClinicalConditionOnAdmisssion { get; set; }

          public String OtherConDrOpinions { get; set; }
          public String ConditionAtTheTimeOfDischarge { get; set; }

          public String PainManagementTechnique { get; set; }
          public String LifeStyle { get; set; }
          public String WarningSymptoms { get; set; }
          public String Radiology { get; set; }
          public int IsNormalOrDeath { get; set; }
          public int DischargesummaryId { get; set; }

        public long DischargeId { get; set; }
        public long AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }

        public int DischargeTypeId { get; set; }
        public int DischargedDocId { get; set; }
        public int DischargedRMOID { get; set; }
        public int AddedBy { get; set; }

        public int UpdatedBy { get; set; }*/

    }

    public class IPSMSInsert
    {
        public String VstCode { get; set; }
        public int MsgId { get; set; }
        public int PatientType { get; set; }
       
    }
    public class UpdateAdmisionDischargeSummary
    {
        public int AdmissionId { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime DischargeTime { get; set; }
        public int IsDischarged { get; set; }

    }
    public class UpdateIPDDischargSummary
    {

        public int DischargesummaryId { get; set; }
        public long DischargeId { get; set; }
        public String History { get; set; }
        public String Diagnosis { get; set; }
        public String Investigation { get; set; }
        public String ClinicalFinding { get; set; }
        public String OpertiveNotes { get; set; }
        public String TreatmentGiven { get; set; }
        public String TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime Followupdate { get; set; }
        public String Remark { get; set; }
     //   public DateTime DischargeSummaryDate { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public int DischargeDoctor1 { get; set; }
        public int DischargeDoctor2 { get; set; }
        public int DischargeDoctor3 { get; set; }
        public DateTime DischargeSummaryTime { get; set; }
        public String DoctorAssistantName { get; set; }
      
       
    }

}

