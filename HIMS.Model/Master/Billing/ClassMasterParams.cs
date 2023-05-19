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
        public Boolean IsActive { get; set; }

    }

    public class ClassMasterUpdate
    {

        public int ClassId { get; set; }
        public String ClassName { get; set; }
        public Boolean IsActive { get; set; }
        
    }
}

