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

       
        public bool UpdatePaymentMode(PharmPaymentMode PharmPaymentMode)
        {
            if (PharmPaymentMode.PaymentModeUpdate.vType == "Hospital")
            {
                var disc = PharmPaymentMode.PaymentModeUpdate.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_Payment_Mode", disc);
            }
            else
            {
                var disc = PharmPaymentMode.PaymentModeUpdate.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("m_Update_PaymentPharmacy_Mode", disc);
            }

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
