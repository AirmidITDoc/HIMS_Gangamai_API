using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
    public class PathologyCategoryMasterParams
    {
        public InsertPathologyCategoryMaster InsertPathologyCategoryMaster { get; set; }
        public UpdatePathologyCategoryMaster UpdatePathologyCategoryMaster { get; set; }
    }
    public class InsertPathologyCategoryMaster 
    {
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }
    public class UpdatePathologyCategoryMaster 
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
