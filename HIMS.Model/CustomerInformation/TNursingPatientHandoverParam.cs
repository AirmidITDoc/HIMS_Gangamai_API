using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class TNursingPatientHandoverParam
    {
        public SaveTNursingPatientHandoverParam SaveTNursingPatientHandoverParam { get; set; }
        public UpdateTNursingPatientHandoverParam UpdateTNursingPatientHandoverParam { get; set; }

    }

    public class SaveTNursingPatientHandoverParam
    {
        public int PatHandId { get; set; }
        public int AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
      
        public string ShiftInfo { get; set; }
        public long PatHand_I { get; set; }
        public long PatHand_S { get; set; }
        public long PatHand_B { get; set; }
        public long PatHand_A { get; set; }
        public long PatHand_R { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }




    }
    public class UpdateTNursingPatientHandoverParam
    {
        public int PatHandId { get; set; }
        public int AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }

        public string ShiftInfo { get; set; }
        public long PatHand_I { get; set; }
        public long PatHand_S { get; set; }
        public long PatHand_B { get; set; }
        public long PatHand_A { get; set; }
        public long PatHand_R { get; set; }
        public string Comments { get; set; }
     

        public int ModifiedBy { get; set; }
       

    }
   
    


    }


    