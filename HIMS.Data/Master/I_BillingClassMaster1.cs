using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
    public interface I_BillingClassMaster1
    {
        List<dynamic> Get(string BillingClassName);
        bool Insert(BillingClassMaster billingclassmaster);
        bool Update(BillingClassMaster billingclassmaster);
    }
}
