using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
  public  class R_MaritalStatusMaster :GenericRepository,I_MaritalStatusMaster
    {
        public R_MaritalStatusMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(MaritalStatusMasterParams MaritalStatusMasterParams)
        {
            var disc1 = MaritalStatusMasterParams.MaritalStatusMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_MaritalStatusMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(MaritalStatusMasterParams MaritalStatusMasterParams)
        {
            // throw new NotImplementedException();
            var disc = MaritalStatusMasterParams.MaritalStatusMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_MaritalStatusMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
