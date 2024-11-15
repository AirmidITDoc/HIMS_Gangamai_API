using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class IssueTrackerParams
    {
        public InsertIssueTracker InsertIssueTracker { get; set; }
        public UpdateIssueTracker UpdateIssueTracker { get; set; }
        public UpdateIssueTrackerStatus UpdateIssueTrackerStatus { get; set; }
    }
    public class InsertIssueTracker
    {
        public string IssueNo { get; set; }
        public int ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime IssueTime { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public int RaisedById { get; set; }
        public int AssignedToId { get; set; }
        public int StatusId { get; set; }
        public string DevComment { get; set; }
        public string Comment { get; set; }
        public int ReviewStatusId { get; set; }
        public int ReleaseStatus { get; set; }
        public DateTime ResolvedDate { get; set; }
        public DateTime ResolvedTime { get; set; }
        public string DocumentUpload { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
       

    }
    public class UpdateIssueTracker
    {
        public long IssueId { get; set; }

        public string IssueNo { get; set; }
        public int ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime IssueTime { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public int RaisedById { get; set; }
        public int AssignedToId { get; set; }
        public int StatusId { get; set; }
        public string DevComment { get; set; }
        public string Comment { get; set; }
        public int ReviewStatusId { get; set; }
        public int ReleaseStatus { get; set; }
        public DateTime ResolvedDate { get; set; }
        public DateTime ResolvedTime { get; set; }
        public string DocumentUpload { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

    }
    public class UpdateIssueTrackerStatus
    {
        public string Operation { get; set; }
        public long IssueTrackerId { get; set; }
        public string IssueStatus { get; set; }
        public long UpdatedBy { get; set; }
    }
}
