using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_ManufactureMaster:GenericRepository, I_ManufactureMaster
    {
        public R_ManufactureMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(ManufactureMasterParams manufMasterParams)
        {
            var disc = manufMasterParams.UpdateManufactureMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ManufactureMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ManufactureMasterParams manufMasterParams)
        {
            var disc = manufMasterParams.InsertManufactureMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ManufactureMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
