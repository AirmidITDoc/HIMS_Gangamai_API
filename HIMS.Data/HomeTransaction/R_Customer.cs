using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public class R_Customer :GenericRepository ,I_Customer
    {
        public R_Customer(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

         public bool Save(CustomerParams CustomerParams)
        {
           // throw new NotImplementedException();
            var disc1 = CustomerParams.CustomerInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_T_Customer", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }


        public bool Update(CustomerParams CustomerParams)
        {
           // throw new NotImplementedException();
            var disc1 = CustomerParams.CustomerUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_Customer", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
