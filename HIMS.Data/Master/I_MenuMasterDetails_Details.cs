using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
   public interface I_MenuMasterDetails_Details
    {
        public bool Save(MenuMasterDetails_DetailsParams MenuMasterDetails_DetailsParams);
        public bool Update(MenuMasterDetails_DetailsParams MenuMasterDetails_DetailsParams);
    }
}
