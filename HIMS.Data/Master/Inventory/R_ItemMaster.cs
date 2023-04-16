using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_ItemMaster:GenericRepository,I_ItemMaster
    {

        public R_ItemMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }
          
        //Item insert update to store 
        public bool Update(ItemMasterParams itemMasterParams)
        {
            var disc1 = itemMasterParams.UpdateItemMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_ItemMaster_1", disc1);

            var D_Det = itemMasterParams.DeleteAssignItemToStore.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Delete_AssignItemtoStore", D_Det);

            foreach (var a in itemMasterParams.InsertAssignItemToStore)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ItemMasterParams itemMasterParams)
        {
            var outputId = new SqlParameter
            {
                SqlDbType = SqlDbType.BigInt,
                ParameterName = "@ItemID",
                Value = 0,
                Direction = ParameterDirection.Output
            };

            var disc1 = itemMasterParams.InsertItemMaster.ToDictionary();
            disc1.Remove("ItemID");
            var itemId = ExecNonQueryProcWithOutSaveChanges("Insert_ItemMaster_1_New", disc1, outputId);

            foreach (var a in itemMasterParams.InsertAssignItemToStore)
            {
                var disc = a.ToDictionary();
                disc["ItemId"] = itemId;
                ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_assignItemToStore_1", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
    
    }

}
