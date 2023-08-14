using HIMS.Common.Utility;
using HIMS.Model.Master.Pathology;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Pathology
{
    public class R_UnitMaster:GenericRepository,I_UnitMaster
    {
        public R_UnitMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(UnitMasterParams unitMasterParams)
        {
            var disc = unitMasterParams.UpdateUnitMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_PathUnitMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(UnitMasterParams unitMasterParams)
        {
            var disc = unitMasterParams.InsertUnitMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_PathUnitMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
