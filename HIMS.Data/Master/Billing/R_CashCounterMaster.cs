using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{ 
  public  class R_CashCounterMaster :GenericRepository,I_CashCounterMaster
    {
        public R_CashCounterMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(CashCounterMasterParams CashCounterMasterParams)
        {
            var disc1 = CashCounterMasterParams.CashCounterMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_CashCounter_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(CashCounterMasterParams CashCounterMasterParams)
        {
            // throw new NotImplementedException();
            var disc = CashCounterMasterParams.CashCounterMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_CashCounter_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
