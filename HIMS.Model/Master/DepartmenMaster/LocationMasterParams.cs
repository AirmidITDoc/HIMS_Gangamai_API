using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DepartmenMaster
{
   public class LocationMasterParams
    {
        public LocationMasterInsert LocationMasterInsert { get; set; }
        public LocationMasterUpdate LocationMasterUpdate { get; set; }

    }

    public class LocationMasterInsert
    {
        public String LocationName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class LocationMasterUpdate
    {
        public int LocationID { get; set; }
        public String LocationName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}
