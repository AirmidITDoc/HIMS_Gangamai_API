using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
   public class R_ConsessionReasonMaster :GenericRepository,I_ConsessionReasonMaster
    {
        public R_ConsessionReasonMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(ConsessionReasonMasterParams ConsessionReasonMasterParams)
        {
            var disc1 = ConsessionReasonMasterParams.ConsessionReasonMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ConcessionReasonMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(ConsessionReasonMasterParams ConsessionReasonMasterParams)
        {
            // throw new NotImplementedException();
            var disc = ConsessionReasonMasterParams.ConsessionReasonMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ConcessionReasonMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
