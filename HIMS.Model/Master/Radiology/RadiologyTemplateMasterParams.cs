using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Radiology
{
    public class RadiologyTemplateMasterParams
    {
        public InsertRadiologyTemplateMaster InsertRadiologyTemplateMaster { get; set; }
        public UpdateRadiologyTemplateMaster UpdateRadiologyTemplateMaster { get; set; }
    }
    public class InsertRadiologyTemplateMaster
    {
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }
    public class UpdateRadiologyTemplateMaster
    {
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
