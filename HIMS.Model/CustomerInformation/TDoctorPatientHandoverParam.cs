using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public  class TDoctorPatientHandoverParam
    {
        public SaveTDoctorPatientHandoverParam SaveTDoctorPatientHandoverParam { get; set; }
        public UpdateTDoctorPatientHandoverParam UpdateTDoctorPatientHandoverParam { get; set; }
    }

    public class SaveTDoctorPatientHandoverParam
    { 
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string ShiftInfo { get; set; }
        public string PatHand_I { get; set; }
        public string PatHand_S { get; set; }
        public string PatHand_B { get; set; }
        public string PatHand_A { get; set; }
        public string PatHand_R { get; set; }
        public long CreatedBy { get; set; }
        public long DocHandId { get; set; }

    }
    public class UpdateTDoctorPatientHandoverParam
    { 
        public long DocHandId { get; set; }
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string ShiftInfo { get; set; }
        public string PatHand_I { get; set; }
        public string PatHand_S { get; set; }
        public string PatHand_B { get; set; }
        public string PatHand_A { get; set; }
        public string PatHand_R { get; set; } 
        public long ModifiedBy { get; set; }

    }

}
