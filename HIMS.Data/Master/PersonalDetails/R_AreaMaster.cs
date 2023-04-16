using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_AreaMaster :GenericRepository,I_AreaMaster
    {
        public R_AreaMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(AreaMasterParams AreaMasterParams)
        {
            var disc1 = AreaMasterParams.AreaMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_AreaMaster", disc1);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(AreaMasterParams AreaMasterParams)
        {
            // throw new NotImplementedException();
            var disc = AreaMasterParams.AreaMasterInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_AreaMaster", disc);
            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
