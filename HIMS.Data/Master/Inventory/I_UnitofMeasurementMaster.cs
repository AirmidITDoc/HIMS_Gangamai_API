using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_UnitofMeasurementMaster
    {
        public bool Insert(UnitofMeasurementMasterParams uomMasterParams);
        public bool Update(UnitofMeasurementMasterParams uomMasterParams);
    }
}
