using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
  public interface I_IssueTracking_Sw 
    {
        bool Update(IssueTracking_SwParams IssueTracking_SwParams);
        bool Save(IssueTracking_SwParams IssueTracking_SwParams);
    }
}
