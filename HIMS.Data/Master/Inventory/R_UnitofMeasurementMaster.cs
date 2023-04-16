using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_UnitofMeasurement : GenericRepository, I_UnitofMeasurementMaster
    {
        public R_UnitofMeasurement(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(UnitofMeasurementMasterParams uomMasterParams)
        {
            var disc = uomMasterParams.UpdateUnitofMeasurementMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_UnitofMeasurement", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(UnitofMeasurementMasterParams uomMasterParams)
        {
            var disc = uomMasterParams.InsertUnitofMeasurementMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_UnitofMeasurementMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
