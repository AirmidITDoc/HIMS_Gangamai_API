using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class NursingWeightParam
    {
        public SaveNursingWeight SaveNursingWeight { get; set; }
        public UpdateNursingWeight UpdateNursingWeight { get; set; }

    }
    public class SaveNursingWeight
    {
       
        public DateTime PatWeightDate { get; set; }
        public DateTime PatWeightTime { get; set; }
        public long AdmissionId { get; set; }
        public long PatWeightValue { get; set; }
        public long CreatedBy { get; set; }
       

    }

    public class UpdateNursingWeight
    {

        public long PatWeightId { get; set; }
        public DateTime PatWeightDate { get; set; }
        public DateTime PatWeightTime { get; set; }
        public long AdmissionId { get; set; }
        public long PatWeightValue { get; set; }
        public long ModifiedBy { get; set; }
       



    }

}
