using HIMS.Common.Utility;
using HIMS.Model.Master.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.Billing
{
  public  class R_ClassMaster : GenericRepository,I_ClassMaster
    {
        public R_ClassMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(ClassMasterParams ClassMasterParams)
        {
            var disc1 = ClassMasterParams.ClassMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("ps_Update_M_ClassMaster", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(ClassMasterParams ClassMasterParams)
        {
            // throw new NotImplementedException();
            var disc = ClassMasterParams.ClassMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("ps_Insert_M_ClassMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

    }
}
