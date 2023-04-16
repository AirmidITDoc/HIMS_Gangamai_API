using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DepartmenMaster
{
   public class DepartmentMasterParams 
    {
        public DepartmentMasterInsert DepartmentMasterInsert { get; set; }
        public DepartmentMasterUpdate DepartmentMasterUpdate { get; set; }

    }

    public class DepartmentMasterInsert
    {
        public String DepartmentName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class DepartmentMasterUpdate
    {
        public int DepartmentID { get; set; }
        public String DepartmentName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

