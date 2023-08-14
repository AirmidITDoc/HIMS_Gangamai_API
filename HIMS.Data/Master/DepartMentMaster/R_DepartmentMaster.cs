using HIMS.Common.Utility;
using HIMS.Model.Master.DepartmenMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.DepartMentMaster
{
   public class R_DepartmentMaster :GenericRepository,I_DepartmentMaster
    {
        public R_DepartmentMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(DepartmentMasterParams DepartmentMasterParams)
        {
            var disc1 = DepartmentMasterParams.DepartmentMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_DepartmentMaster_1", disc1);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(DepartmentMasterParams DepartmentMasterParams)
        {
            // throw new NotImplementedException();
            var disc = DepartmentMasterParams.DepartmentMasterInsert.ToDictionary();

            ExecNonQueryProcWithOutSaveChanges("insert_DepartmentMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;

        }

        
    }
}
