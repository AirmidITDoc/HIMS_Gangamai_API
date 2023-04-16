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
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_DoctorTypeMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(Model.Master.DoctorMaster.DoctorTypeMasterParams DoctorTypeMasterParams)
        {
            // throw new NotImplementedException();
            var disc = DoctorTypeMasterParams.DoctortTypeMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_DoctorTypeMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }
    }
}
