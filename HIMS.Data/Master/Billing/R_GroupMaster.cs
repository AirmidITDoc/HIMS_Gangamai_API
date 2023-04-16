using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{ 
   public class R_GroupMaster :GenericRepository,I_GroupMaster
    {
        public R_GroupMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(GroupMasterParams GroupMasterParams)
        {
            var disc1 = GroupMasterParams.GroupMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_GroupMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(GroupMasterParams GroupMasterParams)
        {
            // throw new NotImplementedException();
            var disc = GroupMasterParams.GroupMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_GroupMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
