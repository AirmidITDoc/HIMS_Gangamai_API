using HIMS.Common.Utility;
using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public class R_ModeofPaymentMaster:GenericRepository,I_ModeofPaymentMaster
    {
        public R_ModeofPaymentMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection
        }

        public bool Update(ModeofPaymentMasterParams modMasterParams)
        {
            var disc = modMasterParams.UpdateModeofPaymentMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ModeOfPaymentMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ModeofPaymentMasterParams modMasterParams)
        {
            var disc =modMasterParams.InsertModeofPaymentMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ModeOfPaymentMaster", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
