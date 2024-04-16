using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
    public  interface I_Constants
    {
        public string InsertConstantsParam(ConstantsParam constantsParam);
        public bool UpdateConstantsParam(ConstantsParam constantsParam);
    }
}
