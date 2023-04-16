using HIMS.Common.Utility;
using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public class R_BedMaster :GenericRepository,I_BedMaster
    {
        public R_BedMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(BedMasterParams BedMasterParams)
        {
            var disc1 = BedMasterParams.BedMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_BedMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(BedMasterParams BedMasterParams)
        {
            // throw new NotImplementedException();
            var disc = BedMasterParams.BedMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_BedMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
