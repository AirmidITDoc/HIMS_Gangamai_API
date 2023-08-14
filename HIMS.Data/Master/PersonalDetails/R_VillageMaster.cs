using HIMS.Common.Utility;
using HIMS.Model.Master.PersonalDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.PersonalDetails
{
    public class R_VillageMaster : GenericRepository, I_VillageMaster
    {
        public R_VillageMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(VillageMasterParams VillageMasterParams)
        {
            var disc1 = VillageMasterParams.VillageMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_M_VillageMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(VillageMasterParams VillageMasterParams)
        {
            // throw new NotImplementedException();
            var disc = VillageMasterParams.VillageMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_M_VillageMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
