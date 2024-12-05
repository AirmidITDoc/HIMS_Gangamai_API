using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class SaveNursingPainAssessmentParam
    {
        public SaveNursingPainAssessment SaveNursingPainAssessment { get; set; }
        public UpdateNursingPainAssessment UpdateNursingPainAssessment { get; set; }

    }
    public class SaveNursingPainAssessment
    {
       
        public DateTime PainAssessmentDate { get; set; }
        public DateTime PainAssessmentTime { get; set; }
        public long AdmissionId { get; set; }
        public long PainAssessementValue { get; set; }
        public long CreatedBy { get; set; }
       

    }

    public class UpdateNursingPainAssessment
    {

        public long PainAssessmentId { get; set; }
        public DateTime PainAssessmentDate { get; set; }
        public DateTime PainAssessmentTime { get; set; }
        public long AdmissionId { get; set; }
        public long PainAssessementValue { get; set; }
        public long ModifiedBy { get; set; }
       



    }

}
