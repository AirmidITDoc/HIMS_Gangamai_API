using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
   public class R_CompanyTypeMaster :GenericRepository,I_CompanyTypeMaster 
    {
        public R_CompanyTypeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(CompanyTypeMasterParams CompanyTypeMasterParams)
        {
            var disc1 = CompanyTypeMasterParams.CompanyTypeMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_CompanyTypeMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(CompanyTypeMasterParams CompanyTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = CompanyTypeMasterParams.CompanyTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_CompanyTypeMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

       /* public bool Update(CompanyMasterParams CompanyMasterParams)
        {
            throw new NotImplementedException();
        }

        public bool Save(CompanyMasterParams CompanyMasterParams)
        {
            throw new NotImplementedException();
        }*/
    }
}
