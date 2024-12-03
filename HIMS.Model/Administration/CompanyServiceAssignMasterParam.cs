using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using HIMS.Model.Opd;

namespace HIMS.Model.Administration
{
    public class CompanyServiceAssignMaster
    {
        public Delete_CompantServiceDetails Delete_CompantServiceDetails { get; set; }
        public List<insert_CompanyServiceAssignMaster> insert_CompanyServiceAssignMaster { get; set; }
    }
    public class Delete_CompantServiceDetails
    {
        public long CompanyId { get; set; }
      
    }

    public class insert_CompanyServiceAssignMaster
    {
        public long CompanyId { get; set; }
        public long ServiceId { get; set; }
        public long ServicePrice { get; set; }
        public long ServiceQty { get; set; }
        public long IsActive { get; set; }
        public long CreatedBy { get; set; }
    }
}
