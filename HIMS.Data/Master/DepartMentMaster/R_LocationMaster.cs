using HIMS.Common.Utility;
using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public class R_LocationMaster :GenericRepository,I_LocationMaster
    {
        public R_LocationMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(LocationMasterParams LocationMasterParams)
        {
            var disc1 = LocationMasterParams.LocationMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_LocationMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(LocationMasterParams LocationMasterParams)
        {
            // throw new NotImplementedException();
            var disc = LocationMasterParams.LocationMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_LocationMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        
    }
}
