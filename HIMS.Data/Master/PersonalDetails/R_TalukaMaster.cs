using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
   public class R_TalukaMaster :GenericRepository,I_TalukaMaster
    {
        public R_TalukaMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(TalukaMasterParams TalukaMasterParams)
        {
            var disc1 = TalukaMasterParams.TalukaMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_TalukaMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(TalukaMasterParams TalukaMasterParams)
        {
            // throw new NotImplementedException();
            var disc = TalukaMasterParams.TalukaMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_TalukaMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
