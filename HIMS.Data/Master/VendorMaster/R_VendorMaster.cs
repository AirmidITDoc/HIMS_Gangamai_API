using HIMS.Common.Utility;
using HIMS.Model.Master.VendorMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master.VendorMaster
{
   public class R_VendorMaster :GenericRepository,I_VendorMaster
    {
        public R_VendorMaster(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(VendorMasterParams vendorMasterParams)
        {
            //Update VendorMaster
            var disc = vendorMasterParams.VendorMasterUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_M_VendorMaster", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(VendorMasterParams vendorMasterParams)
        {

            var disc = vendorMasterParams.VendorMasterInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("insert_M_VendorMaster_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
