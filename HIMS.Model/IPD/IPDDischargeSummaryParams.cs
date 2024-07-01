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
        public UpdateAdmisionDischargeSummary UpdateAdmisionDischargeSummary { get; set; }
        public List<InsertIPPrescriptionDischarge> InsertIPPrescriptionDischarge { get; set; }
        public DeleteIPPrescriptionDischarge DeleteIPPrescriptionDischarge { get; set; }
    }


    public class InsertIPDDischargSummary
    {
        public int DischargesummaryId { get; set; }
        public long AdmissionId { get; set; }
        public long DischargeId { get; set; }
        public string History { get; set; }
        public string Diagnosis { get; set; }
        public string Investigation { get; set; }
        public string ClinicalFinding { get; set; }
        public string OpertiveNotes { get; set; }
        public string TreatmentGiven { get; set; }
        public string TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime Followupdate { get; set; }
        public string Remark { get; set; }
        public DateTime DischargeSummaryDate { get; set; }
        public DateTime DischargeSummaryTime { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public long DischargeDoctor1 { get; set; }
        public long DischargeDoctor2 { get; set; }
        public long DischargeDoctor3 { get; set; }
        public string DoctorAssistantName { get; set; }
        public string ClaimNumber { get; set; }
        public string PreOthNumber { get; set; }
        public long AddedBy { get; set; }
        public string SurgeryProcDone { get; set; }
        public string ICD10CODE { get; set; }
        public string ClinicalConditionOnAdmisssion { get; set; }
        public string OtherConDrOpinions { get; set; }
        public string ConditionAtTheTimeOfDischarge { get; set; }
        public string PainManagementTechnique { get; set; }
        public string LifeStyle { get; set; }
        public string WarningSymptoms { get; set; }
        public string Radiology { get; set; }
        public bool IsNormalOrDeath { get; set; }

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
        public string History { get; set; }
        public string Diagnosis { get; set; }
        public string Investigation { get; set; }
        public string ClinicalFinding { get; set; }
        public string OpertiveNotes { get; set; }
        public string TreatmentGiven { get; set; }
        public string TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime Followupdate { get; set; }
        public string Remark { get; set; }
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public long DischargeDoctor1 { get; set; }
        public long DischargeDoctor2 { get; set; }
        public long DischargeDoctor3 { get; set; }
        public string DoctorAssistantName { get; set; }
        public string ClaimNumber { get; set; }
        public string PreOthNumber { get; set; }
        public long UpdatedBy { get; set; }
        public string SurgeryProcDone { get; set; }
        public string ICD10CODE { get; set; }
        public string ClinicalConditionOnAdmisssion { get; set; }
        public string OtherConDrOpinions { get; set; }
        public string ConditionAtTheTimeOfDischarge { get; set; }
        public string PainManagementTechnique { get; set; }
        public string LifeStyle { get; set; }
        public string WarningSymptoms { get; set; }
        public string Radiology { get; set; }
        public bool IsNormalOrDeath { get; set; }


    }
    public class InsertIPPrescriptionDischarge
    {
        public long OPD_IPD_ID { get; set; }
        public long OPD_IPD_Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime PTime { get; set; }
        public long ClassID { get; set; }
        public long GenericId { get; set; }
        public long DrugId { get; set; }
        public long DoseId { get; set; }
        public long Days { get; set; }
        public long InstructionId { get; set; }
        public long QtyPerDay { get; set; }
        public long TotalQty { get; set; }
        public long Instruction { get; set; }
        public long Remark { get; set; }
        public long IsEnglishOrIsMarathi { get; set; }
        public long StoreId { get; set; }
        public long CreatedBy { get; set; }
    }
    public class DeleteIPPrescriptionDischarge
    {
        public long OPD_IPD_ID { get; set; }
    }

}

