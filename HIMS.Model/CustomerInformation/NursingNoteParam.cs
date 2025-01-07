using HIMS.Model.HomeDelivery;
using System;
using System.Collections.Generic;
using System.Text;
using  HIMS.Model.CustomerInformation;

namespace HIMS.Model.CustomerInformation
{
    public class NursingNoteParam
    {
        public SaveNursingNoteParam SaveNursingNoteParam { get; set; }
        public UpdateNursingNoteParam UpdateNursingNoteParam { get; set; }
      
    }

    public class SaveNursingNoteParam
    {
      
        public int DocNoteId { get; set; }
        public int AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string NursingNotes { get; set; }
        public string CreatedBy { get; set; }
       


      
    }
    public class UpdateNursingNoteParam
    {
        public int DocNoteId { get; set; }
        public int AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string NursingNotes { get; set; }
        public string ModifiedBy { get; set; }


    }
}

    