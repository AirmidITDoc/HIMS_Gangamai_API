using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Prescription
{
   public class PrescriptionTemplateMasterParams
    {
        public PrescriptionTemplateMasterInsert PrescriptionTemplateMasterInsert { get; set; }
        public PrescriptionTemplateMasterUpdate PrescriptionTemplateMasterUpdate { get; set; }
    }
    public class PrescriptionTemplateMasterInsert
    {
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
   
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }

    public class PrescriptionTemplateMasterUpdate
    {
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }

    }
}

