using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
  public  class IssueTrackingInformation
    {
        public InsertIssueTrackinginfo InsertIssueTrackinginfo { get; set; }
        
        public UpdateIssueTrackinginfo UpdateIssueTrackinginfo { get; set; }
        
    }

    public class InsertIssueTrackinginfo
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
        public int AddedBy { get; set; }
        public int UpdatedBy { get; set; }
       
        //public DateTime AddedDatetime { get; set; }
        //public DateTime UpdatedDatetime { get; set; }

        public long IssueTrackerId { get; set; }

    }
    public class UpdateIssueTrackinginfo
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
        public int UpdatedBy { get; set; }
        public DateTime AddedDatetime { get; set; }
        public DateTime UpdatedDatetime { get; set; }

        public long IssueTrackerId { get; set; }
    }

    
}

