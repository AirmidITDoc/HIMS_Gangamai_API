using HIMS.Model.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Administration
{
   public interface I_NewTemplateDescription
    {
        bool Insert(NewTemplateDescriptionParam NewTemplateDescriptionParam);

        bool Update(NewTemplateDescriptionParam NewTemplateDescriptionParam);
    }
}
