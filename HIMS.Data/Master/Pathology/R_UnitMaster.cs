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
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_PathUnitMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(UnitMasterParams unitMasterParams)
        {
            var disc = unitMasterParams.InsertUnitMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_PathUnitMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
