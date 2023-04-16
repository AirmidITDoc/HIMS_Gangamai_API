using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class UnitofMeasurementMasterParams
    {

        public InsertUnitofMeasurementMaster InsertUnitofMeasurementMaster { get; set; }
        public UpdateUnitofMeasurementMaster UpdateUnitofMeasurementMaster { get; set; }
    }

    public class InsertUnitofMeasurementMaster
    {
        public string UnitofMeasurementName { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }

    }

    public class UpdateUnitofMeasurementMaster
    {
        public long UnitofMeasurementId { get; set; }
        public string UnitofMeasurementName { get; set; }
        public Boolean IsDeleted { get; set; }
        public long UpdatedBy { get; set; }
    }
}
