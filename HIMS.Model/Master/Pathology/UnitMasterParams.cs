using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Pathology
{
    public class UnitMasterParams
    {
        public InsertUnitMaster InsertUnitMaster { get; set; }
        public UpdateUnitMaster UpdateUnitMaster { get; set; }
    }
    public class InsertUnitMaster 
    {
        public string UnitName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }
    public class UpdateUnitMaster 
    {
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
