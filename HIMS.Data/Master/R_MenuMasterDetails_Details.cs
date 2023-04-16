using HIMS.Common.Utility;
using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public class R_MenuMasterDetails_Details :GenericRepository,I_MenuMasterDetails_Details
    {
        public R_MenuMasterDetails_Details(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(MenuMasterDetails_DetailsParams menuMasterDetails_DetailsParams)
        {
            //Update VendorMaster
            var disc = menuMasterDetails_DetailsParams.MenuMasterDetails_DetailsUpdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Update_Menu_Master_detail_detail_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }

        public bool Save(MenuMasterDetails_DetailsParams menuMasterDetails_DetailsParams)
        {

            var disc = menuMasterDetails_DetailsParams.MenuMasterDetails_DetailsInsert.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("Insert_Menu_Master_detail_detail_W_1", disc);

            //commit transaction
            _unitofWork.SaveChanges();
            return true;
        }
    }
}
