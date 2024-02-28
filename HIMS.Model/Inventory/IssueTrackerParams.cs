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
        public DateTime IssueRaisedDate { get; set; }
        public DateTime IssueRaisedTime { get; set; }
        public string IssueSummary { get; set; }
        public string IssueDescription { get; set; }
        public string UploadImagePath { get; set; }
        public string ImageName { get; set; }
        public string IssueStatus { get; set; }
        public string IssueRaised { get; set; }
        public string IssueAssigned { get; set; }
        public long Addedby { get; set; }
    }
    public class UpdateIssueTracker
    {
        public string Operation { get; set; }
        public long IssueTrackerId { get; set; }
        public string IssueSummary { get; set; }
        public string IssueDescription { get; set; }
        public string UploadImagePath { get; set; }
        public string ImageName { get; set; }
        public string IssueStatus { get; set; }
        public string IssueRaised { get; set; }
        public string IssueAssigned { get; set; }
        public long UpdatedBy { get; set; }
    }
    public class UpdateIssueTrackerStatus
    {
        public string Operation { get; set; }
        public long IssueTrackerId { get; set; }
        public string IssueStatus { get; set; }
        public long UpdatedBy { get; set; }
    }
}
