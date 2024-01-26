using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public interface I_IPRefundofAdvance
    {
        public String Insert(IPRefundofAdvanceParams IPRefundofAdvanceParams);
        // public bool Update(IPRefundofAdvanceParams IPRefundofAdvanceParams);

        string ViewIPRefundofAdvanceReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath);
    }
}
