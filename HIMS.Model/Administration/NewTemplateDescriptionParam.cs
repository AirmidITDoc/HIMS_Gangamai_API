using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Administration
{
   public class NewTemplateDescriptionParam
    {
        public InsertTempDescParam InsertTempDescParam { get; set; }

        public UpdateTempDescParam UpdateTempDescParam { get; set; }
    }

    public class InsertTempDescParam
    {
          public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
    }


    public class UpdateTempDescParam
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
    }
}
