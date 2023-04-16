using HIMS.Common.Utility;
using HIMS.Model.Opd;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_Payment :GenericRepository,I_Payment
    {
        public R_Payment(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(PaymentParams PaymentParams)
        {
            var disc1 = PaymentParams.PaymentUpdate.ToDictionary();
            // ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BankMaster", disc1);
            
           ExecNonQueryProcWithOutSaveChanges("ps_Update_Payment_Advance", disc1);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(PaymentParams PaymentParams)
        {
            // throw new NotImplementedException();
            var disc = PaymentParams.PaymentInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_Payment_New_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       /* public bool Update(PaymentParams PaymentParams)
        {
            throw new NotImplementedException();
        }

        public bool Save(PaymentParams PaymentParams)
        {
            throw new NotImplementedException();
        }*/
    }
}
