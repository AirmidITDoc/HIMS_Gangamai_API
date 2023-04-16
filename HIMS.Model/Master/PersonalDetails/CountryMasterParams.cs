using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class CountryMasterParams
    {
        public CountryMasterInsert CountryMasterInsert { get; set; }
        public CountryMasterUpdate CountryMasterUpdate { get; set; }

    }

    public class CountryMasterInsert
    {
        public String CountryName { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class CountryMasterUpdate
    {

        public int CountryID { get; set; }
        public String CountryName { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

