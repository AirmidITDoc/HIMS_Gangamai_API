using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.PersonalDetails
{
   public class VillageMasterParams
    {
        public VillageMasterInsert VillageMasterInsert { get; set; }
        public VillageMasterUpdate VillageMasterUpdate { get; set; }

    }

    public class VillageMasterInsert
    {
        public String VillageName { get; set; }
        public int TalukaId { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
       
    }

    public class VillageMasterUpdate
    {

        public int VillageID { get; set; }
        public String VillageName { get; set; }
        public int TalukaId { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}
