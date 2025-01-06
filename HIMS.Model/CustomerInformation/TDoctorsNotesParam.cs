using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class TDoctorsNotesParam
    {
        public SaveTDoctorsNotesParam SaveTDoctorsNotesParam { get; set; }
        public UpdateTDoctorsNotesParam UpdateTDoctorsNotesParam { get; set; }
    }
    public class SaveTDoctorsNotesParam
    {
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string DoctorsNotes { get; set; }
        public long CreatedBy { get; set; }

        public long DoctNoteId { get; set; }
    }
    public class UpdateTDoctorsNotesParam
    { 
        public long DoctNoteId { get; set; }
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string DoctorsNotes { get; set; }
        public long ModifiedBy { get; set; }
    }
}
