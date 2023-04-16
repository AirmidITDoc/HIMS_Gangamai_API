using HIMS.Common.Utility;
using HIMS.Model.Opd.OP;
using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Data.Opd
{
   public class R_Configsetting:GenericRepository,I_Configsetting
    {
        public R_Configsetting(IUnitofWork unitofWork) : base(unitofWork)
        {
            //transaction and connection is open when you inject unitofwork
        }

        public bool Update(ConfigSettingparam ConfigSettingparam)
        {
            // throw new NotImplementedException();
                      
            var disc3 = ConfigSettingparam.Configsettingupdate.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("update_ConfigSetting_1", disc3);

            _unitofWork.SaveChanges();

            return true;
        }
    }
}
