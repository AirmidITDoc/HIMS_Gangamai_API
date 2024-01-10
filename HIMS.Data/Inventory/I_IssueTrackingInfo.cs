using HIMS.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Inventory
{
   public interface I_IssueTrackingInfo
    {
        public string Insert(IssueTrackerParams IssueTrackerParams);
        public bool Update(IssueTrackerParams IssueTrackerParams);
        public bool UpdateStatus(IssueTrackerParams IssueTrackerParams);
    }
}
