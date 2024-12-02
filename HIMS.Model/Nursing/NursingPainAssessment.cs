using HIMS.Model.WhatsAppEmail;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Nursing
{
    public class NursingPainAssessment
    {
        public NursingPainAssessmentSave NursingPainAssessmentSave { get; set; }
        public NursingPainAssessmentUpdate NursingPainAssessmentUpdate { get; set; }
    }
    public class NursingPainAssessmentSave
    {
        public DateTime PainAssessmentDate { get; set; }
        public DateTime PainAssessmentTime { get; set; }
        public long AdmissionId { get; set; }
        public long PainAssessementValue { get; set; }
        public long CreatedBy { get; set; }
    }
    public class NursingPainAssessmentUpdate
    {
        public long PainAssessmentId { get; set; }
        public DateTime PainAssessmentDate { get; set; }
        public DateTime PainAssessmentTime { get; set; }
        public long AdmissionId { get; set; }
        public long PainAssessementValue { get; set; }
        public long ModifiedBy { get; set; }

    }

}
