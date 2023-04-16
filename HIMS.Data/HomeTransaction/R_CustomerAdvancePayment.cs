using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction 
{
    public class R_CustomerAdvancePayment :GenericRepository,I_CustomerAdvancePayment
    {
        public R_CustomerAdvancePayment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

            public bool Save(CustomerAdvancePaymentParams customerAdvancePaymentParams)
        {
            //throw new NotImplementedException();
            var disc = customerAdvancePaymentParams.CustomerAdvancePaymentInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_T_CustomerAdvancePayment", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
                
        public bool Update(CustomerAdvancePaymentParams customerAdvancePaymentParams)
        {
            //throw new NotImplementedException();
            var disc = customerAdvancePaymentParams.CustomerAdvancePaymentUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_CustomerAdvancePayment", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
