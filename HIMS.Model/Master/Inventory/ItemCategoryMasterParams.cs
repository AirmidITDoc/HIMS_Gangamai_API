namespace HIMS.Model.Master.Inventory
{
    public class ItemCategoryMasterParams
    {
        public InsertItemCategoryMaster InsertItemCategoryMaster { get; set; }
        public UpdateItemCategoryMaster UpdateItemCategoryMaster { get; set; }
    }

    public class InsertItemCategoryMaster
    { 
        public string ItemCategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
        public long ItemTypeId { get; set; }
    }

    public class UpdateItemCategoryMaster
    {
        public long ItemCategoryId { get; set; }
        public string ItemCategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
        public long ItemTypeId { get; set; }

    }
}
