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
            ExecNonQueryProcWithOutSaveChanges("update_LocationMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(LocationMasterParams LocationMasterParams)
        {
            // throw new NotImplementedException();
            var disc = LocationMasterParams.LocationMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_LocationMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        
    }
}
