using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
   public interface I_PathologyTemplateMaster
    {
        public bool Insert(PathologyTemplateMasterParams pathTemplateMasterParams);
        public bool Update(PathologyTemplateMasterParams pathTemplateMasterParams);
    }
}
