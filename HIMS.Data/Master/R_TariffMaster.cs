using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
   public class R_TariffMaster :GenericRepository,I_TariffMaster
    {
        public R_TariffMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(TariffMasterParams TariffMasterParams)
        {
            var disc1 = TariffMasterParams.TariffMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_TariffMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(TariffMasterParams TariffMasterParams)
        {
            // throw new NotImplementedException();
            var disc = TariffMasterParams.TariffMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_TariffMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
