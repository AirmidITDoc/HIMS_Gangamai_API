using HIMS.Model.CustomerInformation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.CustomerInformation
{
    public class MTemplateMasterParam
    {
        public SaveMTemplateMasterParam SaveMTemplateMasterParam { get; set; }
        public UpdateMTemplateMasterParam UpdateMTemplateMasterParam { get; set; }
        public CancelMTemplateMasterParam CancelMTemplateMasterParam { get; set; }

    }

    public class SaveMTemplateMasterParam
    {
        public int TemplateId { get; set; }
        public string Category { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }
      
        public long IsActive { get; set; }
        
        public int CreatedBy { get; set; }

       
    }
    public class UpdateMTemplateMasterParam
    {
        public int TemplateId { get; set; }
        public string Category { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDesc { get; set; }

        public long IsActive { get; set; }

        public int ModifiedBy { get; set; }
       

    }
    public class CancelMTemplateMasterParam
    {
        public long TemplateId { get; set; }
        public long IsCanelled { get; set; }
        public int IsCancelledBy { get; set; }


       


    }




}


    