using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
   public class R_ItemGenericMaster:GenericRepository,I_ItemGenericMaster
    {
        public R_ItemGenericMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(ItemGenericMasterParams itemGenericMasterParams)
        {
            var disc = itemGenericMasterParams.UpdateItemGenericMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_ItemGenericNameMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ItemGenericMasterParams itemGenericMasterParams)
        {
            var disc = itemGenericMasterParams.InsertItemGenericMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_ItemGenericNameMaster_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
