using HIMS.Model.Administration;
using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Administration
{
    public  interface I_Administration
    {
        public bool UpdateBillcancellation(AdministrationParam administrationParam);
    }
}
