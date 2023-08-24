using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Inventory
{
    public class IndentParams
    {
        public InsertIndent InsertIndent { get; set; }
        public List<InsertIndentDetail> InsertIndentDetail { get; set; }
        public UpdateIndent UpdateIndent { get; set; }
        public DeleteIndent DeleteIndent { get;set;}
    }

    public class InsertIndent
    {
        public DateTime IndentDate  { get; set; }
        public DateTime IndentTime { get; set; }
        public long FromStoreId { get; set; }
        public long ToStoreId { get; set; }
        public long Addedby { get; set; }
    }
    public class InsertIndentDetail{
        public long IndentId { get; set; }
        public long ItemId { get; set; }
        public long Qty { get; set; }
    }

    public class UpdateIndent
    {
        public long IndentId { get; set; }
        public long FromStoreId { get; set; }
        public long ToStoreId { get; set; }
    }
    public class DeleteIndent
    {
        public long IndentId { get; set; }
    }
}
