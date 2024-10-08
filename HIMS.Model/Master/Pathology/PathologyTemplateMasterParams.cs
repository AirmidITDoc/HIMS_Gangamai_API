﻿using System;
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

        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public bool IsDeleted { get; set; }

        public string TemplateDescInHTML { get; set; }
        public int AddedBy { get; set; }
    }
    public class UpdatePathologyTemplateMaster
    {

        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public bool IsDeleted { get; set; }
        public string TemplateDescInHTML { get; set; }
        public int UpdatedBy { get; set; }
       
    }
}
