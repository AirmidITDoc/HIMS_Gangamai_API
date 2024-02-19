using System;
using System.Collections.Generic;
using System.Text;

namespace HIMS.Model.Users
{
    public class UserChangePasswordParams
    {
        public ChangePassword ChangePassword { get; set; }
    }

     public class ChangePassword
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
