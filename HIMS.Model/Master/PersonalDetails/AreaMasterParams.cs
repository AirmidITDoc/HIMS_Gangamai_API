using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
    public class AreaMasterParams
    {
        public AreaMasterInsert AreaMasterInsert { get; set; }
        public AreaMasterUpdate AreaMasterUpdate { get; set; }

    }

    public class AreaMasterInsert
    {
        public String AreaName { get; set; }
        public int TalukaId { get; set; }

        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class AreaMasterUpdate
    {

        public int AreaID { get; set; }
        public String AreaName { get; set; }
        public int TalukaId { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

