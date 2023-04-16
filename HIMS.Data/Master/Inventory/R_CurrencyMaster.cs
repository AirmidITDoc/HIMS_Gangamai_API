using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_CurrencyMaster:GenericRepository,I_CurrencyMaster
    {
        public R_CurrencyMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(CurrencyMasterParams currencyMasterParams)
        {
            var disc = currencyMasterParams.UpdateCurrencyMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_CurrencyMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(CurrencyMasterParams currencyMasterParams)
        {
            var disc = currencyMasterParams.InsertCurrencyMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_CurrencyMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
