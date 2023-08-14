using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.DepartmenMaster
{
   public class WardMasterParams
    {

        public WardMasterInsert WardMasterInsert { get; set; }
        public WardMasterUpdate WardMasterUpdate { get; set; }

    }

    public class WardMasterInsert
    {
        public String RoomName_1 { get; set; }
        public int RoomType_2 { get; set; }
        public int LocationId_3 { get; set; }
        public bool IsAvailible_4 { get; set; }
        public bool IsActive_5 { get; set; }
        public int ClassId { get; set; }

    }

    public class WardMasterUpdate
    {

        public int RoomId_1 { get; set; }
        public String RoomName_2 { get; set; }
        public int RoomType_3 { get; set; }
        public int LocationId_4 { get; set; }
        public bool IsActive_5 { get; set; }
        public int ClassID { get; set; }
    }
}

