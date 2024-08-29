using HIMS.Common.Utility;
using HIMS.Model.Master.DoctorMaster;
using System;
using System.Collections.Generic;
using System.Text;
using static HIMS.Model.Master.DoctorTypeMaster;

namespace HIMS.Data.Master.DoctorMaster
{
  public  class R_DoctorTypeMaster :GenericRepository,I_DoctorTypeMaster
    {
        public R_DoctorTypeMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(Model.Master.DoctorMaster.DoctorTypeMasterParams DoctorTypeMasterParams)
        {
            var disc1 = DoctorTypeMasterParams.DoctorTypeMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_DoctorTypeMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(Model.Master.DoctorMaster.DoctorTypeMasterParams DoctorTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = DoctorTypeMasterParams.DoctortTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("m_insert_DoctorTypeMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
