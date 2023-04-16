using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
  public  class ReligionMasterParams
    {
        public ReligionMasterInsert ReligionMasterInsert { get; set; }
        public ReligionMasterUpdate ReligionMasterUpdate { get; set; }

    }

    public class ReligionMasterInsert
    {
        public String ReligionName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class ReligionMasterUpdate
    {

        public int ReligionID { get; set; }
        public String ReligionName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

