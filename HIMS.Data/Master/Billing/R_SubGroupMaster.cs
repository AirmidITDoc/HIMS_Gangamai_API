using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
   public class R_SubGroupMaster :GenericRepository,I_SubGroupMaster
    {
        public R_SubGroupMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(SubGroupMasterParams SubGroupMasterParams)
        {
            var disc1 = SubGroupMasterParams.SubGroupMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_SubGroupMaster", disc1);
             
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(SubGroupMasterParams SubGroupMasterParams)
        {
            // throw new NotImplementedException();
            var disc = SubGroupMasterParams.SubGroupMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_SubGroupMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
