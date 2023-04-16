using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
  public interface I_MenuMasterDetails
    {
        bool Save(MenuMasterDetailsParams menuMasterDetailsParams);

        bool Update(MenuMasterDetailsParams menuMasterDetailsParams);
    }
}
