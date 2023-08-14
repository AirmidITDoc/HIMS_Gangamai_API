using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
   public class R_SubTpaCompanyMaster :GenericRepository,I_SubTpaCompanyMaster
    {
        public R_SubTpaCompanyMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        } 

        public bool Update(SubTpaCompanyMasterParams SubTpaCompanyMasterParams)
        {
            var disc1 = SubTpaCompanyMasterParams.SubTpaCompanyMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_M_SubTPACompanyMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(SubTpaCompanyMasterParams SubTpaCompanyMasterParams)
        {
            // throw new NotImplementedException();
            var disc = SubTpaCompanyMasterParams.SubTpaCompanyMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("Insert_M_SubTPACompanyMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
