using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Radiology
{
    public class CategoryMasterParams
    {
        public InsertCategoryMaster InsertCategoryMaster { get; set; }
        public UpdateCategoryMaster UpdateCategoryMaster { get; set; }
    }
    public class InsertCategoryMaster
    {
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }
    public class UpdateCategoryMaster
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
