using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{ 
   public class R_BankMaster :GenericRepository,I_BankMaster
    {
        public R_BankMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(BankMasterParams BankMasterParams)
        {
            var disc1 = BankMasterParams.BankMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_BankMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(BankMasterParams BankMasterParams)
        {
            // throw new NotImplementedException();
            var disc = BankMasterParams.BankMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_BankName_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
