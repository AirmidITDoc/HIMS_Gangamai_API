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
        public String BedName { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public int AddedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class BedMasterUpdate
    {

        public int BedID { get; set; }
        public String BedName { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public int UpdatedBy { get; set; }
    }
}

