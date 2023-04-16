using HIMS.Model.Master.Inventory;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Inventory
{
    public interface I_ModeofPaymentMaster
    {
        public bool Insert(ModeofPaymentMasterParams modMasterParams);
        public bool Update(ModeofPaymentMasterParams modMasterParams);
    }
}
