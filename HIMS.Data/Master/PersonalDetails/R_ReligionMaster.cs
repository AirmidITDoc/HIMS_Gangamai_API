using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_ReligionMaster :GenericRepository,I_ReligionMaster
    {
        public R_ReligionMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(ReligionMasterParams ReligionMasterParams)
        {
            var disc1 = ReligionMasterParams.ReligionMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ReligionMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(ReligionMasterParams ReligionMasterParams)
        {
            // throw new NotImplementedException();
            var disc = ReligionMasterParams.ReligionMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ReligionMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        
    }
}
