using HIMS.Common.Utility;
using HIMS.Model.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Pharmacy
{
   public class R_PharmPaymentMode:GenericRepository,I_PharmPaymentMode
    {
        public R_PharmPaymentMode(IUnitofWork unitofWork) : base(unitofWork)
        {

        }

       
        public string UpdatePaymentMode(PharmPaymentMode PharmPaymentMode)
        {
            var disc = PharmPaymentMode.PaymentModeUpdate.ToDictionary();
            var BillNo = ExecNonQueryProcWithOutSaveChanges("Update_PaymentPharmacy_Mode", disc);


            _unitofWork.SaveChanges();
            return BillNo;
        }
    }
}
