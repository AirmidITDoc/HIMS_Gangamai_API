using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
    public class PathologyTemplateMasterParams
    {
        public InsertPathologyTemplateMaster InsertPathologyTemplateMaster { get; set; }
        public UpdatePathologyTemplateMaster UpdatePathologyTemplateMaster { get; set; }
    }
    public class InsertPathologyTemplateMaster
    {
        public long TestId { get; set; }
        public long TemplateId { get; set; }
    }
    public class UpdatePathologyTemplateMaster
    {
        public int PTemplateId  {get;set;}
        public long TestId { get; set; }
        public long TemplateId { get; set; }
    }
}
