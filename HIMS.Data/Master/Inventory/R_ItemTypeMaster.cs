using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_ItemTypeMaster:GenericRepository, I_ItemTypeMaster
    {
        public R_ItemTypeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(ItemTypeMasterParams itemTypeMasterParams)
        {
            var disc = itemTypeMasterParams.UpdateItemTypeMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ItemTypeMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Save(ItemTypeMasterParams itemTypeMasterParams)
        {
            var disc = itemTypeMasterParams.InsertItemTypeMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ItemTypeMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
