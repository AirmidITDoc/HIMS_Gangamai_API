using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class MaterialAcceptParams
    {
        public MaterialAcceptIssueHeader MaterialAcceptIssueHeader { get; set; }
        public List<MaterialAcceptIssueDetails> MaterialAcceptIssueDetails { get; set; }

    }

    public class MaterialAcceptIssueHeader
    {
        public int IssueId { get; set; }
        public int AcceptedBy { get; set; }
        
    }
    public class MaterialAcceptIssueDetails
    {
        public int IssueId { get; set; }
        public int IssueDetId { get; set; }
        public string Status { get; set; }
    }

}
