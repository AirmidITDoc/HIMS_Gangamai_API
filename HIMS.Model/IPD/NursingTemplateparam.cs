using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.IPD
{
   public class NursingTemplateparam
    {
        public NursingTemplateInsert NursingTemplateInsert { get; set; }
        // public DoctorNoteUpdate DoctorNoteUpdate { get; set; }
    }

    public class NursingTemplateInsert
    {

        public String NursTempName { get; set; }
        public String TemplateDesc { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}