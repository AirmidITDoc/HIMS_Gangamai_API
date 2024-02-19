using HIMS.Common.Utility;
using HIMS.Model.IPD;
using HIMS.Model.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace HIMS.Data.Users
{
    public class R_UserChangePassword : GenericRepository, I_UserChangePassword
    {
        public R_UserChangePassword(IUnitofWork unitofWork) : base(unitofWork)
        {

        }
        public bool Update(UserChangePasswordParams userChangePasswordParams)
        {

            var disc3 = userChangePasswordParams.ChangePassword.ToDictionary();
            ExecNonQueryProcWithOutSaveChanges("m_update_UserPassword", disc3);

            _unitofWork.SaveChanges();
            return true;
        }
    }
}
