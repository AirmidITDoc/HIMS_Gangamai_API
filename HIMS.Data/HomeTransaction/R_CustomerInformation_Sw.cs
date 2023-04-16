using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
    public class R_CustomerInformation_Sw : GenericRepository, I_CustomerInformation_Sw
    {

        public R_CustomerInformation_Sw(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }
        public bool Save(CustomerInformation_SwParams CustomerInformation_SwParams)
        {
            //throw new NotImplementedException();
            var disc1 = CustomerInformation_SwParams.CustomerInformation_SwInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_T_CustomerInformation_Sw", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        public bool Update(CustomerInformation_SwParams CustomerInformation_SwParams)
        {
            // throw new NotImplementedException();
            var disc1 = CustomerInformation_SwParams.CustomerInformation_SwUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_CustomerInformation_Sw", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }

    
}
