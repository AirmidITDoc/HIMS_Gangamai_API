using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class StateMasterParams
    {
        public StateMasterInsert StateMasterInsert { get; set; }
        public StateMasterUpdate StateMasterUpdate { get; set; }

    }

    public class StateMasterInsert
    {
        public String StateName { get; set; }
        public int CountryId { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class StateMasterUpdate
    {

        public int StateID { get; set; }
        public String StateName { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

