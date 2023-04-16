using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class StoreMasterParams
    {
        public InsertStoreMaster InsertStoreMaster { get; set; }
        public UpdateStoreMaster UpdateStoreMaster { get; set; }
    }
    public class InsertStoreMaster
    {
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public string IndentPrefix { get; set; }
        public string IndentNo { get; set; }
        public string PurchasePrefix { get; set; }
        public string PurchaseNo { get; set; }
        public string GrnPrefix { get; set; }
        public string GrnNo { get; set; }
        public string GrnreturnNoPrefix { get; set; }
        public string GrnreturnNo { get; set; }
        public string IssueToDeptPrefix { get; set; }
        public string IssueToDeptNo { get; set; }
        public string ReturnFromDeptNoPrefix { get; set; }
        public string ReturnFromDeptNo { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }
    public class UpdateStoreMaster
    {
        public long StoreId { get; set; }
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
      }
}
