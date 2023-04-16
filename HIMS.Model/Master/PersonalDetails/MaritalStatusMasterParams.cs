using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class MaritalStatusMasterParams
    {
        public MaritalStatusMasterInsert MaritalStatusMasterInsert { get; set; }
        public MaritalStatusMasterUpdate MaritalStatusMasterUpdate { get; set; }

    }

    public class MaritalStatusMasterInsert
    {
        public String MaritalStatusName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class MaritalStatusMasterUpdate
    {

        public int MaritalStatusID { get; set; }
        public String MaritalStatusName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

