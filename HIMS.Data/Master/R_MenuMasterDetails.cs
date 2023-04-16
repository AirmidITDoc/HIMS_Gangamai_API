using HIMS.Common.Utility;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public class R_MenuMasterDetails :GenericRepository,I_MenuMasterDetails
    {
        public R_MenuMasterDetails(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(MenuMasterDetailsParams menuMasterDetailsParams)
        {
            //Update VendorMaster
            var disc = menuMasterDetailsParams.MenuMasterDetailsUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_Menu_Master_detail_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(MenuMasterDetailsParams menuMasterDetailsParams)
        {

            var disc = menuMasterDetailsParams.MenuMasterDetailsInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_Menu_Master_detail_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}


