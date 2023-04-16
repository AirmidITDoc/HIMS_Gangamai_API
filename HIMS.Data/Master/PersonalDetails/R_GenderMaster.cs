using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
    public class R_GenderMaster : GenericRepository, I_GenderMaster
    {
        public R_GenderMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(GenderMasterParams GenderMasterParams)
        {
            //throw new NotImplementedException();
            var disc1 = GenderMasterParams.GenderMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_GenderMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(GenderMasterParams GenderMasterParams)
        {
            //throw new NotImplementedException();
            var disc = GenderMasterParams.GenderMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_GenderMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
