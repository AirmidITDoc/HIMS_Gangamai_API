using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
   public class R_ItemClassMaster:GenericRepository,I_ItemClassMaster
    {
        public R_ItemClassMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(ItemClassMasterParams itemClassMasterParams)
        {
            var disc = itemClassMasterParams.UpdateItemClassMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ItemClassMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ItemClassMasterParams itemClassMasterParams)
        {
            var disc = itemClassMasterParams.InsertItemClassMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ItemClassMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
