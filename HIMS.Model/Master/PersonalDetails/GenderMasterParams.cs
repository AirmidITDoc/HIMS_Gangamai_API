using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class GenderMasterParams
    {
        public GenderMasterInsert GenderMasterInsert { get; set; }
        public GenderMasterUpdate GenderMasterUpdate { get; set; }

    }

    public class GenderMasterInsert
    {
        public String GenderName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GenderMasterUpdate
    {
        public int GenderID { get; set; }
        public String GenderName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

