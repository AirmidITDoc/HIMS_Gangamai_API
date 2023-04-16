using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
   public class R_CustomerEnquiry :GenericRepository ,I_CustomerEnquiry
    {
        public R_CustomerEnquiry(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(CustomerEnquiryParams customerEnquiryParams)
        {
            //throw new NotImplementedException();
            var disc1 = customerEnquiryParams.CustomerEnquiryInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_T_CustomerEnquiry", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Update(CustomerEnquiryParams customerEnquiryParams)
        {
           // throw new NotImplementedException();
            var disc1 = customerEnquiryParams.CustomerEnquiryUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_CustomerEnquiry", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
