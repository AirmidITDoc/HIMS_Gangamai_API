using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public interface I_PHAdvanceRefund
    {
        string Insert(PharRefundofAdvanceParams pharRefundofAdvanceParams);

        string ViewIPPharmaRefundofAdvanceReceipt(int RefundId, string htmlFilePath, string htmlHeaderFilePath);
    }
}
