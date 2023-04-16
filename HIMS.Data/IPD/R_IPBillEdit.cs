using HIMS.Common.Utility;
using HIMS.Model.IPD;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.IPD
{
    public class R_IPBillEdit : GenericRepository, I_IPBillEdit
    {
        public R_IPBillEdit(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool Update(IPBillEditparam IPBillEditparam )
        {
            // throw new NotImplementedException();

            foreach (var a in IPBillEditparam.UpdateAddChargesforBillEdit)
            {
                var disc1 = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_AddCharges_forBillEdit", disc1);
            }
            foreach (var a in IPBillEditparam.UpdateBillForEdit)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_Bill_ForEdit", disc);
            }


            foreach (var a in IPBillEditparam.UpdatePaymentForBillEdit)
            {
                var disc = a.ToDictionary();
                ExecNonQueryProcWithOutSaveChanges("Update_Payment_ForBillEdit", disc);
            }


            _unitofWork.SaveChanges();
            return true;
        }

       
    }
}
