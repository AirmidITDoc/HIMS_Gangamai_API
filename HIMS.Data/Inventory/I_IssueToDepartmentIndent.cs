using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
    public  interface I_IssueToDepartmentIndent
    {
        public string UpdateIndentStatusAganistIss(IssueToDepartmentIndentParam IssueToDepartmentIndentParam);

    }
}
