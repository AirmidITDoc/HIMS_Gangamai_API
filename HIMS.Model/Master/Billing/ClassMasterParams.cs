using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Billing
{
   public class ClassMasterParams
    {
        public ClassMasterInsert ClassMasterInsert { get; set; }
        public ClassMasterUpdate ClassMasterUpdate { get; set; }

    }

    public class ClassMasterInsert
    {
        public String ClassName { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }

    }

    public class ClassMasterUpdate
    {

        public int ClassID { get; set; }
        public String ClassName { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

