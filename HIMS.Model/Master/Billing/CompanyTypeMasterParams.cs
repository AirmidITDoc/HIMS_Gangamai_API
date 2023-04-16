using System;
using System.Collections.Generic;
using System.Text;


namespace HIMS.Model.Master.Billing
{
   public class CompanyTypeMasterParams
    {
        public CompanyTypeMasterInsert CompanyTypeMasterInsert { get; set; }
        public CompanyTypeMasterUpdate CompanyTypeMasterUpdate { get; set; }

    }

    public class CompanyTypeMasterInsert
    {
        public String TypeName { get; set; }
        public int AddedBy { get; set; }
        public Boolean IsDeleted { get; set; }

    }

    public class CompanyTypeMasterUpdate
    {
        
        public int CompanyTypeId { get; set; }
        public String TypeName { get; set; }
        public Boolean IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

