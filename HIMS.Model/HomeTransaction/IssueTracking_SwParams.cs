using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Transaction
{
  public  class IssueTracking_SwParams
    {
        public IssueTracking_SwInsert IssueTracking_SwInsert { get; set; }
        public IssueTracking_SwUpdate IssueTracking_SwUpdate { get; set; }
    }

    public class IssueTracking_SwInsert
    {
        
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public int ClientID { get; set; }
        public string IssueDescription { get; set; }
        public string IssueStatus { get; set; }

        public string IssueResolvedComment { get; set; }
        public DateTime IssueResolvedDate { get; set; }
        public int AddedBy { get; set; }
        public int IssueStatusId { get; set; }
        
    }

    public class IssueTracking_SwUpdate
    {
        public int IssueId { get; set; }
        public DateTime TranDate { get; set; }
        public DateTime TranTime { get; set; }
        public int ClientID { get; set; }
        public string IssueDescription { get; set; }
        public string IssueStatus { get; set; }

        public string IssueResolvedComment { get; set; }
        public DateTime IssueResolvedDate { get; set; }
        public int UpdatedBy { get; set; }

        public int IssueStatusId { get; set; }
    }

}
