using HIMS.Common.Utility;
using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public class R_WardMaster :GenericRepository,I_WardMaster
    {
        public R_WardMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(WardMasterParams WardMasterParams)
        {
            var disc1 = WardMasterParams.WardMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_WardMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(WardMasterParams WardMasterParams)
        {
            // throw new NotImplementedException();
            var disc = WardMasterParams.WardMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_WardMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
