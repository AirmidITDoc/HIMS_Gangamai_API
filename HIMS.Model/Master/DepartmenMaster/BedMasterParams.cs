using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DepartmenMaster
{
  public  class BedMasterParams
    {

        public BedMasterInsert BedMasterInsert { get; set; }
        public BedMasterUpdate BedMasterUpdate { get; set; }

    }

    public class BedMasterInsert
    {

        public String BedName_1 { get; set; }
        public int RoomId_2 { get; set; }
        public bool IsAvailible_3 { get; set; }
                public bool IsActive_4 { get; set; }

    }

    public class BedMasterUpdate
    {
       
        public int BedId_1 { get; set; }
        public String BedName_2 { get; set; }
        public int RoomId_3 { get; set; }
        
        public bool IsActive_4 { get; set; }
        
    }
}

