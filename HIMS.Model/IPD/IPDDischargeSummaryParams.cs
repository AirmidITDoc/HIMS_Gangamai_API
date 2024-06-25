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
        public DateTime OPDate { get; set; }
        public DateTime OPTime { get; set; }
        public int DischargeDoctor1 { get; set; }
        public int DischargeDoctor2 { get; set; }
        public int DischargeDoctor3 { get; set; }
        public DateTime DischargeSummaryTime { get; set; }
        public String DoctorAssistantName { get; set; }


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

