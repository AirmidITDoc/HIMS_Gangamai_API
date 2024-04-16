using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_Workorder
    {
        public string InsertWorkOrder(Workorder Workorder);
        public bool UpdateWorkOrder(Workorder Workorder);
        string ViewPurWorkorder(int WOID, string htmlFilePath, string HeaderName);
    }
}
