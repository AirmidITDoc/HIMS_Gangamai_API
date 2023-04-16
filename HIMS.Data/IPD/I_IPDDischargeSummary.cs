using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
   public interface I_IPDDischargeSummary
    {
        public String Insert(IPDDischargeSummaryParams IPDDischargeSummaryParams);
        public bool Update(IPDDischargeSummaryParams IPDDischargeSummaryParams);
    }
}
