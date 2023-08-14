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
            ExecNonQueryProcWithOutSaveChanges("update_ModeOfPayment_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
        public bool Insert(ModeofPaymentMasterParams modMasterParams)
        {
            var disc =modMasterParams.InsertModeofPaymentMaster.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_ModeOfPayment_1", disc);
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
