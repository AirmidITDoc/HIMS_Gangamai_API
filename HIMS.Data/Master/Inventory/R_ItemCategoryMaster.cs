using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_ItemCategoryMaster : GenericRepository,I_ItemCategoryMaster
    {
        public R_ItemCategoryMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

      
        public bool Save(ItemCategoryMasterParams itemCategoryMasterParams)
        {
          //  throw new NotImplementedException();


            var disc = itemCategoryMasterParams.InsertItemCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ItemCategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(ItemCategoryMasterParams itemCategoryMasterParams)
        {
           // throw new NotImplementedException();

            var disc = itemCategoryMasterParams.UpdateItemCategoryMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ItemCategoryMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
