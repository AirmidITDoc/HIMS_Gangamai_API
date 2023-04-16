using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Master.Inventory
{
    public class ModeofPaymentMasterParams
    {
        public InsertModeofPaymentMaster InsertModeofPaymentMaster { get; set; }
        public UpdateModeofPaymentMaster UpdateModeofPaymentMaster { get; set; }
    }
    public class InsertModeofPaymentMaster 
    {
        public string ModeOfPayment { get; set; }
        public bool IsDeleted { get; set; }
        public long AddedBy { get; set; }
    }
    public class UpdateModeofPaymentMaster
    {
        public long Id { get; set; }
        public string ModeOfPayment { get; set; }
        public bool IsDeleted { get; set; }
        public long UpdatedBy { get; set; }

    }
}
