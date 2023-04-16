using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class DoctorNoteparam
    {

        public DoctorNoteInsert DoctorNoteInsert { get; set; }
       // public DoctorNoteUpdate DoctorNoteUpdate { get; set; }
    }

    public class DoctorNoteInsert
    {
        public int AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public int IsAddedBy { get; set; }
        public int DoctNoteId { get; set; }
        public String DoctorsNotes { get; set; }
    }
}