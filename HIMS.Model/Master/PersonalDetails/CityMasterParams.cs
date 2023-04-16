using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class CityMasterParams
    {
        public CityMasterInsert CityMasterInsert { get; set; }
        public CityMasterUpdate CityMasterUpdate { get; set; }

    }

    public class CityMasterInsert
    {
        public String CityName { get; set; }
        public int StateId { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class CityMasterUpdate
    {

        public int CityID { get; set; }
        public String CityName { get; set; }
        public int StateId { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

