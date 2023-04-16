using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
    public interface I_grn
    {
        bool Save(GrnParams grnParams);
    }
}
