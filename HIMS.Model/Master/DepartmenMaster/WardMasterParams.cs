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
        public String RoomName { get; set; }
        public int RoomType { get; set; }
        public int LocationId { get; set; }
        public bool IsAvailable { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int ClassId { get; set; }

    }

    public class WardMasterUpdate
    {

        public int RoomID { get; set; }
        public String RoomName { get; set; }
        public int RoomType { get; set; }
        public int LocationId { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
        public int ClassId { get; set; }
    }
}

