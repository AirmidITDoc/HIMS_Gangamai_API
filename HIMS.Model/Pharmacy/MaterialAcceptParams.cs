using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Pharmacy
{
    public class MaterialAcceptParams
    {
        public MaterialAcceptIssueHeader MaterialAcceptIssueHeader { get; set; }
        public List<MaterialAcceptIssueDetails> MaterialAcceptIssueDetails { get; set; }
        public MaterialAcceptStockUpdate MaterialAcceptStockUpdate { get; set; }
        public UpdateStockToMainStock UpdateStockToMainStock { get; set; }

    }

    public class MaterialAcceptIssueHeader
    {
        public int IssueId { get; set; }
        public int AcceptedBy { get; set; }
        public int IsAccepted { get; set; }

    }
    public class MaterialAcceptIssueDetails
    {
        public int IssueId { get; set; }
        public int IssueDetId { get; set; }
        public string Status { get; set; }
    }
    public class MaterialAcceptStockUpdate
    {
        public int IssueId { get; set; }

    }
    public class UpdateStockToMainStock
    {
        public long IssueId { get; set; }
        public long IssueDetId { get; set; }
        public int ReturnQty { get; set; }
        public long StockId { get; set; }
        public long StoreId { get; set; }
    }

}
