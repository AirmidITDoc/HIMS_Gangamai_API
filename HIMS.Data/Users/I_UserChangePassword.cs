using System;
using System.Collections.Generic;
using System.Text;
using HIMS.Model.Users;

namespace HIMS.Data.Users
{
    public interface I_UserChangePassword
    {
        public bool Update(UserChangePasswordParams UserChangePasswordParams);


        public bool Insertlogin(UserChangePasswordParams UserChangePasswordParams);
        public bool UpdateLogin(UserChangePasswordParams UserChangePasswordParams);
    }
}
