using HIMS.Model.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Master
{
    public interface I_MenuMaster
    {
        bool Save(MenuMasterParams menuMasterParams);

        bool Update(MenuMasterParams menuMasterParams);
    }
}
