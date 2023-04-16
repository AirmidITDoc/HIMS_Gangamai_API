using HIMS.Common.Utility;
using HIMS.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Transaction
{
    public class R_CustomerInfo_PayAdv_Sw : GenericRepository ,I_CustomerInfo_PayAdv_Sw 
    {

        //
        public R_CustomerInfo_PayAdv_Sw(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Save(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams)
        {
            //throw new NotImplementedException();
            var disc = CustomerInfo_PayAdv_SwParams.CustomerInfo_PayAdv_SwInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_T_CustomerInfo_PayAdv_Sw", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }


        public bool Update(CustomerInfo_PayAdv_SwParams CustomerInfo_PayAdv_SwParams)
        {
            //throw new NotImplementedException();
            var disc = CustomerInfo_PayAdv_SwParams.CustomerInfo_PayAdv_SwUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_T_CustomerInfo_PayAdv_Sw", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }


    }
}
